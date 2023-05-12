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
    [Table("processgroup")]
    public class ProcessGroupModel
    {
        [Key]
        public int processgroupid { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string status { get; set; }
        public string createdby { get; set; }
        public DateTime createddate { get; set; }
    }
}

