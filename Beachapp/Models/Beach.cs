using System;
using System.ComponentModel.DataAnnotations;


namespace Beachapp.Models
{
    public class Beach
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
        public string PhotoPath { get; set; }
        [Required]
        public string Location{get;set;}
        public string PosterId{get;set;}
        public string Poster{get;set;}
        public DateTimeOffset CreatedAt{get;set;}
    }
}
