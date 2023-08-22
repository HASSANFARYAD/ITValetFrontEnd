using ITValetFrontEnd.ClientSideServices;
using ITValetFrontEnd.HelperClasses;
using ITValetFrontEnd.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
using NuGet.Common;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;
using System.Runtime.InteropServices;

namespace ITValetFrontEnd.Controllers
{
    public class UserController : Controller
    {
        private readonly GeneralHelper _helper;
        private readonly IAdminService _adminService;
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        HttpClient _httpClient = new HttpClient();
        public UserController(GeneralHelper generalHelper, IAdminService adminService, IAuthService authService, IUserService userService)
        {
            _helper = generalHelper;
            _adminService = adminService;
            _authService = authService;
            _userService = userService;
        }

        public async Task<IActionResult> Account(string? messages = "")
        {
            var user = await _helper.ValidateUserAsync();
            if (user == null)
            {
                return RedirectToAction("UserLogin", "Auth");
            }
            if (user != null)
            {
                List<string> Days = new List<string>() { "Sunday", "Monday", "Tuesday","Wednesday", "Thursday", "Friday","Saturday"};
                List<string> Day = new List<string>();
                string todayDay = DateTime.UtcNow.DayOfWeek.ToString();
                int index = Days.IndexOf(todayDay);
                if(DateTime.UtcNow.TimeOfDay <= TimeSpan.Parse("11:59"))
                {
                    Day = Days.GetRange(index, Days.Count - index);
                }
                else
                {
                    index = index + 1;
                    if (index < Days.Count - 1)
                    {
                        Day = Days.GetRange(index, Days.Count - index);
                    }
                    else
                    {
                        Day.Add(Days[index]);
                    }
                }
                var response = await _adminService.GetUserById(user.Id, user.Token);
                if (response.StatusCode == "200" && response.Status == true)
                {
                    var updateUserRecord = JsonConvert.DeserializeObject<UserListDto>(response.Data.ToString());
                    ViewBag.UserRecord = updateUserRecord;
                }
                ViewBag.TodaysDay = string.Join(", ", Day);
            }
            return View();
        }

        public async Task<IActionResult> PostUpdateProfile(PostUpdateUserDto updateUser)
        {
            var user = await _helper.ValidateUserAsync();
            if (user == null)
            {
                return RedirectToAction("UserLogin", "Auth");
            }
            if (user != null)
            {
                updateUser.Id = user.Id;
                var response = await _userService.UpdateProfile(updateUser, user.Token);
                if (response.StatusCode == "200" && response.Status == true)
                {
                    var userObj = JsonConvert.DeserializeObject<UserClaims>(response.Data.ToString());
                    if (userObj != null)
                    {
                        userObj.Token = user.Token;
                        userObj.UserEncId = user.UserEncId;
                        await PostLogins(userObj);
                    }
                    return RedirectToAction("Account", new { msg = response.Message, color = ProjectVariables.SuccessColor });
                }
                else
                {
                    return RedirectToAction("UpdateProfile", "Auth", new { msg = response.Message, color = ProjectVariables.SuccessColor });
                }
            }
            return RedirectToAction("Login", "Auth");
        }

        public async Task<IActionResult> UploadProfileImage(IFormFile ImagePath)
        {
            var user = await _helper.ValidateUserAsync();
            if (user != null)
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(user.Token);
                _httpClient.Timeout = TimeSpan.FromMinutes(10);
                string endpoint = ProjectVariables.BaseUrl + "Admin/UploadPicture";
                string para = "UserId=" + user.Id;
                var requestUri = $"{endpoint}?{para}";
                var request = new HttpRequestMessage();
                if (ImagePath != null)
                {
                    request = new HttpRequestMessage(HttpMethod.Put, requestUri)
                    {
                        Content = BuildMultipartContent(ImagePath)
                    };
                }
                using var apiResponse = await _httpClient.SendAsync(request);
                var response = await apiResponse.Content.ReadFromJsonAsync<ResponseDto>();

                if (apiResponse.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    return RedirectToAction("Account", new { msg = response.Message, color = ProjectVariables.DangerColor });
                }
                else
                {
                    var userObj = JsonConvert.DeserializeObject<UserClaims>(response.Data.ToString());
                    if (userObj != null)
                    {
                        userObj.Token = user.Token;
                        userObj.UserEncId = user.UserEncId;
                        await PostLogins(userObj);
                    }
                    return RedirectToAction("Account", new { msg = response.Message, color = ProjectVariables.SuccessColor });

                }
            }
            else
            {
                return RedirectToAction("Auth", "UserLogin", new { msg = "Login Again", color = ProjectVariables.DangerColor });
            }
            
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("UserLogin", "Auth");
        }

