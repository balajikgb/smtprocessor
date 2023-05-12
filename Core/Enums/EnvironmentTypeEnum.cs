using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Enums
{
    public enum EnvironmentTypeEnum
    {
        [Display(Name = "10.5")]
        Environment_10_5,
        [Display(Name = "10.7")]
        Environment_10_7
    }
}
