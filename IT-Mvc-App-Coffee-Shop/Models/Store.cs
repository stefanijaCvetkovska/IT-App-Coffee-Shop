using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IT_Mvc_App_Coffee_Shop.Models
{
    public class Store
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Store Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Store Location")]
        public string Location { get; set; }
        [Required]
        public double Rating { get; set; }
        [Required]
        [Display(Name = "Place Image")]
        public string LocationImg { get; set; }
    }
}