        #region CustomerRequestHelp
        public async Task<IActionResult> RequestService(string msg = "", string color = "")
        {
            var user = await _helper.ValidateUserAsync();
            if (user == null)
            {
                return RedirectToAction("UserLogin", "Auth");
            }
            if (user.Role != "Valet")
            {
                ViewBag.RequestedServiceUserId = user.Id;
                return View();
            }
            else
            {
                return RedirectToAction("Error", "Home", new {msg = "You are not authorized to access this page", color = ProjectVariables.DangerColor});
            }
        }

        public async Task<IActionResult> PostRequestService(PostAddRequestServices postAddRequestServices)
        {
            var user = await _helper.ValidateUserAsync();
            if (postAddRequestServices.RequestServiceType == "Live")
            {
                postAddRequestServices.RequestServiceType = "1";
                postAddRequestServices.FromDateTime = DateTime.Now.ToString();
                postAddRequestServices.ToDateTime = DateTime.Now.ToString();
            }
            else
            {
                postAddRequestServices.RequestServiceType = "2";
            }
            var response = await _userService.AddRequestService(postAddRequestServices, user.Token);
            if (response.StatusCode == "200" && response.Status == true)
            {
                return RedirectToAction("UsersForRequestedService", new { requestServiceId = response.Data, msg = response.Message, color = ProjectVariables.SuccessColor });
            }
            else
            {
                return RedirectToAction("RequestService", new { msg = response.Message, color = ProjectVariables.SuccessColor });
            }
        }

        public async Task<IActionResult> UsersForRequestedService(string requestServiceId = "", string msg = "", string color = "")
        {
            var user = await _helper.ValidateUserAsync();
            if (user == null)
            {
                return RedirectToAction("UserLogin", "Auth");
            }
            if (user.Role != "Valet")
            {
                var response = await _userService.GetRequestServiceById(requestServiceId, user.Token);
                if (response.StatusCode == "200" && response.Status == true)
                {
                    var requestedService = JsonConvert.DeserializeObject<RequestServicesDto>(response.Data.ToString());
                    ViewBag.RequestedService = requestedService;
                }
                return View();
            }
            else
            {
                return RedirectToAction("Error", "Home", new { msg = "You are not authorized to access this page", color = ProjectVariables.DangerColor });
            }
        }

        public async Task<IActionResult> ManageAppointments()
        {
            var loggendInUser = await _helper.ValidateUserAsync();
            if (loggendInUser == null)
            {
                return RedirectToAction("UserLogin", "Auth");
            }
            ViewBag.User = loggendInUser;
            return View();
        }

        #endregion

        #region Orders
        public async Task<IActionResult> ManageOrders()
        {
            var loggendInUser = await _helper.ValidateUserAsync();
            if (loggendInUser == null)
            {
                return RedirectToAction("UserLogin", "Auth");
            }
            ViewBag.User = loggendInUser;
            return View();
        }

        public async Task<IActionResult> OrderDetail(string? orderId = "")
        {
            var loggendInUser = await _helper.ValidateUserAsync();
            if (loggendInUser == null)
            {
                return RedirectToAction("Index", "Home", new { msg = "Session Expired, Logged In Again", color = ProjectVariables.DangerColor });
            }
            var response = await _userService.GetOrderById(orderId, loggendInUser.Token);
            if (response.StatusCode == "200" && response.Status == true)
            {
                var updateUserRecord = JsonConvert.DeserializeObject<OrderDtoList>(response.Data.ToString());
                ViewBag.UserRecord = updateUserRecord;
            }
            ViewBag.User = loggendInUser;
            return View();
        }

        #endregion

        #region UserProfile

