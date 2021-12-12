using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IT_Mvc_App_Coffee_Shop.Models
{
    public class MakeOrder
    {
        public int DrinkId { get; set; }
        public int BrandId { get; set; }
        public int StoreId { get; set; }
        public virtual List<Brand> Brands { get; set; }
        public virtual List<Store> Stores { get; set; }
        [Required(ErrorMessage = "Phone field cannot be empty!")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Address field cannot be empty")]
        public string Address { get; set; }
        public MakeOrder()
        {
            Brands = new List<Brand>();
            Stores = new List<Store>();
        }
    }
}