using Core.Interfaces.Services;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ArasPLMWebAp.Controllers
{
    public class BaseController: Controller
    {
        protected IUserService _userService { get; }
        protected UserDTO _user { get; set; }

        public BaseController(IUserService userService)
        {
            _userService = userService;
        }

        public override void OnActionExecuting(ActionExecutingContext ctx)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                _user = HttpContext.GetUserDTO();
                if (_user == null)
                {
                    _user = _userService.GetCurrentUser();
                    HttpContext.AddUserDTO(_user);

                    if (_user != null)
                    {
                        HttpContext.Session.SetString("UserId", _user.Id.ToString());
                        HttpContext.Session.SetString("UserName", _user.UserName);
                        HttpContext.Session.SetString("AcceptedFlag", _user.Accepted);
                        HttpContext.Session.SetString("Language", _user.Language.ToString());
                        HttpContext.Session.SetString("UserRole", _user.Role.ToString());
                        HttpContext.Session.SetString("FirstName", _user.FirstName.ToString());
                        HttpContext.Session.SetString("LastName", _user.LastName.ToString());
                        //HttpContext.Session.SetString("GenericID", _user.GenericID);

                    }
                   
                }
            }
            base.OnActionExecuting(ctx);
        }

        protected ActionResult HandleRedirect(bool isSuccess, string redirectUrl)
        {
            ActionResult result = BadRequest();

            if (isSuccess)
            {
                if (string.IsNullOrEmpty(redirectUrl))
                {
                    result = Ok();
                }
                else
                {
                    result = Redirect(redirectUrl);
                }
            }

            return result;
        }
    }
}
