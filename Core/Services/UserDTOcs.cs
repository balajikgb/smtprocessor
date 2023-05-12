using Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Services
{
    public class UserModel
    {
        [Required]
        public string UserEmail { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public class UserDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public UserRoles? Role { get; set; }
        public bool IsUserActive { get; set; }

        public ICollection<UserCompany> UserCompanies { get; set; }
        public bool IsAccessForbidden { get; set; }
        public string Accepted { get; set; }

        public Language? Language { get; set; }
        public string GenericID { get; set; }
        public string Page { get; set; }

        public string Action { get; set; }
    }


    public class UserCompany
    {
        public string CompanyId { get; set; }
        public string CompanyName { get; set; }
        public int? SortOrder { get; set; }
    }
    public class ButtonAccessList
    {
        public string Save { get; set; }
        public string Edit { get; set; }
        public string Create { get; set; }

        public string View { get; set; }

        public string QuickApply { get; set; }
        public string Publish { get; set; }
        public string Delete { get; set; }
        public string MakeChanges { get; set; }
        public string Description { get; set; }
    }
}