        public async Task<IActionResult>ViewUserProfile(string Id = "", int preview = -1)
        {
            var loggedInUser = await _helper.ValidateUserAsync();
            if (loggedInUser == null)
            {
                return RedirectToAction("UserLogin", "Auth");
            }
            if (!string.IsNullOrEmpty(Id))
            {
                ViewBag.loggedinUser = loggedInUser;
                var userResponse = await _adminService.GetUserByIdEncId(Id, loggedInUser.Token);

                if (userResponse.StatusCode == "200" && userResponse.Status==true)
                {
                    var updateUserRecord = JsonConvert.DeserializeObject<UserListDto>(userResponse.Data.ToString());
                    ViewBag.UserRecord = updateUserRecord;

                    var userSkillsResponse = await _userService.GetUserSkillsByUserId(updateUserRecord.Id, loggedInUser.Token);

                    if (userSkillsResponse.StatusCode == "200" && userSkillsResponse.Status == true)
                    {
                        var userSkillsList = JsonConvert.DeserializeObject<List<UserSkillDto>>(userSkillsResponse.Data.ToString());
                        ViewBag.UserSkillsList = userSkillsList;
                    }
                }
            }

            ViewBag.preview = preview;
            return View();
        }

        public async Task<IActionResult> CheckoutPayment(CheckOutDTO Data)
        {
            var loggedInUser = await _helper.ValidateUserAsync();
            if (loggedInUser == null)
            {
                return RedirectToAction("UserLogin", "Auth");
            }
            var userResponse = await _adminService.GetUserById(loggedInUser.Id, loggedInUser.Token);

            if (userResponse.StatusCode == "200" && userResponse.Status==true)
            {
                var updateUserRecord = JsonConvert.DeserializeObject<UserListDto>(userResponse.Data.ToString());
                ViewBag.UserRecord = updateUserRecord;
            }

            var workingHours = (float)TimeSpan.FromTicks(Convert.ToDateTime(Data.ToDateTime).Subtract(Convert.ToDateTime(Data.FromDateTime)).Ticks).TotalHours;
            Data.wHours = workingHours.ToString("0.00");

            Data.workCharges = (25 * workingHours).ToString();
            ViewBag.PayableOrderCharges = Data.workCharges;

            Data.stripeFee = (Convert.ToDouble(Data.workCharges) * 0.04f).ToString(); // 4% stripe fee
            Data.workCharges += Data.stripeFee;

            ViewBag.PayableAmount = (Convert.ToDouble(Data.workCharges) * 100).ToString(); // Convert to cents for Stripe

            Data.platformFee = (Convert.ToDouble(Data.workCharges) * 0.13f).ToString(); // 13% platform fee

            return View(Data);
        }


        public async Task<IActionResult> ChargePayment(CheckOutDTO Data)
        {
            var loggedInUser = await _helper.ValidateUserAsync();

            var chargeResponse = await _userService.ChargeStripePayment(Data, loggedInUser.Token);

            if (chargeResponse.StatusCode != "200" ||  chargeResponse.Status!=true)
            {
                return RedirectToAction("Orders", "Seller", new { msg = "Something Went Wrong! Please Try Again!" });
            }

            return RedirectToAction("Orders", "Seller", new { msg = "Payment Successful!" });
        }


        #endregion
        
        private async Task<bool> PostLogins(UserClaims data, int way = -1)
        {
            var claims = AuthService.GenerateClaimsForUser(data);
            var claimsIdentity = new ClaimsIdentity(claims, "UserClaims");
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, new AuthenticationProperties()
            {
                IsPersistent = true
            });
            return true;
        }

        private MultipartFormDataContent BuildMultipartContent(IFormFile file)
        {
            byte[] data;
            using (var br = new BinaryReader(file.OpenReadStream()))
            {
                data = br.ReadBytes((int)file.OpenReadStream().Length);
            }
            MultipartFormDataContent multipartFormContent = new();
            multipartFormContent.Headers.ContentType.MediaType = "multipart/form-data";
            var byteArrayContent = new ByteArrayContent(data);
            multipartFormContent.Add(byteArrayContent, "file", file.FileName);
            return multipartFormContent;
        }


    }
}
