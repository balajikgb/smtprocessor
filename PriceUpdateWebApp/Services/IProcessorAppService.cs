using PriceUpdateRepository.DataModels;
using System.Collections.Generic;
using PriceUpdateRepository;
using Core.Services;
using System.Data;
using ArasPLMWebAp.Models;
namespace PriceUpdate.Services
{
    public interface IProcessorAppService 
    {
        #region Process
        List<CreateProcessClass> GetRunControlbyProcess(Context _context);
        List<CreateProcessClass> GetProcessData(Context _context);
        List<CreateProcessClass> GetProcessRecord(Context _context, int Id);
        string SaveProcess(Context _context, List<CreateProcessClass> process);
        string DeleteProcess(Context _context, int Id);
        #endregion
        #region Process Group
        List<CreateProcessGroupClass> GetProcessGroupData(Context _context);
        List<CreateProcessGroupClass> GetProcessGroupRecord(Context _context, int Id);
        string SaveProcessGroup(Context _context, List<CreateProcessGroupClass> processgroup);
        string DeleteProcessGroup(Context _context, int Id);
        #endregion
        #region Process Group Items
        List<CreateProcessClass> GetProcessDataActive(Context _context);
        List<CreateProcessGroupItemsClass> ProcessDatabyProcessGroupId(Context _context, int Id);
        string SaveProcessGroupItems(Context _context, List<CreateProcessGroupItemsClass> groupitems);
        string DeleteProcessGroupItems(Context _context, int Id);
        #endregion
        #region Run Control Page
        List<CreateProcessGroupClass> GetProcessGroupDataActive(Context _context);
        List<CreateProcessClass> GetProcessDataByProcessGroupId(Context _context, int Id);
        List<CreateServerClass> GetServerDataActive(Context _context);
        List<CreateRunControlPageClass> GetRunControlPageData(Context _context);
        List<CreateRunControlPageClass> GetRunControlPageRecord(Context _context, int Id);
        string SaveRunControlPage(Context _context, List<CreateRunControlPageClass> runcontrol);
        string DeleteRunControlPage(Context _context, int Id);
        #endregion   
        #region Server
        List<CreateServerClass> GetServerData(Context _context);
        List<CreateServerClass> GetServerRecord(Context _context, int Id);
        string SaveServer(Context _context, List<CreateServerClass> server);
        string DeleteServer(Context _context, int Id);
        #endregion
        #region Process Request
        int GetMaxControlId(Context _context, int ProcessId, int ProcessGroupId);
        List<CreateScheduleClass> GetSchedulersDataActive(Context _context);
        List<CreateProcessClass> ProcessRequestTableByProcessId(Context _context, int ProcessId, int ProcessGroupId);
        string SaveProcessRequest(Context _context, List<ProcessRequestClass> processrequest);
        List<ProcessRequestsClass> ProcessRequestbyProcessRequestId(Context _context, int Id);
        string SaveProcessRequestItems(Context _context, List<ProcessRequestsClass> processitems);
        string DeleteProcessRequestItems(Context _context, int Id);
        #endregion
        #region Process Request Monitor
        List<ProcessRequestvwModel> GetProcessRequestData(Context _context);
        #endregion
    }
}
