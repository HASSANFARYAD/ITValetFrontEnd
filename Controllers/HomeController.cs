using ITValetFrontEnd.HelperClasses;
using ITValetFrontEnd.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Net.Http.Headers;

namespace ITValetFrontEnd.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly GeneralHelper _helper;
        HttpClient _httpClient = new HttpClient();
        private readonly string _baseUrl;
        public HomeController(ILogger<HomeController> logger, GeneralHelper helper)
        {
            _logger = logger;
            _helper = helper;
            _baseUrl = ProjectVariables.BaseUrl;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<IActionResult> Messages(string? Way, string? UserId)
        {
            var getLoggedInUserId = await _helper.ValidateUserAsync();
            if (getLoggedInUserId == null)
            {
                return RedirectToAction("UserLogin", "Auth");
            }
            ViewBag.LoggedInUserId = getLoggedInUserId;
            ViewBag.Way = string.IsNullOrEmpty(Way) ? "" : Way;
            ViewBag.UserId = string.IsNullOrEmpty(UserId) ? "" : UserId;
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        public async Task<ActionResult> SystemPackages()
        {
            var user = await _helper.ValidateUserAsync();
            if (user == null)
            {
                return RedirectToAction("UserLogin", "Auth", new { msg = "Session Expired, Logged In Again", color = ProjectVariables.DangerColor });
            }
            if (user != null)
            {
                string idAsString = user.Id.ToString();
                string apiEndpoint = $"User/GetUserSessionStatus?customerId={idAsString}";

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(user.Token);
                var response = await _httpClient.GetAsync(_baseUrl + apiEndpoint);
                var responseObj = await response.Content.ReadFromJsonAsync<ResponseDto>();

                if (responseObj.Status == true)
                {
                    ViewBag.IsSession = true;
                }
                else
                {
                    ViewBag.IsSession = false;
                }
                ViewBag.UserName = user.UserName;
                ViewBag.Id = user.Id;
            }
            return View();

        }

        /*  public async Task<IActionResult> PostFeedback(PostAddContact contact)
          {
              HttpClient httpClient = new HttpClient();
              httpClient.BaseAddress = new Uri(ProjectVariables.BaseUrl);
              string jsonContact = JsonConvert.SerializeObject(contact);
              StringContent content = new StringContent(jsonContact, Encoding.UTF8, "application/json");
              string endpoint = "Contact/PostAddContact";
              using (var Response = await httpClient.PostAsync(endpoint, content))
              {
                  if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                  {
                      var response = await Response.Content.ReadFromJsonAsync<ResponseDto>();
                      if(response != null && response.Status == false)
                      {
                          return RedirectToAction("Index", new { msg = response.Message});
                      }
                      return RedirectToAction("Index", new { msg = response.Message});
                  }
                  else if (Response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                  {
                      var errorContent = await Response.Content.ReadAsStringAsync();
                      var errorDto = JsonConvert.DeserializeObject<ResponseDto>(errorContent);

                      if (errorDto != null)
                      {
                          return RedirectToAction("Error", new { errorMsg = errorDto.Message });
                      }
                  }
              }
              return RedirectToAction("Contact", new { msg = "Cannot procced your request at the moment... Please try again Later" });
          }
  */
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}