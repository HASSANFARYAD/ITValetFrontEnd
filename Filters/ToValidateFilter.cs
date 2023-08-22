using ITValetFrontEnd.HelperClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace ITValetFrontEnd.Filters
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public class RoleBasedValidationAttribute : Attribute, IActionFilter
    {
        private readonly EnumRoles _role;

        public RoleBasedValidationAttribute(EnumRoles role)
        {
            _role = role;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var helper = (GeneralHelper)context.HttpContext.RequestServices.GetService(typeof(GeneralHelper));

            var loggedInUser = helper.ValidateUserAsync().GetAwaiter().GetResult();
            if (loggedInUser == null)
            {
                // User is not logged in, redirect to the login page
                context.Result = new RedirectToRouteResult(new { controller = "Auth", action = "Login" });
            }
            else
            {
                var userRole = (EnumRoles)Enum.Parse(typeof(EnumRoles), loggedInUser.Role);
                if (_role != userRole)
                {
                    // User is logged in but does not have the required role, redirect to unauthorized page
                    context.Result = new RedirectToRouteResult(new { controller = "Error", action = "NotFound" });
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // This method is not implemented as per your code
        }
    }
}
