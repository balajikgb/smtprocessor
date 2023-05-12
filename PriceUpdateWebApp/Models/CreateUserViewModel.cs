using Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArasPLMWebAp.Models
{
    public class CreateUserViewModel
    {
        public int Id { get; set; }
        public bool Active { get; set; }
        public string UserName { get; set; }

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

        [Display(Name = "Comment:")]
        [StringLength(500)]
        public string Comment { get; set; }

        public Language? Language { get; set; }

        public string GenericID { get; set; }

        public UserRoles? UserRole { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreateDate { get; set; } = DateTime.Now;

    }
}
