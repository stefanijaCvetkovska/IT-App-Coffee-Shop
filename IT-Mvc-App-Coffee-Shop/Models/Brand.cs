using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IT_Mvc_App_Coffee_Shop.Models
{
    public class Brand
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display (Name = "Brand")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "About Us")]
        public string About { get; set; }
        [Required]
        [Display(Name = "Logo")]
        public string LogoUrl { get; set; }
    }
}