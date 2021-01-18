﻿using System;
using System.ComponentModel.DataAnnotations;


namespace Beachapp.Models
{
    public class Beach
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

        public string ImageFile { get; set; }
    }
}
