using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ArasPLMWebAp.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;
using PriceUpdateRepository.DataModels;
using PriceUpdate.Services;
using System.Web.Http;
using Core.Services;
using PriceUpdateRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Data.SqlClient;
using System.Net;
using System.Net.Http.Formatting;
using Newtonsoft.Json;
using System.Threading.Tasks;
using static Azure.Core.HttpHeader;
using Newtonsoft.Json.Linq;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using System.Xml.Linq;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Core.Enums;
using Core.Interfaces.Services;
using System.Drawing.Drawing2D;
using Microsoft.Extensions.Options;
using ProcessorWebApp.Resources;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Hosting;
using DocumentFormat.OpenXml.Spreadsheet;
using Org.BouncyCastle.Utilities;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Reflection;
using DocumentFormat.OpenXml;
using System.Text;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using DocumentFormat.OpenXml.ExtendedProperties;
using MimeKit;
using Microsoft.AspNetCore.Identity;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml.Office2010.Word.Drawing;
using System.Security.Claims;
namespace ArasPLMWebAp.Controllers
{
    [Authorize]
    public class ProcessorController : Controller
    {
        private readonly Context _context;
        private readonly IWebHostEnvironment _env;
        EmailSettings _emailSettings = null;
        private readonly IConfiguration _configuration;
        protected UserDTO _user { get; set; }
        protected IUserService _userService { get; }
        //private readonly ExternalDbContext _externalcontext;

