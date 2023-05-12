using Microsoft.Extensions.Options;
using System.IO;
using System.Web.UI;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.Text;
using MailKit.Search;
using System.Net.Mail;
using AspNetCore;
using System.Net;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using Microsoft.AspNetCore.Html;
using ProcessorWebApp.Resources;
using ClosedXML;
using ClosedXML.Excel;
using System.Net.Mime;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Xml;
using SoapCore;
using ArasPLMWebAp;
using System.Dynamic;
using PriceUpdateRepository;
using Microsoft.Data.SqlClient;
using System.Data.Common;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using PriceUpdateRepository.DataModels;
using Microsoft.Extensions.Azure;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.IdentityModel.Tokens;
using System.Security.Policy;
using Org.BouncyCastle.Crypto.Engines;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json.Schema;
using Core.Interfaces.Services;
using System.Collections.Generic;
using ArasPLMWebAp.Models;
using System.Linq;
using System;
using Core.Enums;
using System.Diagnostics;
using DocumentFormat.OpenXml.Office2010.Excel;
using PriceUpdateRepository.DataModels;
namespace PriceUpdate.Services
{
    public class ProcessorAppService : IProcessorAppService
    {
        protected IUserService _userService;

        public object Blogs { get; set; }

        #region Process
        //Get Process Data
        public List<CreateProcessClass> GetProcessData(Context _context)
        {
            var process = _context.Processes.ToList();
            if (process != null)
            {
                List<CreateProcessClass> processlist = process
                    .Select(m => new CreateProcessClass()
                    {
                        processid = m.processid,
                        name = m.name,
                        processname=m.processid+"-"+m.name,
                        description=m.description,
                        command=m.command,
                        status=m.status=="1"?OrderResx.Active:OrderResx.InActive,
                        createdby=m.createdby,
                        createddate= Convert.ToDateTime(m.createddate).ToString("dd-MM-yyyy hh:mm:ss")
                    }).ToList();
                return processlist;
            }
            return null;
        }
        //Get Run Control by Process
        public List<CreateProcessClass> GetRunControlbyProcess(Context _context)
        {
            var process = _context.RunControlPage.Where(r => r.status=="1").ToList();
            if (process != null)
            {
                List<CreateProcessClass> processlist = process
                    .Select(m => new CreateProcessClass()
                    {
                        runid=m.runid,
                        processid = m.processid
                    }).ToList();
                return processlist;
            }
            return null;
        }
        //Get Process record
        public List<CreateProcessClass> GetProcessRecord(Context _context, int Id)
        {
            var process = _context.Processes.Where(b => b.processid == Id).ToList();
            if (process != null)
            {
                List<CreateProcessClass> processlist = process
                    .Select(m => new CreateProcessClass()
                    {
                        processid = m.processid,
                        name = m.name,
                        description = m.description,
                        command = m.command,
                        status = m.status,
                    })
                   .ToList();
                return processlist;
            }
            return null;
        }
        //Save Process
        public string SaveProcess(Context _context, List<CreateProcessClass> process)
        {
            string result = "";
            ProcessClassModel processlist = _context.Processes.Where(e => e.processid == process[0].processid).FirstOrDefault();
            ProcessClassModel processdetails = _context.Processes.Where(e => e.name.Trim().ToUpper() == process[0].name.Trim().ToUpper() && e.processid != process[0].processid).FirstOrDefault();
            try
            {
                _context.Database.BeginTransaction();
                var processdetailslist = new PriceUpdateRepository.DataModels.ProcessClassModel();
                if (processdetails != null)
                {
                    result = OrderResx.AlreadyExisting;
                }
                if (processlist == null && processdetails == null)
                {
                    processdetailslist.name = process[0].name;
                    processdetailslist.description = process[0].description;
                    processdetailslist.command = process[0].command;
                    processdetailslist.status = process[0].status;
                    processdetailslist.createdby = process[0].createdby;
                    processdetailslist.createddate = DateTime.Parse(process[0].createddate);
                    _context.Add(processdetailslist);
                    _context.SaveChanges();
                    _context.Database.CommitTransaction();
                    result = OrderResx.Success;
                }
                else if (processlist != null && processdetails == null)
                {
                    processlist.name = process[0].name;
                    processlist.description = process[0].description;
                    processlist.command = process[0].command;
                    processlist.status = process[0].status;
                    _context.SaveChanges();
                    _context.Database.CommitTransaction();
                    result = OrderResx.Success;
                }
            }
            catch (Exception ex)
            {
                result = OrderResx.Failure;
                _context.Database.RollbackTransaction();
            }
            return result;
        }
        //Delete Process
        public string DeleteProcess(Context _context, int Id)
        {
            string result = "";
            var processgroupitems = _context.ProcessesGroupItems.Where(p => p.processid == Id).ToList();
            var runcontrolitems = _context.RunControlPage.Where(r => r.processid == Id).ToList();
            var processrequestitems = _context.ProcessRequest.Where(p => p.processid == Id).ToList();
            try
            {
                var process = _context.Processes.Where(b => b.processid == Id).FirstOrDefault();
                _context.Remove(process);
                _context.SaveChanges();
                if (processgroupitems.Count > 0)
                {
                    foreach (var processgroup in processgroupitems)
                    {
                        _context.Remove(processgroup);
                    }
                    _context.SaveChanges();
                }
                if (runcontrolitems.Count > 0)
                {
                    foreach (var runcontrol in runcontrolitems)
                    {
                        _context.Remove(runcontrol);
                    }
                    _context.SaveChanges();
                }
                if (processrequestitems.Count > 0)
                {
                    foreach (var processrequest in processrequestitems)
                    {
                        _context.Remove(processrequest);
                    }
                    _context.SaveChanges();
                }
                result = OrderResx.Success;
            }
            catch (Exception ex)
            {
                result = OrderResx.Failure;
            }
            return result;
        }
        #endregion
        #region Process Group
        //Get Process Group Data
        public List<CreateProcessGroupClass> GetProcessGroupData(Context _context)
        {
            var processgroup = _context.ProcessesGroup.ToList();
            if (processgroup != null)
            {
                List<CreateProcessGroupClass> processgrouplist = processgroup
                    .Select(m => new CreateProcessGroupClass()
                    {
                        processgroupid = m.processgroupid,
                        name = m.name,
                        processgroupname = m.processgroupid + "-" + m.name,
                        description = m.description,
                        status = m.status == "1" ? OrderResx.Active : OrderResx.InActive,
                        createdby = m.createdby,
                        createddate = Convert.ToDateTime(m.createddate).ToString("dd-MM-yyyy hh:mm:ss")
                    }).ToList();
                return processgrouplist;
            }
            return null;
        }
        //Get Process Group record
        public List<CreateProcessGroupClass> GetProcessGroupRecord(Context _context, int Id)
        {
            var processgroup = _context.ProcessesGroup.Where(p => p.processgroupid == Id).ToList();
            var processgroupitems = _context.ProcessesGroupItems.Where(p => p.processgroupid == Id).Count();
            if (processgroup != null)
            {
                List<CreateProcessGroupClass> processgrouplist = processgroup
                    .Select(m => new CreateProcessGroupClass()
                    {
                        processgroupid = m.processgroupid,
                        name = m.name,
                        description = m.description,
                        status = m.status,
                        processgroupitemcount = processgroupitems
                    }).ToList();
                //List<CreateProcessGroupClass> processgroupitemcount = processgroupitems
                //    .Select(m => new CreateProcessGroupClass()
                //    {
                //        processgroupitemcount = m.processid
                //    }).ToList();
                //processgrouplist.AddRange(processgroupitemcount);
                return processgrouplist;
            }
            return null;
        }
        //Save Process Group
        public string SaveProcessGroup(Context _context, List<CreateProcessGroupClass> processgroup)
        {
            string result = "";
            ProcessGroupModel processgroupdata = _context.ProcessesGroup.Where(e => e.processgroupid == processgroup[0].processgroupid).FirstOrDefault();
            ProcessGroupModel processgroupdetails = _context.ProcessesGroup.Where(e => e.name.Trim().ToUpper() == processgroup[0].name.Trim().ToUpper() && e.processgroupid != processgroup[0].processgroupid).FirstOrDefault();
            try
            {
                _context.Database.BeginTransaction();
                var processgrouplist = new PriceUpdateRepository.DataModels.ProcessGroupModel();
                if (processgroupdetails != null)
                {
                    result = OrderResx.AlreadyExisting;
                }
                if (processgroupdata == null && processgroupdetails == null)
                {
                    processgrouplist.name = processgroup[0].name;
                    processgrouplist.description = processgroup[0].description;
                    processgrouplist.status = processgroup[0].status;
                    processgrouplist.createdby = processgroup[0].createdby;
                    processgrouplist.createddate = DateTime.Parse(processgroup[0].createddate);
                    _context.Add(processgrouplist);
                    _context.SaveChanges();
                    _context.Database.CommitTransaction();
                    result = OrderResx.Success;
                }
                else if (processgrouplist != null && processgroupdetails == null)
                {
                    processgrouplist.name = processgroup[0].name;
                    processgrouplist.description = processgroup[0].description;
                    processgrouplist.status = processgroup[0].status;
                    _context.SaveChanges();
                    _context.Database.CommitTransaction();
                    result = OrderResx.Success;
                }
            }
            catch (Exception ex)
            {
                result = OrderResx.Failure;
                _context.Database.RollbackTransaction();
            }
            return result;
        }
        //Delete Process Group
        public string DeleteProcessGroup(Context _context, int Id)
        {
            string result = "";
            var processgroupitems = _context.ProcessesGroupItems.Where(p => p.processgroupid == Id).ToList();
            var runcontrolitems = _context.RunControlPage.Where(r => r.processgroupid == Id).ToList();
            var processrequestitems = _context.ProcessRequest.Where(p => p.processgroupid == Id).ToList();
            try
            {
                var processgroup = _context.ProcessesGroup.Where(b => b.processgroupid == Id).FirstOrDefault();
                _context.Remove(processgroup);
                _context.SaveChanges();
                if (processgroupitems.Count > 0)
                {
                    foreach (var processgroupitem in processgroupitems)
                    {
                        _context.Remove(processgroupitem);
                    }
                    _context.SaveChanges();
                }
                if (runcontrolitems.Count > 0)
                {
                    foreach (var runcontrol in runcontrolitems)
                    {
                        _context.Remove(runcontrol);
                    }
                    _context.SaveChanges();
                }
                if (processrequestitems.Count > 0)
                {
                    foreach (var processrequest in processrequestitems)
                    {
                        _context.Remove(processrequest);
                    }
                    _context.SaveChanges();
                }
                result = OrderResx.Success;
            }
            catch (Exception ex)
            {
                result = OrderResx.Failure;
            }
            return result;
        }
        #endregion
        #region Process Group Items
        //Get Process Data Active
        public List<CreateProcessClass> GetProcessDataActive(Context _context)
        {
            var process = _context.Processes.Where(p=>p.status=="1").ToList();
            if (process != null)
            {
                List<CreateProcessClass> processlist = process
                    .Select(m => new CreateProcessClass()
                    {
                        processid = m.processid,
                        name = m.name,
                        processname = m.processid + "-" + m.name,
                        description = m.description,
                        command = m.command,
                        status = m.status == "1" ? OrderResx.Active : OrderResx.InActive,
                        createdby = m.createdby,
                        createddate = Convert.ToDateTime(m.createddate).ToString("dd-MM-yyyy hh:mm:ss")
                    }).ToList();
                return processlist;
            }
            return null;
        }
        //Get Process Data by Process GroupId
        public List<CreateProcessGroupItemsClass> ProcessDatabyProcessGroupId(Context _context, int Id)
        {
            List<CreateProcessGroupItemsClass> processgroupitemslist = new List<CreateProcessGroupItemsClass>();
            try
            {
                if (Id != 0)
                {
                    processgroupitemslist = (from A in _context.ProcessesGroupItems
                                             join B in _context.Processes on
                                             A.processid equals B.processid
                                             where A.processgroupid == Id
                                             select new CreateProcessGroupItemsClass
                                             {
                                                 processgroupitemid = A.processgroupitemid,
                                                 processgroupid = A.processgroupid,
                                                 processid = A.processid,
                                                 processname = B.name,
                                                 runsequenceid=A.runsequenceid,
                                                 status = A.status == "1" ? OrderResx.Active : OrderResx.InActive,
                                                 createdby = A.createdby,
                                                 createddate = Convert.ToDateTime(A.createddate).ToString("dd-MM-yyyy hh:mm:ss")
                                             }).OrderBy(p => p.runsequenceid).ToList();
                }
                else
                {
                    CreateProcessGroupItemsClass obj = new CreateProcessGroupItemsClass();
                    obj.processgroupitemid = 0;
                    obj.processgroupid = 0;
                    obj.processid = 0;
                    obj.processname = "";
                    obj.runsequenceid = 0;
                    obj.status = OrderResx.Active;
                    obj.createdby = "";
                    obj.createddate = "";
                    processgroupitemslist.Add(obj);
                }
            }
            catch (Exception ex)
            {

            }
            return processgroupitemslist;
        }
        //Save Process Group Items
        public string SaveProcessGroupItems(Context _context, List<CreateProcessGroupItemsClass> groupitems)
        {
            string result = "";
            ProcessGroupItemsModel groupitemsdata = _context.ProcessesGroupItems.Where(p => p.processgroupitemid == groupitems[0].processgroupitemid).FirstOrDefault();
            ProcessGroupItemsModel processgroupdetails = _context.ProcessesGroupItems.Where(p => p.processgroupid == groupitems[0].processgroupid && p.processid == groupitems[0].processid && p.processgroupitemid != groupitems[0].processgroupitemid).FirstOrDefault();
            ProcessGroupItemsModel procesgroupitemsrunsequencedetails = _context.ProcessesGroupItems.Where(p => p.processgroupid == groupitems[0].processgroupid && p.runsequenceid == groupitems[0].runsequenceid && p.processgroupitemid != groupitems[0].processgroupitemid).FirstOrDefault();
            try
            {
                _context.Database.BeginTransaction();
                var groupitemslist = new PriceUpdateRepository.DataModels.ProcessGroupItemsModel();
                if(procesgroupitemsrunsequencedetails!=null)
                {
                    result = OrderResx.AlreadyExistingRunSequenceId;
                }
                else if (processgroupdetails != null)
                {
                    result = OrderResx.AlreadyExisting;
                }
                else if (groupitemsdata == null && processgroupdetails == null && procesgroupitemsrunsequencedetails==null)
                {
                    groupitemslist.processgroupid = groupitems[0].processgroupid;
                    groupitemslist.processid = groupitems[0].processid;
                    groupitemslist.runsequenceid = groupitems[0].runsequenceid;
                    groupitemslist.status = groupitems[0].status;
                    groupitemslist.createdby = groupitems[0].createdby;
                    groupitemslist.createddate = DateTime.Parse(groupitems[0].createddate);
                    _context.Add(groupitemslist);
                    _context.SaveChanges();
                    _context.Database.CommitTransaction();
                    result = OrderResx.Success;
                }
                else if (groupitemsdata != null && processgroupdetails == null && procesgroupitemsrunsequencedetails == null)
                {
                    groupitemsdata.processgroupid = groupitems[0].processgroupid;
                    groupitemsdata.processid = groupitems[0].processid;
                    groupitemsdata.runsequenceid = groupitems[0].runsequenceid;
                    groupitemsdata.status = groupitems[0].status;
                    _context.SaveChanges();
                    _context.Database.CommitTransaction();
                    result = OrderResx.Success;
                }
            }
            catch (Exception ex)
            {
                result = OrderResx.Failure;
                _context.Database.RollbackTransaction();
            }
            return result;
        }
        //Delete Process Group Items
        public string DeleteProcessGroupItems(Context _context, int Id)
        {
            string result = "";
            try
            {
                var processgroupitems = _context.ProcessesGroupItems.Where(p => p.processgroupitemid == Id).FirstOrDefault();
                _context.Remove(processgroupitems);
                _context.SaveChanges();
                result = OrderResx.Success;
            }
            catch (Exception ex)
            {
                result = OrderResx.Failure;
            }
            return result;
        }
        #endregion
        #region Run Control Page
        //Get Process Group Data Active
        public List<CreateProcessGroupClass> GetProcessGroupDataActive(Context _context)
        {
            var processgroup = _context.ProcessesGroup.Where(p=>p.status=="1").ToList();
            if (processgroup != null)
            {
                List<CreateProcessGroupClass> processgrouplist = processgroup
                    .Select(m => new CreateProcessGroupClass()
                    {
                        processgroupid = m.processgroupid,
                        name = m.name,
                        processgroupname = m.processgroupid + "-" + m.name,
                        description = m.description,
                        status = m.status == "1" ? OrderResx.Active : OrderResx.InActive,
                        createdby = m.createdby,
                        createddate = Convert.ToDateTime(m.createddate).ToString("dd-MM-yyyy hh:mm:ss")
                    }).ToList();
                return processgrouplist;
            }
            return null;
        }
        //Get Server Data Active
        public List<CreateServerClass> GetServerDataActive(Context _context)
        {
            var server = _context.Servers.Where(s=>s.status=="1").ToList();
            if (server != null)
            {
                List<CreateServerClass> serverlist = server
                    .Select(m => new CreateServerClass()
                    {
                        serverid = m.serverid,
                        servername = m.servername,
                        servernames = m.serverid + "-" + m.servername,
                        description = m.description,
                        status = m.status == "1" ? OrderResx.Active : OrderResx.InActive,
                        createdby = m.createdby,
                        createddate = Convert.ToDateTime(m.createddate).ToString("dd-MM-yyyy hh:mm:ss")
                    }).ToList();
                return serverlist;
            }
            return null;
        }
        //Get Run Control Page Data
        public List<CreateRunControlPageClass> GetRunControlPageData(Context _context)
        {
            var process = _context.RunControlPage.ToList();
            if (process != null)
            {
                List<CreateRunControlPageClass> processlist = process
                    .Select(m => new CreateRunControlPageClass()
                    {
                        runid = m.runid,
                        processgroupid = m.processgroupid,
                        processid = m.processid,
                        status = m.status == "1" ? OrderResx.Active : OrderResx.InActive,
                        serverid = m.serverid,
                        runby = m.runby,
                        rundttm = Convert.ToDateTime(m.rundttm).ToString("dd-MM-yyyy hh:mm:ss")
                    }).ToList();
                return processlist;
            }
            return null;
        }
        //Get Run Control Page record

