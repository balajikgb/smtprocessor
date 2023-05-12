using Core.DataModels;
using Core.Interfaces.Services;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PriceUpdateRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ArasPLMWebAp.Models;
using ArasPLMWebAp.Models.UserManagement;
using PriceUpdateWebApp.Services;
using PriceUpdateRepository.DataModels;
using ProcessorWebApp.Resources;

namespace ArasPLMWebAp.Controllers
{
    [Authorize]
    public class UserManagementController : BaseController
    {
        Access objAccess = new Access();
        protected Context _context { get; }
        protected IUsersAdminServises UserAdminService { get; }
        protected IPasswordHasher<PriceUpdateRepository.DataModels.UserModel> _passwordHasher { get; }

        public UserManagementController(IUserService userService, Context context, IUsersAdminServises usersAdminServises,  IPasswordHasher<PriceUpdateRepository.DataModels.UserModel> passwordHasher) : base(userService)
        {
            _context = context;
            UserAdminService = usersAdminServises;
            _passwordHasher = passwordHasher;
        }

        // GET: UserManagementController
        public ActionResult UsersAdmin(string environment)
        {
            if (HttpContext.User.Identity.IsAuthenticated && _user == null)
            {
                return RedirectToAction(OrderResx.UsersNotAuthorized, OrderResx.Home);
            }
            if (_user.IsAccessForbidden == true)
            {
                return RedirectToAction(OrderResx.UsersNotAuthorized, OrderResx.Home);
            }
            _user.Page = OrderResx.User;
            _user.Action = OrderResx.View;

            if (objAccess.UserAccess(_user) == false)
            {
                return RedirectToAction(OrderResx.UsersNotAuthorized, OrderResx.Home);
            }
            ViewBag.environment = objAccess.UserButtonAccess(_user);
            UserDTO userrole = (from u in _context.Users
                                where u.UserName.ToLower() == HttpContext.Session.GetString("UserName").ToString().ToLower() ||
                                u.Email.ToLower() == HttpContext.Session.GetString("UserName").ToString().ToLower()
                                select new UserDTO
                                {
                                    Role = u.UserRole
                                }).FirstOrDefault();
            string userrolename = userrole.Role.ToString();
            ViewBag.Userrole = userrolename;
            return View(OrderResx.UsersAdminView, ViewBag.environment);
        }
        public async Task<ActionResult> CreateUser()
        {
            if (HttpContext.User.Identity.IsAuthenticated && _user == null)
            {
                return RedirectToAction(OrderResx.UsersNotAuthorized, OrderResx.Home);
            }
            if (_user.IsAccessForbidden == true)
            {
                return RedirectToAction(OrderResx.UsersNotAuthorized, OrderResx.Home);
            }
            _user.Page = OrderResx.User;
            _user.Action = OrderResx.Create;

            if (objAccess.UserAccess(_user) == false)
            {
                return RedirectToAction(OrderResx.UsersNotAuthorized, OrderResx.Home);
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
            var CreateUser = new CreateUserViewModel() { 
            };
            return View(OrderResx.CreateUserView, CreateUser);
        }
        [HttpPost]
        public ActionResult CreateUser(CreateUserViewModel user)
        {
            if (HttpContext.User.Identity.IsAuthenticated && _user == null)
            {
                return RedirectToAction(OrderResx.UsersNotAuthorized, OrderResx.Home);
            }
            if (_user.IsAccessForbidden == true)
            {
                return RedirectToAction(OrderResx.UsersNotAuthorized, OrderResx.Home);
            }
            _user.Page = OrderResx.User;
            _user.Action = OrderResx.Create;
            if (objAccess.UserAccess(_user) == false)
            {
                return RedirectToAction(OrderResx.UsersNotAuthorized, OrderResx.Home);
            }
            var us = new PriceUpdateRepository.DataModels.UserModel();
            us.FirstName = user.FirstName;
            us.SecondName = user.LastName;
            us.Active = user.Active;
            us.Comment = user.Comment;
            us.Email = user.UserEmail;
            us.UserName = user.UserEmail;
            us.CreateDate = user.CreateDate;
            us.UserRole = user.UserRole;
            us.Language = user.Language;
            us.GenericID = user.GenericID;
            us.Accepted = OrderResx.NotAccepted;
            _context.Add(us);
            _context.SaveChanges();
            return RedirectToAction(OrderResx.UsersAdminView, OrderResx.UsersManagement);
        }
        [Route("/api/UsersManagement/GetUsers")]
        [HttpGet]
        public List<PriceUpdateRepository.DataModels.UserModel> GetUsers()
        {
            List<PriceUpdateRepository.DataModels.UserModel> data = new List<PriceUpdateRepository.DataModels.UserModel>();
            data = _context.Users.ToList();
            return data;
        }
        [HttpGet]
        public ActionResult<DataTableResponse> GetUsersAdmin()
        {
            List<UserDTO> data = UserAdminService.GetUsers();
            return new DataTableResponse
            {
                RecordsTotal = data.Count(),
                RecordsFiltered = 10,
                Data = data.ToArray()
            };
        }
        public bool EditUsername(string oldUsername, string newUsername)
        {
            if (_context.Users.Any(x => x.UserName == newUsername))
            {
                return false;
            }
            var user = _context.Users.FirstOrDefault(x => x.UserName == oldUsername);
            if (user != null)
            {
                user.Email = newUsername;
                user.UserName = newUsername;
            }
            _context.SaveChanges();
            return true;
        }
        [HttpGet]
        public IActionResult DeleteUser(string email)
        {
            if (HttpContext.User.Identity.IsAuthenticated && _user == null)
            {
                return RedirectToAction(OrderResx.UsersNotAuthorized, OrderResx.Home);
            }
            var delUser = _context.Users.FirstOrDefault(u => u.Email == email || u.UserName == email);
            if (delUser == null)
            {
                return NotFound();
            }
            _context.Users.Remove(delUser);
            _context.SaveChanges();
            return Ok();
        }
        // GET: UserManagementController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserManagementController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserManagementController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserManagementController/Edit/5
        public async Task<ActionResult> Edit(string username, string environment)
        {
            if (HttpContext.User.Identity.IsAuthenticated && _user == null)
            {
                return RedirectToAction(OrderResx.UsersNotAuthorized, OrderResx.Home);
            }
            if (_user.Role != Core.Enums.UserRoles.Admin && _user.UserName != username)
            {
                return Forbid();
            }
            _user.Page = OrderResx.User;
            _user.Action = OrderResx.EditBatch;

            if (objAccess.UserAccess(_user) == false)
            {
                return RedirectToAction(OrderResx.UsersNotAuthorized, OrderResx.Home);
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
            var edit = _context.Users.FirstOrDefault(x => x.UserName == username);
            UserEdition editUser = new UserEdition
            {
                Id = edit.Id,
                Active = edit.Active,
                FirstName = edit.FirstName,
                LastName = edit.SecondName,
                UserEmail = edit.Email,
                Comment = edit.Comment,
                Password = edit.Password,
                UserName = edit.UserName,
                OldUserName = edit.UserName,
                CreateDate = edit.CreateDate,
                UserRole = edit.UserRole,
                Language = edit.Language,
                GenericID = edit.GenericID         
        };
            ViewBag.RetrievedBy = editUser.RetrievedBy;
            ViewBag.Matrix = editUser.Matrix;
            ViewBag.Product = editUser.Product;
            ViewBag.Options = editUser.Options;
            ViewBag.Environment=editUser.Environment;
            if (editUser == null)
            {
                return NotFound();
            }
            return View(OrderResx.EditBatch, editUser);
        }
        public bool CheckForaccessibilityToEditUserName(string newUsername)
        {
            if (_context.Users.Any(x => x.UserName == newUsername))
            {
                return false;
            }
            return true;
        }
        // POST: UserManagementController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserEdition user)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Edit), new { username = user.OldUserName });
            }
            var us = _context.Users.FirstOrDefault(u => u.UserName == user.OldUserName);          
            us.FirstName = user.FirstName;
            us.SecondName = user.LastName;
            us.Active = user.Active;
            us.Comment = user.Comment;
            us.Email = us.UserName == user.UserName ? user.UserEmail : user.UserName;
            us.UserName = user.UserName;
            us.CreateDate = user.CreateDate;
            us.UserRole = user.UserRole;
            us.Language = user.Language;
            us.GenericID = user.GenericID;
            _context.SaveChanges();
            if (_user.UserName == user.OldUserName)
            {
                _user.UserName = us.UserName;
            }
            return RedirectToAction(OrderResx.UsersAdminView, OrderResx.UsersManagement);
        }
        [Route("/api/PriceUpdate/EditUserDetails")]
        [HttpPost]
        public ActionResult EditUserDetails([FromBody] List<UserEdition> userdetails)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Edit), new { username = userdetails[0].OldUserName });
            }
            var us = _context.Users.FirstOrDefault(u => u.UserName == userdetails[0].OldUserName);
            us.FirstName = userdetails[0].FirstName;
            us.SecondName = userdetails[0].LastName;
            us.Comment = userdetails[0].Comment;
            us.Email = us.UserName == userdetails[0].UserName ? userdetails[0].UserEmail : userdetails[0].UserName;
            us.UserName = userdetails[0].UserName;
            us.UserRole = userdetails[0].UserRole;
            us.Language = userdetails[0].Language;
            us.GenericID = userdetails[0].GenericID;
            _context.SaveChanges();
            if (_user.UserName == userdetails[0].OldUserName)
            {
                _user.UserName = us.UserName;
            }
            return RedirectToAction(OrderResx.UsersAdminView, OrderResx.UsersManagement);
        }
        public ActionResult SavePassword(string username, string password)
        {
            var us = _context.Users.FirstOrDefault(x => x.UserName == username);
            us.Password = _passwordHasher.HashPassword(us, password);
            _context.SaveChanges();
            return Ok();
        }
        // GET: UserManagementController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserManagementController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ToggleUserIsForbiddenAccess(ToggleUserIsForbiddenAccessRequest requestModel)
        {
            bool isSuccess = UserAdminService.ToggleUserIsForbiddenAccess(requestModel.UserId, requestModel.IsActionForbidAccess);

            return HandleRedirect(isSuccess, requestModel.RedirectUrl);
        }
    }
}
