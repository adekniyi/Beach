using System;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;


namespace Beachapp.Dtos
{
    public class BeachDto
    {
        public int BeachId { get; set; }
        [Required]
        [Display(Name = "Beach Name")]
        public string BeachName { get; set; }
        [Required]
        [Display(Name = "Beach Details")]
        public string BeachDetails { get; set; }
        [Required]
        [Display(Name = "Beach Picture")]
        public IFormFile BeachPicture { get; set; }
       // [Required]
        public string Location{get;set;}
        //public string PosterId{get;set;}
    }
}
