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

namespace ArasPLMWebAp.Models
{
    public class UserEdition
    {
        public int Id { get; set; }
        public bool Active { get; set; }
        public string UserName { get; set; }
        public string OldUserName { get; set; }

        [Display(Name = "First Name:")]
        [StringLength(20, MinimumLength = 3)]
        public string FirstName { get; set; }

        [Display(Name = "Last Name:")]
        [StringLength(20, MinimumLength = 3)]
        public string LastName { get; set; }

        [Display(Name = "User Email:")]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string UserEmail { get; set; }

        [Display(Name = "Password:")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,32}$", ErrorMessage = "Password must be between 8 and 32 characters and contain one uppercase letter, one lowercase letter, one digit and one special character.")]
        [StringLength(32, ErrorMessage = "Must be between 8 and 32 characters", MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Approve Password:")]
        [StringLength(32, ErrorMessage = "Must be between 8 and 32 characters", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ApprovePassword { get; set; }
        [Display(Name = "Comment:")]
        [StringLength(500)]
        public string Comment { get; set; }

        public UserRoles? UserRole { get; set; }

        public Language? Language { get; set; }

        public string GenericID { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreateDate { get; set; } = DateTime.Now;

        //public ICollection<ViewedOrderModel> ViewedOrders { get; set; }

        //public IList<EditUserBaans> UserBaans { get; set; }
        //public IList<EditUserCompanies> UserCompanies { get; set; }
        //public IList<EditUserCompanies> Customers { get; set; }
        public string SelectedCompaniesid { get; set; }
        public string SelectedCompaniesName { get; set; }
        public string SelectedCustomersid { get; set; }
        public string SelectedCustomersName { get; set; }

        [Display(Name = "Environment:")]
        public string Environment { get; set; }

        [Display(Name = "RetrievedBy:")]

        public string RetrievedBy { get; set; }

        [Display(Name = "Matrix:")]
        public string Matrix { get; set; }

        [Display(Name = "Product:")]
        public string Product { get; set; }
        [Display(Name = "Options:")]
        public string Options { get; set; }

        [Display(Name = "DbName:")]
        public string DbName { get; set; }
        [Display(Name = "Server:")]
        public string Server { get; set; }
    }
}
