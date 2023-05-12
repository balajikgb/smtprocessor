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
    [Table("processrequests")]
    public class ProcessRequestsModel
    {
        [Key]
        public int processrequestitemid { get; set; }
        public int processrequestid { get; set; }
        public int processid { get; set; }
        public string status { get; set; }
        public string runby { get; set; }
        public DateTime rundatetime { get; set; }
        public string createdby { get; set; }
        public DateTime createddate { get; set; }
    }
}

