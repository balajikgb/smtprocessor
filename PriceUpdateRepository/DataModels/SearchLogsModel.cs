using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PriceUpdateRepository.DataModels
{
    [Table("SearchLogs")]
    public class SearchLogsModel
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Actions { get; set; }
        public DateTime DateTime { get; set; }
        public string PartNumber { get; set; }

    }
}
