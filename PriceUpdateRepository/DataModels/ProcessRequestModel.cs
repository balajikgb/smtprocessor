using Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceUpdateRepository.DataModels
{
    [Table("processrequest")]
    public class ProcessRequestModel
    {
        [Key]
        public int processrequestid { get; set; }
        public int userid { get; set; }
        public int runcontrolid { get; set; }
        public int scheduleid { get; set; }
        public int servername { get; set; }
        public string rundate { get; set; }
        public string runtime { get; set; }
        public string status { get; set; }
        public string createdby { get; set; }
        public DateTime createddate { get; set; }
        public int processid { get; set; }
        public int processgroupid { get; set; }
    }
}

