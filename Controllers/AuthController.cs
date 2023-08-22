using ITValetFrontEnd.ClientSideServices;
using ITValetFrontEnd.Filters;
using ITValetFrontEnd.HelperClasses;
using ITValetFrontEnd.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Common;
using System.Data;
using System.Security.Claims;

namespace ITValetFrontEnd.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly GeneralHelper _helper;
        private readonly IAdminService _adminService;

        public AuthController(IAuthService authService, GeneralHelper generalHelper, IAdminService adminService)
        {
            _authService = authService;
            _helper = generalHelper;
            _adminService = adminService;
        }
        public IActionResult Index()
        {
            return View();
        }

        #region Login
        public IActionResult Login(string msg = "", string color = "black")
        {
            ViewBag.Message = msg;
            ViewBag.Color = color;
            return View();
        }

        public IActionResult UserLogin(string msg = "", string color = "black")
        {
            ViewBag.Message = msg;
            ViewBag.Color = color;
            return View();
        }

        [HttpPost("login")]
        public async Task<ActionResult> PostLogin(LoginDto loginDto, string way = "")
        {
            var responseDto = await _authService.Login(loginDto);

            if (responseDto.StatusCode == "200" && responseDto.Status == true)
            {
                var userObj = JsonConvert.DeserializeObject<UserClaims>(responseDto.Data.ToString());
                // Successful login
                var claims = AuthService.GenerateClaimsForUser(userObj);
                var claimsIdentity = new ClaimsIdentity(claims, "UserClaims");
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, new AuthenticationProperties()
                {
                    IsPersistent = true
                });
                if (way == "Admin")
                {
                    return RedirectToAction("Dashboard", "Admin");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                // Handle error cases based on the responseDto properties
                if (way == "Admin")
                {
                    return RedirectToAction("Login", "Auth", new { msg = responseDto.Message, color = "red" });
                }
                else
                {
                    return RedirectToAction("UserLogin", "Auth", new { msg = responseDto.Message, color = "red" });
                }
            }
        }

        #endregion

        #region ForgotPassword
        public async Task<IActionResult> ForgotPassword()
        {
            return View();
        }
        #endregion

        #region Registeration
        public async Task<IActionResult> Register(string value = "")
        {
            var loggedInUser = await _helper.ValidateUserAsync();
            if (loggedInUser == null)
            {
                ViewBag.UserType = value;
                return View();
            }
            else
            {
                return RedirectToAction("Account", "User");
            }
        }

        public async Task<IActionResult> PostRegister(PostAddUserDto user)
        {
            var responseDto = await _authService.Register(user);
            if(responseDto.StatusCode == "200" && responseDto.Status == true)
            {
                return RedirectToAction("UserLogin", "Auth", new { msg = "Your account has been created, please login to access your account" });
            }
            return View();
        }
        #endregion

        public IActionResult ResetPassword()
        {
            return View();
        }

        #region UpdateProfile
        public async Task<IActionResult> UpdateProfile(string msg = "", string color = "black")
        {
            var user = await _helper.ValidateUserAsync();
            if (user != null)
            {
                var response = await _adminService.GetUserById(user.Id, user.Token);
                if (response.StatusCode == "200" && response.Status == true)
                {
                    var updateUserRecord = JsonConvert.DeserializeObject<UserClaims>(response.Data.ToString());
                    ViewBag.UserRecord = updateUserRecord;
                }
            }
            return View();
        }

        [HttpPost]
        [RoleBasedValidation(EnumRoles.Admin)]
        public async Task<IActionResult> PostUpdateProfile(PostUpdateUserDto updateUser)
        {
            var user = await _helper.ValidateUserAsync();
            if (user != null)
            {
                var response = await _authService.UpdateProfile(updateUser, user.Token);

                if (response.StatusCode == "200" && response.Status == true)
                {
                    return RedirectToAction("UpdateProfile", "Auth", new { msg = response.Message, color = "green" });
                }
                else if (response.StatusCode == "400" && response.Status == false)
                {
                    return RedirectToAction("UpdateProfile", "Auth", new { msg = response.Message, color = "red" });
                }
                else
                {
                    return RedirectToAction("UpdateProfile", "Auth", new { msg = "Something went wrong", color = "red" });
                }
            }
            return RedirectToAction("Login", "Auth");
        }




        #endregion

        public IActionResult UpdatePassword(string msg = "", string color = "black")
        {
            return View();
        }

        [HttpPost]
        [RoleBasedValidation(EnumRoles.Admin)]
        public async Task<IActionResult> PostUpdatePassword(UpdatePasswordDto updatePassword)
        {
            var user = await _helper.ValidateUserAsync();
            if (user != null)
            {
                var response = await _authService.UpdatePassword(updatePassword, user.Token);

                if (response.StatusCode == "200" && response.Status == true)
                {
                    return RedirectToAction("UpdatePassword", "Auth", new { msg = response.Message, color = "green" });
                }
                else if (response.StatusCode == "404" && response.Status == false)
                {
                    return RedirectToAction("UpdatePassword", "Auth", new { msg = response.Message, color = "red" });
                }
                else
                {
                    return RedirectToAction("UpdatePassword", "Auth", new { msg = "Something went wrong", color = "red" });
                }
            }
            return RedirectToAction("Login", "Auth");
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Auth");
        }
    }
}
