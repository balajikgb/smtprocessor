using Core.Interfaces.Repositories;
using Core.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ArasPLMWebAp.Models;
using System.Security.Claims;
using PriceUpdateRepository;
using Core.Enums;
using DocumentFormat.OpenXml.Spreadsheet;
using ProcessorWebApp.Resources;

namespace ArasPLMWebAp.Controllers
{
    public class HomeController : Controller
    {

        protected Context _context { get; }
        private readonly IConfiguration _config;
        private readonly TokenService _tokenService;
        private readonly IUserRepository _userRepository;
        private string generatedToken = null;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IConfiguration config, TokenService tokenService, IUserRepository userRepository, Context context)
        {
            _logger = logger;
            _tokenService = tokenService;
            _userRepository = userRepository;
            _config = config;
            _context = context;

            
           
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return RedirectToAction("OrderTracker", "Orders");
        }
        //Commented to have possibility turn on in future if required
        //[AllowAnonymous]
        //public IActionResult Index()
        //{
        //    if (!HttpContext.User.Identity.IsAuthenticated)
        //    {
        //        return View();
        //    }
        //    ClaimsIdentity claims = HttpContext.User.Identity as ClaimsIdentity;
        //    if (_userRepository.GetUser(AppAuthenticationHelper.GetUsername(HttpContext)) != null)
        //    {
        //        if (claims == null)
        //        {
        //            return RedirectToAction("RegistrationCompleted", "UsersRegistration");
        //        }
        //        _userRepository.AddNewRegistration(new Core.DataModels.UserRegistration()
        //        {
        //            UserEmail = AppAuthenticationHelper.GetUserDTO(HttpContext).UserName,
        //            FirstName = AppAuthenticationHelper.GetUserDTO(HttpContext).FirstName,
        //            LastName = AppAuthenticationHelper.GetUserDTO(HttpContext).LastName,
        //            Active = true
        //        });
        //        return RedirectToAction("RegistrationCompleted", "UsersRegistration");
        //    }
        //    else
        //    {
        //        if (claims != null)
        //        {
        //            if (AppAuthenticationHelper.GetUserBaans(HttpContext) == null || AppAuthenticationHelper.GetUserCompanies(HttpContext) == null)
        //            {
        //                return RedirectToAction("UserWithOutGrants");
        //            }
        //            return RedirectToAction("OrderTracker", "Orders");
        //        }
        //        return RedirectToAction("RegistrationCompleted", "UsersRegistration");
        //    }
        //}
        //[AllowAnonymous]
        //[Route("login")]
        //[HttpPost]
        //public IActionResult Login(UserModel userModel)
        //{
        //    if (string.IsNullOrEmpty(userModel.UserEmail) || string.IsNullOrEmpty(userModel.Password))
        //    {
        //        return (RedirectToAction("Error"));
        //    }
        //    IActionResult response = Unauthorized();
        //    var validUser = GetUser(userModel);

        //    if (validUser != null)
        //    {
        //        generatedToken = _tokenService.BuildToken(_config["Jwt:Key"].ToString(), _config["Jwt:Issuer"].ToString(), _config["Jwt:Audience"].ToString(), validUser);
        //        if (generatedToken != null)
        //        {
        //            HttpContext.Session.SetString("Token", generatedToken);
        //            ViewBag.UserRole = validUser.Role;
        //            return RedirectToAction("OrderTracker", "Orders");
        //        }
        //        else
        //        {
        //            return (RedirectToAction("Error"));
        //        }
        //    }
        //    else
        //    {
        //        return (RedirectToAction("Error"));
        //    }
        //}

        public IActionResult UserWithOutGrants()
        {
            return View("UserWithOutGrants");
        }
        public IActionResult UserNotAuthorized()
        {
            if (HttpContext.Session.GetString("UserName") != null)
            {
                var users = _context.Users.Where(u => u.UserName == HttpContext.Session.GetString("UserName")).FirstOrDefault();
                if (users == null)
                {
                    HttpContext.Session.SetString("UserName", "");
                }
                else
                {
                    HttpContext.Session.SetString("UserName", users.UserName);
                }
            }
            else
            {
                HttpContext.Session.SetString("UserName", "");
            }
            UserDTO userrole = (from u in _context.Users
                                where u.UserName.ToLower() == HttpContext.Session.GetString("UserName").ToString().ToLower() ||
                                u.Email.ToLower() == HttpContext.Session.GetString("UserName").ToString().ToLower()
                                select new UserDTO
                                {
                                    Role = u.UserRole
                                }).FirstOrDefault();
            string userrolename = userrole.Role.ToString();
            ViewBag.Userrole = userrolename;
            return View("UserNotAuthorized");
        }

        public ActionResult ErrorPage()
        {
            return View("ErrorPage");
        }

        [Route("/signout-oidc")]
        public IActionResult SignoutOidc()
        {
            return RedirectToAction("AzureADAuth");
        }

       // [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
       // [HttpGet]
       // public IActionResult AzureADAuth()
      //  {
       //     return RedirectToAction("OrderTracker", "Orders");
       // }

        [Authorize]
        [HttpGet]
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Index));
        }

        private UserDTO GetUser(UserModel userModel)
        {
            // Write your code here to authenticate the user     
            return _userRepository.GetUserWithPassword(userModel);
        }

        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
