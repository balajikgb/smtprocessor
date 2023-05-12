using Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataModels
{
    public class UserRegistration
    {
        public int Id { get; set; }
        public bool Active { get; set; }

        [Display(Name = "First Name:")]
        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string FirstName { get; set; }

        [Display(Name = "Last Name:")]
        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string LastName { get; set; }

        [Display(Name = "Company Name:")]
        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string CompanyName { get; set; }

        [Display(Name = "Company Addres:")]
        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string CompanyAddres { get; set; }

        [Display(Name = "User Email:")]
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string UserEmail { get; set; }

        [Required]
        public DateTime CreateDate { get; set; } = DateTime.Now;

        [Display(Name = "Comment:")]
        [StringLength(500)]
        public string Comment { get; set; }

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
