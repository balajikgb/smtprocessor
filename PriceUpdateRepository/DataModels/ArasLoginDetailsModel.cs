using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PriceUpdateRepository.DataModels
{
    [Table("ArasLoginDetails")]
    public class ArasLoginDetailsModel
    {
        [Key]
        public int Id { get; set; }
        public string ArasUserID { get; set; }
        public string ArasUserName { get; set; }
        public string ArasPassword { get; set; }
        public string BRDSpec { get; set; }
        public DateTime DateTime { get; set; }

    }
}