        //Get Process Data By Process Group Id
        public List<CreateProcessClass> GetProcessDataByProcessGroupId(Context _context,int Id)
        {
            List<CreateProcessClass> processlist = new List<CreateProcessClass>();
            processlist = (from A in _context.Processes join
                         B in _context.ProcessesGroupItems on
                         A.processid equals B.processid
                         where B.processgroupid==Id
                         && A.status=="1"
                         select new CreateProcessClass()
                         {
                               processname = B.processid + "-" + A.name
                         }).ToList();
             return processlist;
        }
        public List<CreateRunControlPageClass> GetRunControlPageRecord(Context _context, int Id)
        {
            List<CreateRunControlPageClass> runcontrolpagelist = new List<CreateRunControlPageClass>();
            runcontrolpagelist = (from A in _context.RunControlPage
                                  join B in _context.ProcessesGroup on
                                  A.processgroupid equals B.processgroupid
                                  join C in _context.Processes on
                                  A.processid equals C.processid
                                  join D in _context.Servers on
                                  A.serverid equals D.serverid
                                  select new CreateRunControlPageClass
                                  {
                                      runid = A.runid,
                                      processgroupid = A.processgroupid,
                                      processgroupname = B.name,
                                      processid = A.processid,
                                      processname = C.name,
                                      status = A.status,
                                      serverid = A.serverid,
                                      servername = D.servername
                                  }).ToList();
            return runcontrolpagelist;
        }
        //Save Run Control Page
        public string SaveRunControlPage(Context _context, List<CreateRunControlPageClass> runcontrol)
        {
            string result = "";
            RunControlPageModel runcontroldata = _context.RunControlPage.Where(r => r.runid == runcontrol[0].runid).FirstOrDefault();
            RunControlPageModel runcontroldetails = _context.RunControlPage.Where(r => r.processgroupid == runcontrol[0].processgroupid && r.processid == runcontrol[0].processid && r.serverid == runcontrol[0].serverid && r.serverid != runcontrol[0].runid).FirstOrDefault();
            try
            {
                _context.Database.BeginTransaction();
                if (runcontroldetails != null)
                {
                    result = OrderResx.AlreadyExisting;
                }
                var runcontrollist = new PriceUpdateRepository.DataModels.RunControlPageModel();
                if (runcontroldata == null && runcontroldetails == null)
                {
                    runcontrollist.processgroupid = runcontrol[0].processgroupid;
                    runcontrollist.processid = runcontrol[0].processid;
                    runcontrollist.status = runcontrol[0].status;
                    runcontrollist.serverid = runcontrol[0].serverid;
                    runcontrollist.runby = runcontrol[0].runby;
                    runcontrollist.rundttm = DateTime.Parse(runcontrol[0].rundttm);
                    _context.Add(runcontrollist);
                    _context.SaveChanges();
                    _context.Database.CommitTransaction();
                    result = OrderResx.Success;
                }
                else if (runcontroldata != null && runcontroldetails == null)
                {
                    runcontroldata.processgroupid = runcontrol[0].processgroupid;
                    runcontroldata.processid = runcontrol[0].processid;
                    runcontroldata.status = runcontrol[0].status;
                    runcontroldata.serverid = runcontrol[0].serverid;
                    _context.SaveChanges();
                    _context.Database.CommitTransaction();
                    result = OrderResx.Success;
                }
            }
            catch (Exception ex)
            {
                result = OrderResx.Failure;
                _context.Database.RollbackTransaction();
            }
            return result;
        }
        //Delete Run Control Page
        public string DeleteRunControlPage(Context _context, int Id)
        {
            string result = "";
            try
            {
                var runcontrolitems = _context.RunControlPage.Where(r => r.runid == Id).FirstOrDefault();
                _context.Remove(runcontrolitems);
                _context.SaveChanges();
                result = OrderResx.Success;
            }
            catch (Exception ex)
            {
                result = OrderResx.Failure;
            }
            return result;
        }
        #endregion
        #region servers
        //Get Server Data
        public List<CreateServerClass> GetServerData(Context _context)
        {
            var server = _context.Servers.ToList();
            if (server != null)
            {
                List<CreateServerClass> serverlist = server
                    .Select(m => new CreateServerClass()
                    {
                        serverid = m.serverid,
                        servername = m.servername,
                        servernames=m.serverid+"-"+m.servername,
                        description = m.description,
                        status = m.status == "1" ? OrderResx.Active : OrderResx.InActive,
                        createdby = m.createdby,
                        createddate = Convert.ToDateTime(m.createddate).ToString("dd-MM-yyyy hh:mm:ss")
                    }).ToList();
                return serverlist;
            }
            return null;
        }
        //Get Server record
        public List<CreateServerClass> GetServerRecord(Context _context, int Id)
        {
            var server = _context.Servers.Where(s=>s.serverid == Id).ToList();
            if (server != null)
            {
                List<CreateServerClass> serverlist = server
                    .Select(m => new CreateServerClass()
                    {
                        serverid = m.serverid,
                        servername = m.servername,
                        description = m.description,
                        status = m.status
                    })
                   .ToList();
                return serverlist;
            }
            return null;
        }
        //Save Server
        public string SaveServer(Context _context, List<CreateServerClass> server)
        {
            string result = "";
            ServersModel serverlist = _context.Servers.Where(e => e.serverid == server[0].serverid).FirstOrDefault();
            ServersModel serverdetails = _context.Servers.Where(e => e.servername.Trim().ToUpper() == server[0].servername.Trim().ToUpper() && e.serverid != server[0].serverid).FirstOrDefault();
            try
            {
                _context.Database.BeginTransaction();
                var serverdetailslist = new PriceUpdateRepository.DataModels.ServersModel();
                if (serverdetails != null)
                {
                    result = OrderResx.AlreadyExisting;
                }
                if (serverlist == null && serverdetails == null)
                {
                    serverdetailslist.servername = server[0].servername;
                    serverdetailslist.description = server[0].description;
                    serverdetailslist.status = server[0].status;
                    serverdetailslist.createdby = server[0].createdby;
                    serverdetailslist.createddate = DateTime.Parse(server[0].createddate);
                    _context.Add(serverdetailslist);
                    _context.SaveChanges();
                    _context.Database.CommitTransaction();
                    result = OrderResx.Success;
                }
                else if (serverlist != null && serverdetails == null)
                {
                    serverlist.servername = server[0].servername;
                    serverlist.description = server[0].description;
                    serverlist.status = server[0].status;
                    _context.SaveChanges();
                    _context.Database.CommitTransaction();
                    result = OrderResx.Success;
                }
            }
            catch (Exception ex)
            {
                result = OrderResx.Failure;
                _context.Database.RollbackTransaction();
            }
            return result;
        }
        //Delete Server
        public string DeleteServer(Context _context, int Id)
        {
            string result = "";
            var runcontrolitems = _context.RunControlPage.Where(r => r.serverid == Id).ToList();
            var processrequestitems = _context.ProcessRequest.Where(p => p.servername == Id).ToList();
            try
            {
                var server = _context.Servers.Where(b => b.serverid == Id).FirstOrDefault();
                _context.Remove(server);
                _context.SaveChanges();
                if (runcontrolitems.Count > 0)
                {
                    foreach (var runcontrol in runcontrolitems)
                    {
                        _context.Remove(runcontrol);
                    }
                    _context.SaveChanges();
                }
                if (processrequestitems.Count > 0)
                {
                    foreach (var processrequest in processrequestitems)
                    {
                        _context.Remove(processrequest);
                    }
                    _context.SaveChanges();
                }
                result = OrderResx.Success;
            }
            catch (Exception ex)
            {
                result = OrderResx.Failure;
            }
            return result;
        }
        #endregion
        #region Process Request
        public int GetMaxControlId(Context _context, int ProcessId, int ProcessGroupId)
        {
            int runcontrolid = 0;
            ProcessRequestModel processdetails = new ProcessRequestModel();
            if (ProcessId != 0)
            {
                processdetails = _context.ProcessRequest.Where(p => p.processid == ProcessId).FirstOrDefault();
            }
            else
            {
                processdetails = _context.ProcessRequest.Where(p => p.processgroupid == ProcessGroupId).FirstOrDefault();
            }
            if (processdetails == null)
            {
                runcontrolid = runcontrolid + 1;
            }
            else
            {
                if (ProcessId != 0)
                {
                    runcontrolid = _context.ProcessRequest.DefaultIfEmpty().Where(p => p.processid == ProcessId).Max(b => b == null ? 0 : b.runcontrolid);
                }
                else
                {
                    runcontrolid = _context.ProcessRequest.DefaultIfEmpty().Where(p => p.processgroupid == ProcessGroupId).Max(b => b == null ? 0 : b.runcontrolid);
                }
                runcontrolid = runcontrolid + 1;
            }
            return runcontrolid;
        }
      
