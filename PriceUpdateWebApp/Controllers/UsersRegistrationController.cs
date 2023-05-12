using Core.Interfaces.Services;
using Core.Interfaces.Repositories;
using Core.DataModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ArasPLMWebAp.Models;
using Core.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Core.RepositoryModels;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Core.Services;

namespace ArasPLMWebAp.Controllers
{
    [Authorize]
    public class UsersRegistrationController : BaseController
    {
        protected IUserRepository _userRepository;

        public UsersRegistrationController(IUserRepository userRepository, IUserService userService) : base(userService)
        {
            _userRepository = userRepository;
        }

        public ActionResult Index()
        {
            return RedirectToAction("RegUser");
        }

        public ActionResult RegUser()
        {
            if (HttpContext.IsInCustomRole())
            {
                return RedirectToAction("OrderTracker", "Orders");
            }
            if (User.Identity.IsAuthenticated && _user != null
                && (_user.Role == null))
            {
                return RedirectToAction("RegistrationCompleted", "UsersRegistration");
            }
            var regUserModel = new UserRegistration();
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                regUserModel.UserEmail = _userService.GetCurrentUsername();
            }
            return View(regUserModel);
        }
        public ActionResult TmpRegUser()
        {
            var regUserModel = new UserRegistration();
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                regUserModel.UserEmail = _userService.GetCurrentUsername();
            }
            return View("RegUser",regUserModel);
        }
        public ActionResult RegistrationCompleted()
        {
            if (User.Identity.IsAuthenticated && _user == null)
            {
                return RedirectToAction("UserNotAuthorized", "Home");
            }
            if (User.Identity.IsAuthenticated && _user != null
                && _user.IsAccessForbidden)
            {
                return RedirectToAction("AccountIsBlocked", "UsersRegistration");
            }
            if (HttpContext.IsInCustomRole())
            {
                return RedirectToAction("OrderTracker", "Orders");
            }
            return View("RegistrationCompleted");
        }
        public ActionResult AccountIsBlocked()
        {
            return View();
        }
        // POST: UsersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegUser([Bind("FirstName, LastName, CompanyName, CompanyAddres," +
            " UserEmail, Market, UserType, Comment, Password, ApprovePassword")] UserRegistration user)
        {
            if (HttpContext.IsInCustomRole())
            {
                return RedirectToAction("OrderTracker", "Orders");
            }
            if (HttpContext.User.Identity.IsAuthenticated && !AppAuthenticationHelper.IsJWTSignInProvider(HttpContext.User))
            {
                ModelState.Remove("Password");
                ModelState.Remove("ApprovePassword");
            }
            if (ModelState.IsValid)
            {
                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    user.UserEmail = _userService.GetCurrentUsername();
                }
                _userRepository.AddNewRegistration(user);
                return RedirectToAction("RegistrationCompleted");
            }
            return View(user);
        }
    }
}
