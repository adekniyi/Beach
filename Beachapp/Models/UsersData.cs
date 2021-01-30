using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication;

namespace Beachapp.Models
{
    public class UsersData
    {
       public int UsersDataId { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "User Picture")]
        public string UserPicture { get; set; }
        [Required]
        public string Location{get;set;}
        public string Email{get;set;}
    }
}
