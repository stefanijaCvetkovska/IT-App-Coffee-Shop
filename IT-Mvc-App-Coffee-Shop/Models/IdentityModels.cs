﻿using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IT_Mvc_App_Coffee_Shop.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<IT_Mvc_App_Coffee_Shop.Models.Brand> Brands { get; set; }

        public System.Data.Entity.DbSet<IT_Mvc_App_Coffee_Shop.Models.Store> Stores { get; set; }

        public System.Data.Entity.DbSet<IT_Mvc_App_Coffee_Shop.Models.Drink> Drinks { get; set; }

        public System.Data.Entity.DbSet<IT_Mvc_App_Coffee_Shop.Models.Profile> Profiles { get; set; }
    }
}