using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Core.Enums;
using Microsoft.EntityFrameworkCore;

namespace PriceUpdateRepository.DataModels
{
    [Table("Users")]
    public class UserModel
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }

        [DefaultValue(true)]
        public bool Active { get; set; }

        [MaxLength(20)]
        public string FirstName { get; set; }

        [MaxLength(20)]
        public string SecondName { get; set; }

        [MaxLength(60)]
        public string CompanyName { get; set; }

        [MaxLength(60)]
        public string CompanyAddres { get; set; }

        [MaxLength(13)]
        [Phone]
        public string UserPhone { get; set; }

        public UserRoles? UserRole { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreateDate { get; set; } = DateTime.Now;

        [MaxLength(500)]
        public string Comment { get; set; }

        [NotMapped]
        public bool IsAccessForbidden
        {
            get { return IsAccessForbiddenInt > 0; }
            set { IsAccessForbiddenInt = value ? 1 : 0; }
        }

        [Column("IsAccessForbidden")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int IsAccessForbiddenInt { get; set; }

        public string Accepted { get; set; }

        public Language? Language { get; set; }

        public string GenericID { get; set; }
    }
}
