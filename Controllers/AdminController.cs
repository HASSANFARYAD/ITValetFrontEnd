using ITValetFrontEnd.ClientSideServices;
using ITValetFrontEnd.Filters;
using ITValetFrontEnd.HelperClasses;
using ITValetFrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

namespace ITValetFrontEnd.Controllers
{
    public class AdminController : Controller
    {
        private readonly GeneralHelper _helper;
        private readonly IAdminService _adminService;

        public AdminController(GeneralHelper generalHelper, IAdminService adminService)
        {
            _helper = generalHelper;
            _adminService = adminService;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region Manage_Customers_And_ITValets 
        //Valet(4) and Customer(3) values for database insertion:
        public IActionResult AddNewUser(string msg = "", string color = "black", int valetOrCustomer = 0)
        {
            ViewBag.Message = msg;
            ViewBag.Color = color;
            ViewBag.ValetOrCustomer = valetOrCustomer;
            return View();
        }

        [HttpPost]
        [RoleBasedValidation(EnumRoles.Admin)]
        public async Task<IActionResult> PostNewUser(PostAddUserDto userObj)
        {
            var user = await _helper.ValidateUserAsync();
            if (user != null)
            {
                var response = await _adminService.PostAddUser(userObj, user.Token);
                if (response.StatusCode == "200" && response.Status == true)
                {
                    return RedirectToAction("AddNewUser", "Admin", new { msg = response.Message, color = "green" });
                }
                else if (response.StatusCode == "400" && response.Status == false)
                {
                    return RedirectToAction("AddNewUser", "Admin", new { msg = response.Message, color = "red" });
                }
                else
                {
                    return RedirectToAction("AddNewUser", "Admin", new { msg = "Something went wrong", color = "red" });
                }
            }
            return RedirectToAction("Login", "Auth");
        }

        [RoleBasedValidation(EnumRoles.Admin)]
        public IActionResult ViewUser(string msg = "", string color = "black", int valetOrCustomer = 0)
        {
            ViewBag.Message = msg;
            ViewBag.Color = color;
            ViewBag.ValetOrCustomer = valetOrCustomer;
            return View();
        }

        public IActionResult UserUnderReviews(string msg = "", string color = "black", int valetOrCustomer = 0)
        {
            ViewBag.Message = msg;
            ViewBag.Color = color;
            ViewBag.ValetOrCustomer = valetOrCustomer;
            return View();
        }
        #endregion

        #region ContactUs_FeedBackRecord

        [RoleBasedValidation(EnumRoles.Admin)]
        public IActionResult FeedbackRecord(string msg = "", string color = "black")
        {
            ViewBag.Message = msg;
            ViewBag.Color = color;
            return View();
        }
        #endregion

        #region UserSubscription_Record
        public IActionResult UserSubscriptionRecord(string msg = "", string color = "black")
        {
            ViewBag.Message = msg;
            ViewBag.Color = color;
            return View();
        }
        #endregion

        #region UpdateUserStatus
        [HttpPost]
        public async Task<IActionResult> UpdateUserStatus(int role, string EncId, string status = "")
        {
            var user = await _helper.ValidateUserAsync();
            if (user != null)
            {
                if(status == "Deleted")
                {
                    var response = await _adminService.UpdateUserStatus(role, EncId, user.Token, 0);
                    if (response.Status == true)
                    {
                        return RedirectToAction("ViewUser", "Admin", new { msg = response.Message, color = "green", valetOrCustomer = role });
                    }
                    else
                    {
                        return RedirectToAction("ViewUser", "Admin", new { msg = response.Message, color = "red", valetOrCustomer = role });
                    }
                }

            }
            return RedirectToAction("Login", "Auth");

        }
        #endregion
    }
}
