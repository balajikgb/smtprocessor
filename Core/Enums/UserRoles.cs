using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Enums
{
    public enum UserRoles
    {
        //[Display(Name = "Customer")]
        //Customer,
        [Display(Name = "Agent")]
        Rep,
        [Display(Name = "Admin")]
        Admin,
        [Display(Name = "Manager")]
        Manager,
        [Display(Name = "Editor")]
        Editor,
        [Display(Name = "Viewer")]
        Viewer,
    }

    public enum Language
    { 
        [Display(Name = "English")]
        English
        //[Display(Name = "Hindi")]
        //Hindi,
        //[Display(Name = "Romanian")]
        //Romanian,
        //[Display(Name = " Simplified_Chinese")]
        //Simplified_Chinese,
        //[Display(Name = "Portuguese")]
        //Portuguese,
        //[Display(Name = "Kannada")]
        //Kannada
    }
    public enum Identifiers
    {
        [Display(Name = "PROMOTER DIRECTOR")]
        English,
        [Display(Name = "Hindi")]
        Hindi,
        [Display(Name = "Romanian")]
        Romanian,
        [Display(Name = " Simplified_Chinese")]
        Simplified_Chinese,
        [Display(Name = "Portuguese")]
        Portuguese,
        [Display(Name = "Kannada")]
        Kannada
    }
    public enum Status
    {
        [Display(Name = "Active")]
        Active,
        [Display(Name = "In Active")]
        InActive,
    }

}
