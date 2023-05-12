using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ArasPLMWebAp.Models
{
    public class EditPasswordModel
    {
        public string UserName { get; set; }

        [Display(Name = "Password:")]
        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,32}$", ErrorMessage = "Password must be between 8 and 32 characters and contain one uppercase letter, one lowercase letter, one digit and one special character.")]
        [StringLength(32, ErrorMessage = "Must be between 8 and 32 characters", MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Approve Password:")]
        [Required(ErrorMessage = "Confirm Password is required")]
        [StringLength(32, ErrorMessage = "Must be between 8 and 32 characters", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ApprovePassword { get; set; }
    }
}
