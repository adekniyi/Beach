using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;


namespace Beachapp.Data
{
         public class ApplicationUser : IdentityUser
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "User Picture")]
        public string UserPicture { get; set; }
        [Required]
        public string Location{get;set;}
    }
}
