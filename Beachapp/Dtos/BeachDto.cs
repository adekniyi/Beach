using System;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;


namespace Beachapp.Dtos
{
    public class BeachDto
    {
         public int BeachId { get; set; }
        [Required]

        public string BeachName { get; set; }
        [Required]
        [Display(Name = "Beach Details")]
        public string BeachDetails { get; set; }
        [Required]
        [Display(Name = "Beach Picture")]
        public string BeachPicture { get; set; }

        public IFormFile ImageFile { get; set; }
    }
}