        ProcessorAppService objAppService = new ProcessorAppService();
        public ProcessorController(Context context, IOptions<EmailSettings> options, IWebHostEnvironment env, IConfiguration configuration)
        {
            var emailid = "";
            this._context = context;
            _emailSettings = options.Value;
            _env = env;
            _configuration = configuration;           
        }
        #region Process
        public ActionResult Process(string environment)
        {
            var emailid = "";
            var username = User.Identities.ToList();
            foreach (var name in username)
            {
                emailid = name.Name;
            }
            var emailidcheck = _context.Users.Where(u => u.Email.Trim().ToUpper() == emailid.Trim().ToUpper()).FirstOrDefault();
            if (emailidcheck != null)
            {
                HttpContext.Session.SetString("UserId", emailidcheck.Id.ToString());
                HttpContext.Session.SetString("UserName", emailidcheck.UserName);
                HttpContext.Session.SetString("AcceptedFlag", emailidcheck.Accepted);
                HttpContext.Session.SetString("Language", emailidcheck.Language.ToString());
                HttpContext.Session.SetString("UserRole", emailidcheck.UserRole.ToString());
                HttpContext.Session.SetString("FirstName", emailidcheck.FirstName.ToString());
                HttpContext.Session.SetString("LastName", emailidcheck.SecondName.ToString());
            }
            return View(OrderResx.ProcessTitle, ViewBag.environment);
        }
        public ActionResult CreateProcess()
        {
            int UserId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            string UserRole = HttpContext.Session.GetString("UserRole");
            var CreateProcess = new CreateProcessClass()
            {

            };
            if (UserRole != OrderResx.Admin)
            {
                return RedirectToAction("UserNotAuthorized", "Home");
            }
            return View(OrderResx.CreateProcessTitle, CreateProcess);
        }
        //Get Run Control by Process
        [Route("/api/Processor/GetRunControlbyProcess")]
        [HttpGet]
        public ActionResult<DataTableResponse> GetRunControlbyProcess()
        {
            List<CreateProcessClass> data = objAppService.GetRunControlbyProcess(_context);
            return new DataTableResponse
            {
                RecordsTotal = data.Count(),
                RecordsFiltered = 10,
                Data = data.ToArray()
            };
        }
        //Get Process Data
        [Route("/api/Processor/GetProcessData")]
        [HttpGet]
        public ActionResult<DataTableResponse> GetProcessData()
        {
            List<CreateProcessClass> data = objAppService.GetProcessData(_context);
            return new DataTableResponse
            {
                RecordsTotal = data.Count(),
                RecordsFiltered = 10,
                Data = data.ToArray()
            };
        }       
        //Get Process Record
        [Route("/api/Processor/GetProcessRecord")]
        [HttpGet]
        public ActionResult<DataTableResponse> GetProcessRecord(int Id)
        {
            List<CreateProcessClass> data = objAppService.GetProcessRecord(_context, Id);

            return new DataTableResponse
            {
                RecordsTotal = data.Count(),
                RecordsFiltered = 10,
                Data = data.ToArray()
            };
        }
        //Save Process
        [Route("/api/Processor/SaveProcess")]
        [HttpPost]
        public List<CreateProcessClass> SaveProcess([FromBody] List<CreateProcessClass> process)
        {
            List<CreateProcessClass> objlist = new List<CreateProcessClass>();
            string result = "";
            try
            {
                result = objAppService.SaveProcess(_context, process);
                CreateProcessClass listobjlist = new CreateProcessClass();
                listobjlist.Result = result;
                objlist.Add(listobjlist);

            }
            catch (Exception ex)
            {
                CreateProcessClass listobjnew = new CreateProcessClass();
                listobjnew.Result = result;
                objlist.Add(listobjnew);
            }
            return objlist;

        }
        //Delete Process
        [Route("/api/Processor/DeleteProcess")]
        [HttpGet]
        public string DeleteProcess(int Id)
        {
            string result = "";
            try
            {
                result = objAppService.DeleteProcess(_context, Id);
            }
            catch (Exception ex)
            {

            }
            return result;
        }
        #endregion
        #region Process Group
        public ActionResult ProcessGroup(string environment)
        {
            return View(OrderResx.ProcessGroup, ViewBag.environment);
        }
        public ActionResult CreateProcessGroup()
        {
            int UserId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            string UserRole = HttpContext.Session.GetString("UserRole");
            var CreateProcessGroup = new CreateProcessGroupClass()
            {

            };
            if (UserRole != OrderResx.Admin)
            {
                return RedirectToAction("UserNotAuthorized", "Home");
            }
            return View(OrderResx.CreateProcessGroupTitle, CreateProcessGroup);
        }
        //Get Process Group Data
        [Route("/api/Processor/GetProcessGroupData")]
        [HttpGet]
        public ActionResult<DataTableResponse> GetProcessGroupData()
        {
            List<CreateProcessGroupClass> data = objAppService.GetProcessGroupData(_context);
            return new DataTableResponse
            {
                RecordsTotal = data.Count(),
                RecordsFiltered = 10,
                Data = data.ToArray()
            };
        }
        //Get Process Group Record
        [Route("/api/Processor/GetProcessGroupRecord")]
        [HttpGet]
        public ActionResult<DataTableResponse> GetProcessGroupRecord(int Id)
        {
            List<CreateProcessGroupClass> data = objAppService.GetProcessGroupRecord(_context, Id);

            return new DataTableResponse
            {
                RecordsTotal = data.Count(),
                RecordsFiltered = 10,
                Data = data.ToArray()
            };
        }
        //Save Process Group
        [Route("/api/Processor/SaveProcessGroup")]
        [HttpPost]
        public List<CreateProcessGroupClass> SaveProcessGroup([FromBody] List<CreateProcessGroupClass> processgroup)
        {
            List<CreateProcessGroupClass> objlist = new List<CreateProcessGroupClass>();
            string result = "";
            try
            {
                result = objAppService.SaveProcessGroup(_context, processgroup);
                CreateProcessGroupClass listobjlist = new CreateProcessGroupClass();
                listobjlist.Result = result;
                objlist.Add(listobjlist);

            }
            catch (Exception ex)
            {
                CreateProcessGroupClass listobjnew = new CreateProcessGroupClass();
                listobjnew.Result = result;
                objlist.Add(listobjnew);
            }
            return objlist;

        }
        //Delete Process Group
        [Route("/api/Processor/DeleteProcessGroup")]
        [HttpGet]
        public string DeleteProcessGroup(int Id)
        {
            string result = "";
            try
            {
                result = objAppService.DeleteProcessGroup(_context, Id);
            }
            catch (Exception ex)
            {

            }
            return result;
        }
        #endregion
        #region Process Group Items
        //Get Process Data Active
        [Route("/api/Processor/GetProcessDataActive")]
        [HttpGet]
        public ActionResult<DataTableResponse> GetProcessDataActive()
        {
            List<CreateProcessClass> data = objAppService.GetProcessDataActive(_context);
            return new DataTableResponse
            {
                RecordsTotal = data.Count(),
                RecordsFiltered = 10,
                Data = data.ToArray()
            };
        }
        //Get Process Data by Process GroupId
        [Route("/api/Processor/ProcessDatabyProcessGroupId")]
        [HttpGet]
        public ActionResult<DataTableResponse> ProcessDatabyProcessGroupId(int Id)
        {
            List<CreateProcessGroupItemsClass> data = objAppService.ProcessDatabyProcessGroupId(_context, Id);
            return new DataTableResponse
            {
                RecordsTotal = data.Count(),
                RecordsFiltered = 10,
                Data = data.ToArray()
            };
        }
        //Save Process Group Items
        [Route("/api/Processor/SaveProcessGroupItems")]
        [HttpPost]
        public List<CreateProcessGroupItemsClass> SaveProcessGroupItems([FromBody] List<CreateProcessGroupItemsClass> groupitems)
        {
            List<CreateProcessGroupItemsClass> objlist = new List<CreateProcessGroupItemsClass>();
            string result = "";
            try
            {
                result = objAppService.SaveProcessGroupItems(_context, groupitems);
                CreateProcessGroupItemsClass listobjlist = new CreateProcessGroupItemsClass();
                listobjlist.Result = result;
                objlist.Add(listobjlist);

            }
            catch (Exception ex)
            {
                CreateProcessGroupItemsClass listobjnew = new CreateProcessGroupItemsClass();
                listobjnew.Result = result;
                objlist.Add(listobjnew);
            }
            return objlist;

        }
        //Delete Process Group Items
        [Route("/api/Processor/DeleteProcessGroupItems")]
        [HttpGet]
        public string DeleteProcessGroupItems(int Id)
        {
            string result = "";
            try
            {
                result = objAppService.DeleteProcessGroupItems(_context, Id);
            }
            catch (Exception ex)
            {

            }
            return result;
        }
        #endregion
        #region Run Control Page
        public ActionResult RunControlPage(string environment)
        {
            return View(OrderResx.RunControlPage, ViewBag.environment);
        }
        public ActionResult CreateRunControlPage()
        {
            int UserId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            string UserRole = HttpContext.Session.GetString("UserRole");
            var CreateRunControlPage = new CreateRunControlPageClass()
            {

            };
            if (UserRole != OrderResx.Admin)
            {
                return RedirectToAction("UserNotAuthorized", "Home");
            }
            return View(OrderResx.CreateRunControlPageTitle, CreateRunControlPage);
        }
        //Get Process Group Data Active
        [Route("/api/Processor/GetProcessGroupDataActive")]
        [HttpGet]
        public ActionResult<DataTableResponse> GetProcessGroupDataActive()
        {
            List<CreateProcessGroupClass> data = objAppService.GetProcessGroupDataActive(_context);
            return new DataTableResponse
            {
                RecordsTotal = data.Count(),
                RecordsFiltered = 10,
                Data = data.ToArray()
            };
        }
        //Get Process Data By Process Group Id
        [Route("/api/Processor/GetProcessDataByProcessGroupId")]
        [HttpGet]
        public ActionResult<DataTableResponse> GetProcessDataByProcessGroupId(int Id)
        {
            List<CreateProcessClass> data = objAppService.GetProcessDataByProcessGroupId(_context, Id);
            return new DataTableResponse
            {
                RecordsTotal = data.Count(),
                RecordsFiltered = 10,
                Data = data.ToArray()
            };
        }
        //Get Server Data Active
        [Route("/api/Processor/GetServersDataActive")]
        [HttpGet]
        public ActionResult<DataTableResponse> GetServersDataActive()
        {
            List<CreateServerClass> data = objAppService.GetServerDataActive(_context);
            return new DataTableResponse
            {
                RecordsTotal = data.Count(),
                RecordsFiltered = 10,
                Data = data.ToArray()
            };
        }
        //Get Run Control Page Data
        [Route("/api/Processor/GetRunControlPageData")]
        [HttpGet]
        public ActionResult<DataTableResponse> GetRunControlPageData()
        {
            List<CreateRunControlPageClass> data = objAppService.GetRunControlPageData(_context);
            return new DataTableResponse
            {
                RecordsTotal = data.Count(),
                RecordsFiltered = 10,
                Data = data.ToArray()
            };
        }
        //Get Run Control Page Record
        [Route("/api/Processor/GetRunControlPageRecord")]
        [HttpGet]
        public ActionResult<DataTableResponse> GetRunControlPageRecord(int Id)
        {
            List<CreateRunControlPageClass> data = objAppService.GetRunControlPageRecord(_context, Id);

            return new DataTableResponse
            {
                RecordsTotal = data.Count(),
                RecordsFiltered = 10,
                Data = data.ToArray()
            };
        }
        //Save Run Control Page
        [Route("/api/Processor/SaveRunControlPage")]
        [HttpPost]
        public List<CreateRunControlPageClass> SaveRunControlPage([FromBody] List<CreateRunControlPageClass> runcontrol)
        {
            List<CreateRunControlPageClass> objlist = new List<CreateRunControlPageClass>();
            string result = "";
            try
            {
                result = objAppService.SaveRunControlPage(_context, runcontrol);
                CreateRunControlPageClass listobjlist = new CreateRunControlPageClass();
                listobjlist.Result = result;
                objlist.Add(listobjlist);

            }
            catch (Exception ex)
            {
                CreateRunControlPageClass listobjnew = new CreateRunControlPageClass();
                listobjnew.Result = result;
                objlist.Add(listobjnew);
            }
            return objlist;

        }
        //Delete Run Control Page
        [Route("/api/Processor/DeleteRunControlPage")]
        [HttpGet]
        public string DeleteRunControlPage(int Id)
        {
            string result = "";
            try
            {
                result = objAppService.DeleteRunControlPage(_context, Id);
            }
            catch (Exception ex)
            {

            }
            return result;
        }
        #endregion
        #region Servers
        public ActionResult Servers(string environment)
        {
            return View(OrderResx.ServersTitle, ViewBag.environment);
        }
        public ActionResult CreateServer()
        {
            int UserId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            string UserRole = HttpContext.Session.GetString("UserRole");
            var CreateServer = new CreateServerClass()
            {

            };
            if (UserRole != OrderResx.Admin)
            {
                return RedirectToAction("UserNotAuthorized", "Home");
            }
            return View(OrderResx.CreateServerTitle, CreateServer);
        }
        //Get Server Data
        [Route("/api/Processor/GetServersData")]
        [HttpGet]
        public ActionResult<DataTableResponse> GetServersData()
        {
            List<CreateServerClass> data = objAppService.GetServerData(_context);
            return new DataTableResponse
            {
                RecordsTotal = data.Count(),
                RecordsFiltered = 10,
                Data = data.ToArray()
            };
        }
        //Get Server Record
        [Route("/api/Processor/GetServerRecord")]
        [HttpGet]
        public ActionResult<DataTableResponse> GetServerRecord(int Id)
        {
            List<CreateServerClass> data = objAppService.GetServerRecord(_context, Id);

            return new DataTableResponse
            {
                RecordsTotal = data.Count(),
                RecordsFiltered = 10,
                Data = data.ToArray()
            };
        }
        //Save Server
        [Route("/api/Processor/SaveServer")]
        [HttpPost]
        public List<CreateServerClass> SaveServer([FromBody] List<CreateServerClass> server)
        {
            List<CreateServerClass> objlist = new List<CreateServerClass>();
            string result = "";
            try
            {
                result = objAppService.SaveServer(_context, server);
                CreateServerClass listobjlist = new CreateServerClass();
                listobjlist.Result = result;
                objlist.Add(listobjlist);

            }
            catch (Exception ex)
            {
                CreateServerClass listobjnew = new CreateServerClass();
                listobjnew.Result = result;
                objlist.Add(listobjnew);
            }
            return objlist;

        }
        //Delete Server
        [Route("/api/Processor/DeleteServer")]
        [HttpGet]
        public string DeleteServer(int Id)
        {
            string result = "";
            try
            {
                result = objAppService.DeleteServer(_context, Id);
            }
            catch (Exception ex)
            {

            }
            return result;
        }
        #endregion
        #region Process Request
        public ActionResult CreateProcessRequest()
        {
            int UserId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            string UserRole = HttpContext.Session.GetString("UserRole");
            var CreateProcessRequest = new ProcessRequestClass()
            {

            };
            if (UserRole != OrderResx.Admin)
            {
                return RedirectToAction("UserNotAuthorized", "Home");
            }
            return View(OrderResx.CreateProcessRequestTitle, CreateProcessRequest);
        }
        //Get Schedulers Data Active
        [Route("/api/Processor/GetSchedulersDataActive")]
        [HttpGet]
        public ActionResult<DataTableResponse> GetSchedulersDataActive()
        {
            List<CreateScheduleClass> data = objAppService.GetSchedulersDataActive(_context);
            return new DataTableResponse
            {
                RecordsTotal = data.Count(),
                RecordsFiltered = 10,
                Data = data.ToArray()
            };
        }
        //Get Max Control Id
        [Route("api/Processor/GetMaxControlId")]
        [HttpGet]
        public int GetMaxControlId(int ProcessId,int ProcessGroupId)
        {
            int runcontrolid = 0;
            runcontrolid = objAppService.GetMaxControlId(_context, ProcessId, ProcessGroupId);
            return runcontrolid;
        }
        //Get Process Request by Process Id
        [Route("/api/Processor/ProcessRequestTableByProcessId")]
        [HttpGet]
        public ActionResult<DataTableResponse> ProcessRequestTableByProcessId(int ProcessId,int ProcessGroupId)
        {
            List<CreateProcessClass> data = objAppService.ProcessRequestTableByProcessId(_context, ProcessId, ProcessGroupId);
            return new DataTableResponse
            {
                RecordsTotal = data.Count(),
                RecordsFiltered = 10,
                Data = data.ToArray()
            };
        }
        //Save Process
        [Route("/api/Processor/SaveProcessRequest")]
        [HttpPost]
        public List<ProcessRequestClass> SaveProcessRequest([FromBody] List<ProcessRequestClass> processrequest)
        {
            List<ProcessRequestClass> objlist = new List<ProcessRequestClass>();
            string result = "";
            try
            {
                result = objAppService.SaveProcessRequest(_context, processrequest);
                ProcessRequestClass listobjlist = new ProcessRequestClass();
                listobjlist.Result = result;
                objlist.Add(listobjlist);

            }
            catch (Exception ex)
            {
                ProcessRequestClass listobjnew = new ProcessRequestClass();
                listobjnew.Result = result;
                objlist.Add(listobjnew);
            }
            return objlist;

        }
        //Get Process Request Record
        [Route("/api/Processor/GetProcessRequestRecord")]
        [HttpGet]
        public ActionResult<DataTableResponse> GetProcessRequestRecord(int Id)
        {
            List<ProcessRequestClass> data = objAppService.GetProcessRequestRecord(_context, Id);

            return new DataTableResponse
            {
                RecordsTotal = data.Count(),
                RecordsFiltered = 10,
                Data = data.ToArray()
            };
        }
        //Get ProcessRequest by Process Request Id
        [Route("/api/Processor/ProcessRequestbyProcessRequestId")]
        [HttpGet]
        public ActionResult<DataTableResponse> ProcessRequestbyProcessRequestId(int Id)
        {
            List<ProcessRequestsClass> data = objAppService.ProcessRequestbyProcessRequestId(_context, Id);
            return new DataTableResponse
            {
                RecordsTotal = data.Count(),
                RecordsFiltered = 10,
                Data = data.ToArray()
            };
        }
        //Save Process Request Items
        [Route("/api/Processor/SaveProcessRequestItems")]
        [HttpPost]
        public List<ProcessRequestsClass> SaveProcessRequestItems([FromBody] List<ProcessRequestsClass> processitems)
        {
            List<ProcessRequestsClass> objlist = new List<ProcessRequestsClass>();
            string result = "";
            try
            {
                result = objAppService.SaveProcessRequestItems(_context, processitems);
                ProcessRequestsClass listobjlist = new ProcessRequestsClass();
                listobjlist.Result = result;
                objlist.Add(listobjlist);

            }
            catch (Exception ex)
            {
                ProcessRequestsClass listobjnew = new ProcessRequestsClass();
                listobjnew.Result = result;
                objlist.Add(listobjnew);
            }
            return objlist;
        }
        //Delete Process Request Items
        [Route("/api/Processor/DeleteProcessRequestItems")]
        [HttpGet]
        public string DeleteProcessRequestItems(int Id)
        {
            string result = "";
            try
            {
                result = objAppService.DeleteProcessRequestItems(_context, Id);
            }
            catch (Exception ex)
            {

            }
            return result;
        }
        #endregion
        #region Process Request Monitor
        public ActionResult ProcessRequestMonitor(string environment)
        {
            return View(OrderResx.ProcessRequestMonitor, ViewBag.environment);
        }
        //Get Process Request Data
        [Route("/api/Processor/GetProcessRequestData")]
        [HttpGet]
        public ActionResult<DataTableResponse> GetProcessRequestData()
        {
            List<ProcessRequestvwModel> data = objAppService.GetProcessRequestData(_context);
            return new DataTableResponse
            {
                RecordsTotal = data.Count(),
                RecordsFiltered = 10,
                Data = data.ToArray()
            };
        }
        #endregion
    }
}