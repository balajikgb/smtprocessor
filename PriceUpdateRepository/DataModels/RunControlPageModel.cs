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
    [Table("runcontrolpage")]
    public class RunControlPageModel
    {
        [Key]
        public int runid { get; set; }
        public int processgroupid { get; set; }
        public int processid { get; set; }
        public string status { get; set; }
        public int serverid { get; set; }
        public string runby { get; set; }
        public DateTime rundttm { get; set; }
    }
}

