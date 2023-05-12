using Core.Enums;
using Core.Services;
using PriceUpdateRepository.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
namespace ArasPLMWebAp.Models
{
    public class ProcessorModel
    {

        //[Display(Name = "
        //:")]
        [StringLength(20, MinimumLength = 3)]
        public string PartNumber { get; set; }

        public Identifiers? Identifier { get; set; }

        //[Display(Name = "Retrieve By:")]
        [StringLength(20, MinimumLength = 3)]

        public string Description { get; set; }

        public string CurrentListPrice { get; set; }


        public string NewListPrice { get; set; }

    }
    public class MatrixData
    {
        [Key]
        public int MatrixID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Byte DataTypeID { get; set; }

        public string DataType { get; set; }
        public Guid EntityID { get; set; }
        public DateTime MaintenDate { get; set; }
        public string MaintenanceDate { get; set; }
        public Byte[] CompiledMatrix { get; set; }
        public Byte Version { get; set; }
        public string DefaultValue { get; set; }
        public string Result { get; set; }




    }
    public class SaveMatrixData
    {
        [Key]
        public int id { get; set; }
        public string retrievedby { get; set; }
        public string identifier { get; set; }
        public string option { get; set; }
        public string matrix { get; set; }
        public string matrix_name { get; set; }
        public string product { get; set; }
        public int batch_id { get; set; }
        public int batch_item_id { get; set; }
        public string batch_name { get; set; }
        public string status { get; set; }
        public string environment { get; set; }
        public int Matrix_id { get; set; }
        public string entityid { get; set; }
        public string oldprice { get; set; }
        public string newprice { get; set; }
        public string effectivedate { get; set; }
        public string comments { get; set; }
        public string createdby { get; set; }
        public string lastupdatedby { get; set; }
        public string createddttm { get; set; }
        public string lastupddttm { get; set; }
        public string XMLMatrix { get; set; }
        public string XMLProduct { get; set; }
        public string XMLOptions { get; set; }
        public string XMLMatrixData { get; set; }
        public string XMLProductData { get; set; }
        public string pricechange { get; set; }
        public decimal DifferentPrice { get; set; }
        public decimal DifferentPercentage { get; set; }
        public decimal percent { get; set; }
        public Boolean Percentage { get; set; }
        public decimal ChangePercentage { get; set; }
        public string matrix_field { get; set; }
        public string description { get; set; }
        public string changemultiple { get; set; }
        public string UserMarket { get; set; }
        public string xmldata { get; set; }
        public string type { get; set; }
        public string pricesymbol { get; set; }
        public string itemtype { get; set; }

    }
    public class ViewMatrixData
    {
        public string retrievedby { get; set; }
        public string identifier { get; set; }
        public string options { get; set; }
        public string matrix { get; set; }
        public string batch_id { get; set; }
        public string batch_name { get; set; }
        public string status { get; set; }
        public string entityid { get; set; }
        public string xmldata { get; set; }
        public string oldprice { get; set; }
        public string newprice { get; set; }
        public string effectivedate { get; set; }
        public string createdby { get; set; }
        public string createddttm { get; set; }
        public string pricechange { get; set; }
        public decimal DifferentPrice { get; set; }
        public decimal DifferentPercentage { get; set; }
        public decimal percent { get; set; }
        public string Matrix_Name { get; set; }
        public string UserId { get; set; }
        public string Field_Name { get; set; }
        public string count { get; set; }
        public int matrix_id { get; set; }
        public string matrix_field { get; set; }
        public string namecheck { get; set; }
        public string description { get; set; }
        public string type { get; set; }
        public string itemtype { get; set; }
        public List<UserMarket> UseMarkets { get; set; }
        public List<MultipleField> MultipleFields { get; set; }
        public string usermarket { get; set; }
        public string approverID { get; set; }
        public int incrementid { get; set; }
        public Boolean percentage { get; set; }
        public decimal changepercentage { get; set; }
        public string pricesymbol { get; set; }
    }
    public class UserMarket
    {
        public string UserMarketName { get; set; }
    }
    public class MultipleField
    {
        public string Field_Name { get; set; }
    }
    public class Person
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
    }
    public class employees
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
    }

    public class RootObject
    {
        public List<employees> employees { get; set; }
    }
    public class SaveMatrixDataLists
    {
        public SaveMatrixData SaveMatrixDataList { get; set; }
        public string Result { get; set; }


    }

    public class CanvasField
    {
        public double Id { get; set; }
        public double RecStartx { get; set; }
        public double RecStarty { get; set; }
        public double RecWidth { get; set; }
        public double RecHeight { get; set; }
    }
    public class CreateBatchApproverIDsModel
    {
        public int BatchId { get; set; }
        public string ApproverIDs { get; set; }
    }
    public class CreateBatchModel
    {
        public int BatchId { get; set; }
        public string BatchName { get; set; }
        public string Status { get; set; }
        public string Comments { get; set; }
        public string EffectiveDate { get; set; }
        public string CurrentStage { get; set; }
        public string Createdby { get; set; }
        public string Lastupdatedby { get; set; }
        public string Appliedby { get; set; }
        public string CreatedDateTime { get; set; }
        public string LastUpdateDateTime { get; set; }
        public string AppliedDateTime { get; set; }
        public string ExpiryDateTime { get; set; }
        public string Result { get; set; }
        public List<Approvedbys> Approvedbydetails { get; set; }
        public int ApproverID { get; set; }
        public int LineCount { get; set; }
        public string Environments { get; set; }
        public string Approvedby { get; set; }
        public string ApproverIDs { get; set; }
        public string UserName { get; set; }
        public string createduserid { get; set; }
        public int batch_process_count { get; set; }
        public int batch_stditem_count { get; set; }
        public int batch_line_count { get; set; }
        public int batch_item_count { get; set; }

    }
    public class EnvironmentModel
    {
        public int environment_id { get; set; }
        public string name { get; set; }
    }
    public class CreateBatchProcessModel
    {
        public string Retrievedby { get; set; }
        public int BatchId { get; set; }
        public string BatchName { get; set; }
        public int ProcessID { get; set; }
        public int EnvironmentId { get; set; }
        public string Environment { get; set; }
        public string EffectiveDate { get; set; }
        public DateTime EffDate { get; set; }
        public string Comments { get; set; }
        public string Status { get; set; }
        public string Runby { get; set; }
        public string RunDateTime { get; set; }
        public string Appliedby { get; set; }
        public string AppliedDateTime { get; set; }
        public string Result { get; set; }
        public Boolean StandardItemsEnvironment { get; set; }
    }

    public class CreateEnvironmentModel
    {
        public int EnvironmentId { get; set; }
        public string Name { get; set; }
        public string Server { get; set; }
        public string DbName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Createdby { get; set; }
        public string CreatedDateTime { get; set; }
        public string Lastupdatedby { get; set; }
        public string LastUpdateDateTime { get; set; }
        public string Result { get; set; }
        public string Url { get; set; }
        public Boolean StandardItemsEnvironment { get; set; }
        public Nullable<int> ProcessId { get; set; }

    }
    public class CreateBatchLineModel
    {
        public int batch_line_id { get; set; }
        public int batch_id { get; set; }
        public string identifier { get; set; }
        public string matrix { get; set; }
        public string retrievedby { get; set; }
        public string effectivedate { get; set; }
        public string Createdby { get; set; }
        public string CreatedDateTime { get; set; }
        public string Lastupdatedby { get; set; }
        public string LastUpdateDateTime { get; set; }
        public string Result { get; set; }
        public string XMLBatchLines { get; set; }
        public string matrix_field { get; set; }

    }
    public class CreateBatchItemModel
    {
        public int incrementid { get; set; }
        public int batch_item_id { get; set; }
        public int batch_line_id { get; set; }
        public int batch_id { get; set; }
        public int Matrix_id { get; set; }
        public string entityid { get; set; }
        public string xmldata { get; set; }
        public string oldprice { get; set; }
        public string newprice { get; set; }
        public string Result { get; set; }
        public string XMLBatchItems { get; set; }
        public int percentage { get; set; }
        public decimal changepercentage { get; set; }
        public string Matrix_name { get; set; }
        public string XMLMultiPriceItems { get; set; }
        public string Field_Name { get; set; }
        public string matrix_field { get; set; }
        public string retrievedby { get; set; }
        public string identifier { get; set; }
        public string option { get; set; }
        public string matrix { get; set; }
        public int batch_id_line { get; set; }
        public string createdby { get; set; }
        public string lastupdatedby { get; set; }
        public string createddttm { get; set; }
        public string lastupddttm { get; set; }
        public string changemultiple { get; set; }
        public string description { get; set; }
        public string usermarket { get; set; }
        public string itemtype { get; set; }
        public string pricesymbol { get; set; }

    }
    public class CreateBatchStandardItemsModel
    {
        public int incrementid { get; set; }
        public int batch_standard_item_id { get; set; }
        public int batch_id { get; set; }
        public int batch_line_id { get; set; }
        public string retrievedby { get; set; }
        public string identifier { get; set; }
        public string code { get; set; }
        public string partnumber { get; set; }
        public string statcode { get; set; }
        public string pricegroup { get; set; }
        public string Unit { get; set; }
        public string cost { get; set; }
        public string costcurrency { get; set; }
        public string oldprice { get; set; }
        public string newprice { get; set; }
        public decimal changepercentage { get; set; }
        public int percentage { get; set; }
        public string createdby { get; set; }
        public string createddttm { get; set; }
        public string lastupdatedby { get; set; }
        public string lastupddttm { get; set; }
        public string changemultiple { get; set; }
        public string description { get; set; }
        public string usermarket { get; set; }
        public string salesoffice { get; set; }
        public string Result { get; set; }
        public string XMLBatchItems { get; set; }
        public string effectivedate { get; set; }
        public List<ViewItemData> batchlines { get; set; }
    }
    public class SaveMultiplePriceItemModel
    {
        public int Matrix_id { get; set; }
        public string entity { get; set; }
        public decimal newprice { get; set; }
        public string Result { get; set; }
        public string Matrix_name { get; set; }
        public string XMLMultiPriceItems { get; set; }
        public string model { get; set; }
        public string xmldata { get; set; }
        public string Field_Name { get; set; }

    }
    public class XMLMultipleField
    {
        public string xmldata { get; set; }
    }
    public class PriceMatricesDataList
    {
        public int IncrementId { get; set; }
        public int MatrixId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Byte DataTypeID { get; set; }
        public string EntityId { get; set; }
        public string MaintenanceDate { get; set; }
        public Byte[] CompiledMatrix { get; set; }
        public Byte Version { get; set; }
        public string DefaultValue { get; set; }
        public string DataType { get; set; }
        public string createddttm { get; set; }
        public string lastupddttm { get; set; }
        public string usermarket { get; set; }

    }

    public class MatricesDimensionsList
    {

        public int MatrixDimensionID { get; set; }
        public int MatrixID { get; set; }
        public int Sequence { get; set; }
        public string Name { get; set; }
        public string ValueExpression { get; set; }
        public string Description { get; set; }
        public Byte DataTypeID { get; set; }
        public string AccessMethod { get; set; }
        public Guid EntityID { get; set; }

        public string DataType { get; set; }
    }

    public class DimensionsData
    {
        [Key]
        public int MatrixDimensionID { get; set; }
        public int MatrixID { get; set; }
        public int Sequence { get; set; }
        public string Name { get; set; }
        public string ValueExpression { get; set; }
        public string Description { get; set; }
        public Byte DataTypeID { get; set; }
        public string AccessMethod { get; set; }
        public Guid EntityID { get; set; }

        public string DataType { get; set; }
    }

    public class UpdateMatrixData
    {
        [Key]
        public string fieldName { get; set; }
        public string value { get; set; }
        public string Table { get; set; }
        public int ID { get; set; }
    }
    public class BatchProcessData
    {
        public int batch_id { get; set; }
        public int process_id { get; set; }
        public int Matrix_id { get; set; }
        public string Matrix_Name { get; set; }
        public string oldprice { get; set; }
        public string newprice { get; set; }
        public string totalvalue { get; set; }
        public string changepercentage { get; set; }
        public string username { get; set; }
        public string XMLData { get; set; }
        public string Result { get; set; }
    }
    public class CreateBatchLogsModel
    {
        public int Id { get; set; }
        public int BatchId { get; set; }
        public string BatchName { get; set; }
        public string Action { get; set; }
        public string Description { get; set; }
        public string TransactionDate { get; set; }
        public string TransactionBy { get; set; }
    }
    public class CreateBatchApproverModel
    {
        public int ID { get; set; }
        public int BatchID { get; set; }
        public int ApproverID { get; set; }
    }
    public class Approvedbys
    {
        public int BatchId { get; set; }
        public int Approvedby { get; set; }
    }
    public class EmailSettings
    {
        public string SourceEmailId { get; set; }
        public string EmailId { get; set; }
        public string SourceName { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public bool UseSSL { get; set; }
        public bool Flag { get; set; }
    }
    public class EmailData
    {
        public string EmailToId { get; set; }
        public string EmailToName { get; set; }
        public string EmailSubject { get; set; }
        public string EmailBody { get; set; }
    }
    public class BatchData
    {
        public int BatchId { get; set; }
        public string BatchName { get; set; }
        public string Status { get; set; }
        public string EffectiveDate { get; set; }
        public string Createdby { get; set; }
        public string CreatedDateTime { get; set; }
        public string Lastupdatedby { get; set; }
        public string LastupdatedDateTime { get; set; }
    }
    public class BatchLineData
    {
        public string RetrieveBy { get; set; }
        public string Identifier { get; set; }
        public string Matrix { get; set; }
        public string EffectiveDate { get; set; }
        public string Createdby { get; set; }
        public string CreatedDateTime { get; set; }
        public string Lastupdatedby { get; set; }
        public string LastupdatedDateTime { get; set; }
    }
    public class BatchItemData
    {
        public string OldPrice { get; set; }
        public string NewPrice { get; set; }
        public string Percentage { get; set; }
        public string MatrixName { get; set; }
        public string Description { get; set; }
        public string Createdby { get; set; }
        public string CreatedDateTime { get; set; }
        public string Lastupdatedby { get; set; }
        public string LastupdatedDateTime { get; set; }
    }
    public class ViewPartNoData
    {
        public string RetrieveBy { get; set; }
        public int batch_id { get; set; }
        public string Company { get; set; }
        public string SalesOffice { get; set; }
        public string PriceCurrency { get; set; }
        public string SoldToBusinessPartner { get; set; }
        public string Item { get; set; }
        public string PriceGroup { get; set; }
        public string StatCode { get; set; }
    }
    public class ResultPartNoData
    {
        public int incrementid { get; set; }
        public string Item { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Unit { get; set; }
        public int DiscountPercentage { get; set; }
        public decimal Cost { get; set; }
        public string CostCurrency { get; set; }
        public string batch_id { get; set; }
        public string newprice { get; set; }
        public decimal changepercentage { get; set; }
        public Boolean percentage { get; set; }
        public string createdby { get; set; }
    }
    public class StatCodes
    {
        public string StatCode { get; set; }
    }
    public class PriceGroupNames
    {
        public string PriceGroup { get; set; }
    }
    public class ViewItemData
    {
        public string retrievedby { get; set; }
        public string identifier { get; set; }
        public string salesoffice { get; set; }
        public string partnumber { get; set; }
        public string pricegroup { get; set; }
        public string statcode { get; set; }
        public string code { get; set; }
        public int batch_id { get; set; }
        public int process_id { get; set; }
        public string batch_name { get; set; }
        public string status { get; set; }
        public string oldprice { get; set; }
        public string newprice { get; set; }
        public string effectivedate { get; set; }
        public string createdby { get; set; }
        public string createddttm { get; set; }
        public string lastupdatedby { get; set; }
        public string lastupddttm { get; set; }
        public string pricechange { get; set; }
        public decimal DifferentPrice { get; set; }
        public decimal DifferentPercentage { get; set; }
        public decimal percent { get; set; }
        public string UserId { get; set; }
        public string count { get; set; }
        public string namecheck { get; set; }
        public string description { get; set; }
        public string type { get; set; }
        public string approverID { get; set; }
        public Boolean percentage { get; set; }
        public decimal ChangePercentage { get; set; }
        public string item { get; set; }
        public string Unit { get; set; }
        public string Cost { get; set; }
        public string CostCurrency { get; set; }
    }
    public class ViewPriceUpdateData
    {
        public string Batch { get; set; }
        public string Retrievedby { get; set; }
        public string Identifier { get; set; }
        public string MatrixName { get; set; }
        public string UserMarket { get; set; }
        public string XmlData { get; set; }
        public string EffectiveDate { get; set; }
        public string OldPrice { get; set; }
        public string NewPrice { get; set; }
        public string PriceChanges { get; set; }
        public string Lastupdatedby { get; set; }
        public string ItemType { get; set; }
        public string Matrix_Id { get; set; }
        public string MatrixField { get; set; }
        public string PriceSymbol { get; set; }
        public bool Percentage { get; set; }
        public decimal ChangePercentage { get; set; }

    }
    public class ViewListItemData
    {
        public string Retrievedby { get; set; }
        public string Identifier { get; set; }
        public string PartNumber { get; set; }
        public string Description { get; set; }
        public string PriceGroup { get; set; }
        public string StatCode { get; set; }
        public string EffectiveDate { get; set; }
        public string Unit { get; set; }
        public string Cost { get; set; }
        public string CostCurrency { get; set; }
        public string OldPrice { get; set; }
        public string NewPrice { get; set; }
    }
    public class CreateSalesOfficeClass
    {
        public string Id { get; set; }
        public string Company { get; set; }
        public string SalesOffice { get; set; }
        public string Description { get; set; }
        public Boolean Active { get; set; }
        public string CreateDate { get; set; }
        public string LastModifiedDate { get; set; }
        public string Name { get; set; }
        public string Result { get; set; }
        public string PriceBook { get; set; }
        public string LNSalesOffice { get; set; }
        public int TotalCount { get; set; }
    }
    public class CreateProcessClass
    {
        public int processid { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string command { get; set; }
        public string status { get; set; }
        public string createdby { get; set; }
        public string createddate { get; set; }
        public string Result { get; set; }
        public string processname { get; set; }
        public int runid { get; set; }
    }
    public class CreateProcessGroupClass
    {
        public int processgroupid { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string status { get; set; }
        public string createdby { get; set; }
        public string createddate { get; set; }
        public string Result { get; set; }
        public string processgroupname { get; set; }
        public int processgroupitemcount { get; set; }
    }
    public class CreateProcessGroupItemsClass
    {
        public int processgroupitemid { get; set; }
        public int processgroupid { get; set; }
        public int processid { get; set; }
        public string processname { get; set; }
        public int runsequenceid { get; set; }
        public string status { get; set; }
        public string createdby { get; set; }
        public string createddate { get; set; }
        public string Result { get; set; }
    }
    public class CreateRunControlPageClass
    {
        public int runid { get; set; }
        public int processgroupid { get; set; }
        public string processgroupname { get; set; }
        public int processid { get; set; }
        public string processname { get; set; }
        public string status { get; set; }
        public int serverid { get; set; }
        public string servername { get; set; }
        public string runby { get; set; }
        public string rundttm { get; set; }
        public string Result { get; set; }
    }
    public class CreateServerClass
    {
        public int serverid { get; set; }
        [Display(Name = "Server Name:")]
        public string servername { get; set; }
        public string description { get; set; }
        public string status { get; set; }
        public string createdby { get; set; }
        public string createddate { get; set; }
        public string Result { get; set; }
        public string servernames { get; set; }
    }
 
    public class StatusClass
    {
        public string Status { get; set; }
        public int Count { get; set; }
    }
    public class UserRoleClass
    {
        public UserRoles? Role { get; set; }
        public int Count { get; set; }
    }
    public class EnvironmentClass
    {
        public string Name { get; set; }
        public int Count { get; set; }
    }
    public class BatchRecordsClass
    {
        public int BatchId { get; set; }
        public string Batch { get; set; }
        public int Count { get; set; }
    }
    public class MatrixDataClass
    {
        public string MatrixId { get; set; }
        public string OldPrice { get; set; }
        public string NewPrice { get; set; }
    }
    public class MultipleNonMultipleClass
    {
        public string Name { get; set; }
        public string Name1 { get; set; }
        public int MultipleCount { get; set; }
        public int NonMultipleCount { get; set; }
    }
    public class BatchMatrixCountClass
    {
        public string Matrix { get; set; }
        public int Count { get; set; }
    }
    public class BatchItemTypeCountClass
    {
        public string ItemType { get; set; }
        public int Count { get; set; }
    }
    public class CreateScheduleClass
    {
        public int scheduleid { get; set; }
        public string name { get; set; }
        public string schedulename { get; set; }
        public string description { get; set; }
        public string status { get; set; }
        public string createdby { get; set; }
        public DateTime createddate { get; set; }
    }
    public class ProcessRequestClass
    {
        public int processrequestid { get; set; }
        public int userid { get; set; }
        public string username { get; set; }
        public int runcontrolid { get; set; }
        public int scheduleid { get; set; }
        public string schedulename { get; set; }
        public int serverid { get; set; }
        public int servername { get; set; }
        public string server { get; set; }
        public string rundate { get; set; }
        public string runtime { get; set; }
        public string status { get; set; }
        public string createdby { get; set; }
        public string createddate { get; set; }
        public int processid { get; set; }
        public int processgroupid { get; set; }
        public string Result { get; set; }
    }
    public class ProcessRequestsClass
    {
        public int processrequestitemid { get; set; }
        public int processrequestid { get; set; }
        public int processid { get; set; }
        public string processname { get; set; }
        public string status { get; set; }
        public string runby { get; set; }
        public string rundatetime { get; set; }
        public string createdby { get; set; }
        public string createddate { get; set; }
        public string Result { get; set; }
    }
}