        //Get Schedulers Data Active
        public List<CreateScheduleClass> GetSchedulersDataActive(Context _context)
        {
            var schedulers = _context.Schedulers.Where(s => s.status == "1").ToList();
            if (schedulers != null)
            {
                List<CreateScheduleClass> schedulerslist = schedulers
                    .Select(m => new CreateScheduleClass()
                    {
                        schedulename=m.scheduleid+"-"+m.name
                    }).ToList();
                return schedulerslist;
            }
            return null;
        }
        //Get Process Request by Process Id
        public List<CreateProcessClass> ProcessRequestTableByProcessId(Context _context, int ProcessId, int ProcessGroupId)
        {
            List<CreateProcessClass> processlist = new List<CreateProcessClass>();
            try
            {
                if (ProcessId != 0)
                {
                    processlist = (from A in _context.Processes
                                   where A.processid == ProcessId
                                   select new CreateProcessClass
                                   {
                                       processid = A.processid,
                                       name = A.name,
                                       processname = A.processid + "-" + A.name,
                                       description = A.description,
                                       command = A.command,
                                       status = A.status == "1" ? OrderResx.Active : OrderResx.InActive,
                                       createdby = A.createdby,
                                       createddate = Convert.ToDateTime(A.createddate).ToString("dd-MM-yyyy hh:mm:ss")
                                   }).ToList();
                }
                else
                {
                    processlist = (from A in _context.ProcessesGroupItems
                                   join B in _context.Processes
                                   on A.processid equals B.processid
                                   where A.processgroupid==ProcessGroupId
                                   orderby A.runsequenceid
                                   select new CreateProcessClass
                                   {
                                       processid = B.processid,
                                       name = B.name,
                                       processname = B.processid + "-" + B.name,
                                       description = B.description,
                                       command = B.command,
                                       status = B.status == "1" ? OrderResx.Active : OrderResx.InActive,
                                       createdby = B.createdby,
                                       createddate = Convert.ToDateTime(B.createddate).ToString("dd-MM-yyyy hh:mm:ss")
                                   }).ToList();
                }
            }
            catch (Exception ex)
            {

            }
            return processlist;
        }
        //Save Process Request
        public string SaveProcessRequest(Context _context, List<ProcessRequestClass> processrequest)
        {
            string result = "";
            //ProcessRequestModel processrequestlist = _context.ProcessRequest.Where(p => p.processrequestid == processrequest[0].processrequestid).FirstOrDefault();
            //ProcessRequestModel processrequestdetails = _context.ProcessRequest.Where(e => e.servername == processrequest[0].servername && e.scheduleid==processrequest[0].scheduleid && e.processrequestid != processrequest[0].processrequestid).FirstOrDefault();
            try
            {
                _context.Database.BeginTransaction();
                var processrequestdetailslist = new PriceUpdateRepository.DataModels.ProcessRequestModel();
                //if (processrequestdetails != null)
                //{
                //    result = OrderResx.AlreadyExisting;
                //}
                //if (processrequestlist == null && processrequestdetails == null)
                //{
                    processrequestdetailslist.userid = processrequest[0].userid;
                    processrequestdetailslist.processid = processrequest[0].processid;
                    processrequestdetailslist.processgroupid = processrequest[0].processgroupid;
                    processrequestdetailslist.runcontrolid = processrequest[0].runcontrolid;
                    processrequestdetailslist.scheduleid = processrequest[0].scheduleid;
                    processrequestdetailslist.servername = processrequest[0].servername;
                    processrequestdetailslist.rundate = processrequest[0].rundate;
                    processrequestdetailslist.runtime = processrequest[0].runtime;
                    processrequestdetailslist.status = processrequest[0].status;
                    processrequestdetailslist.createdby = processrequest[0].createdby;
                    processrequestdetailslist.createddate= DateTime.Parse(processrequest[0].createddate);
                    _context.Add(processrequestdetailslist);
                    _context.SaveChanges();
                    _context.Database.CommitTransaction();
                    result = OrderResx.Success;
                //}
                //else if (processrequestlist != null && processrequestdetails == null)
                //{
                //    processrequestlist.userid = processrequest[0].userid;
                //    processrequestlist.runcontrolid = processrequest[0].runcontrolid;
                //    processrequestlist.scheduleid = processrequest[0].scheduleid;
                //    processrequestlist.servername = processrequest[0].servername;
                //    processrequestlist.rundate = processrequest[0].rundate;
                //    processrequestlist.runtime = processrequest[0].runtime;
                //    processrequestlist.status = processrequest[0].status;
                //    _context.SaveChanges();
                //    _context.Database.CommitTransaction();
                //    result = OrderResx.Success;
                //}
            }
            catch (Exception ex)
            {
                result = OrderResx.Failure;
                _context.Database.RollbackTransaction();
            }
            return result;
        }
        //Get Process Request Record
        public List<ProcessRequestClass> GetProcessRequestRecord(Context _context, int Id)
        {
            List<ProcessRequestClass> processrequest = new List<ProcessRequestClass>();
            processrequest = (from A in _context.ProcessRequest join
                                B in _context.Users on
                                A.userid equals B.Id join
                                C in _context.Schedulers on
                                A.scheduleid equals C.scheduleid join
                                D in _context.Servers on
                                A.servername equals D.serverid
                                where A.processid==Id
                                select new ProcessRequestClass()
                                {
                                      processrequestid = A.processrequestid,
                                      userid = A.userid,
                                      username = B.FirstName + ' ' + B.SecondName,
                                      scheduleid = A.scheduleid,
                                      schedulename = C.name,
                                      servername = A.servername,
                                      server = D.servername,
                                      rundate = A.rundate,
                                      runtime = A.runtime,
                                      status = A.status,
                                      createdby = A.createdby,
                                      createddate = Convert.ToDateTime(A.createddate).ToString("dd-MM-yyyy hh:mm:ss"),
                                      processid = A.processid
                                 }).ToList();

            return processrequest;
        }
        //Get Process Request by Process Request Id
        public List<ProcessRequestsClass> ProcessRequestbyProcessRequestId(Context _context, int Id)
        {
            List<ProcessRequestsClass> processrequestitemlistlist = new List<ProcessRequestsClass>();
            try
            {
                if (Id != 0)
                {
                    processrequestitemlistlist = (from A in _context.ProcessRequests
                                             join B in _context.Processes on
                                             A.processid equals B.processid
                                             where A.processrequestid == Id
                                             select new ProcessRequestsClass
                                             {
                                                 processrequestitemid = A.processrequestitemid,
                                                 processid = A.processid,
                                                 processname = B.name,
                                                 status = A.status == "1" ? OrderResx.Active : OrderResx.InActive,
                                                 runby = A.runby,
                                                 rundatetime = Convert.ToDateTime(A.rundatetime).ToString("dd-MM-yyyy hh:mm:ss"),
                                                 createdby = A.createdby,
                                                 createddate = Convert.ToDateTime(A.createddate).ToString("dd-MM-yyyy hh:mm:ss")
                                             }).ToList();
                }
                else
                {
                    ProcessRequestsClass obj = new ProcessRequestsClass();
                    obj.processrequestitemid = 0;
                    obj.processrequestid = 0;
                    obj.processid = 0;
                    obj.processname = "";
                    obj.status = OrderResx.Active;
                    obj.runby = "";
                    obj.rundatetime = "";
                    obj.createdby = "";
                    obj.createddate = "";
                    processrequestitemlistlist.Add(obj);
                }
            }
            catch (Exception ex)
            {

            }
            return processrequestitemlistlist;
        }
        //Save Process Request Items
        public string SaveProcessRequestItems(Context _context, List<ProcessRequestsClass> processitems)
        {
            string result = "";
            ProcessRequestsModel processrequestitemsdata = _context.ProcessRequests.Where(p => p.processrequestitemid == processitems[0].processrequestitemid).FirstOrDefault();
            ProcessRequestsModel processrequestdetails = _context.ProcessRequests.Where(p => p.processrequestid == processitems[0].processrequestid && p.processid == processitems[0].processid && p.processrequestitemid != processitems[0].processrequestitemid).FirstOrDefault();
            try
            {
                _context.Database.BeginTransaction();
                var processrequestitemslist = new PriceUpdateRepository.DataModels.ProcessRequestsModel();
                if (processrequestdetails != null)
                {
                    result = OrderResx.AlreadyExisting;
                }
                else if (processrequestitemsdata == null && processrequestdetails == null)
                {
                    processrequestitemslist.processrequestid = processitems[0].processrequestid;
                    processrequestitemslist.processid = processitems[0].processid;
                    processrequestitemslist.status = processitems[0].status;
                    processrequestitemslist.runby = processitems[0].runby;
                    processrequestitemslist.rundatetime = DateTime.Parse(processitems[0].rundatetime);
                    processrequestitemslist.createdby = processitems[0].createdby;
                    processrequestitemslist.createddate = DateTime.Parse(processitems[0].createddate);
                    _context.Add(processrequestitemslist);
                    _context.SaveChanges();
                    _context.Database.CommitTransaction();
                    result = OrderResx.Success;
                }
                else if (processrequestitemsdata != null && processrequestdetails == null)
                {
                    processrequestitemsdata.processrequestid = processitems[0].processrequestid;
                    processrequestitemsdata.processid = processitems[0].processid;
                    processrequestitemsdata.status = processitems[0].status;
                    processrequestitemsdata.runby = processitems[0].runby;
                    processrequestitemsdata.rundatetime = DateTime.Parse(processitems[0].rundatetime);
                    _context.SaveChanges();
                    _context.Database.CommitTransaction();
                    result = OrderResx.Success;
                }
            }
            catch (Exception ex)
            {
                result = OrderResx.Failure;
                _context.Database.RollbackTransaction();
            }
            return result;
        }
        //Delete Process Request Items
        public string DeleteProcessRequestItems(Context _context, int Id)
        {
            string result = "";
            try
            {
                var processrequestitems = _context.ProcessRequests.Where(p => p.processrequestitemid == Id).FirstOrDefault();
                _context.Remove(processrequestitems);
                _context.SaveChanges();
                result = OrderResx.Success;
            }
            catch (Exception ex)
            {
                result = OrderResx.Failure;
            }
            return result;
        }
        #endregion
        #region Process Request Monitor
        //Get Process Request Data
        public List<ProcessRequestvwModel> GetProcessRequestData(Context _context)
        {
            List<ProcessRequestvwModel> processrequest = new List<ProcessRequestvwModel>();
            processrequest = (from A in _context.getprocessrequestdata
                              select new ProcessRequestvwModel()
                              {
                                  processrequestid = A.processrequestid,
                                  processid = A.processid,
                                  processname=A.processname,
                                  processgroupid = A.processgroupid,
                                  processgroupname=A.processgroupname,
                                  userid = A.userid,
                                  name=A.name,
                                  runcontrolid = A.runcontrolid,
                                  scheduleid = A.scheduleid,
                                  schedulename=A.schedulename,
                                  serverid = A.serverid,
                                  servername=A.servername,
                                  rundate = A.rundate,
                                  runtime = A.runtime,
                                  status = A.status == "1" ? OrderResx.Active : OrderResx.InActive,
                                  createdby = A.createdby,
                                  createddate = Convert.ToDateTime(A.createddate).ToString("dd-MM-yyyy hh:mm:ss")
                              }).OrderByDescending(p => p.processrequestid).ToList();

            return processrequest;
        }
        #endregion
    }
}