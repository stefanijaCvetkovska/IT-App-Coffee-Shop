using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IT_Mvc_App_Coffee_Shop.Models;
using PagedList;

namespace IT_Mvc_App_Coffee_Shop.Controllers
{
    public class DrinksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Drinks
        public ActionResult Index(string sortOrder, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.PriceSort = sortOrder == "price" ? "price_desc" : "price";
            ViewBag.NameSort = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
        
            var drinks = from d in db.Drinks select d;

            switch (sortOrder)
            {
                case "name_desc":
                    drinks = drinks.OrderByDescending(s => s.Name);
                    break;
                case "price":
                    drinks = drinks.OrderBy(s => s.Price);
                    break;
                case "price_desc":
                    drinks = drinks.OrderByDescending(s => s.Price);
                    break;
                default: 
                    drinks = drinks.OrderBy(s => s.Name);
                    break;
            }
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            return View(drinks.ToPagedList(pageNumber, pageSize));
        }

        // GET: Drinks/Details/5
         public ActionResult Details(int? id)
         {
             if (id == null)
             {
                 return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
             }
             Drink drink = db.Drinks.Find(id);
             if (drink == null)
             {
                 return HttpNotFound();
             }
             return View(drink);
         }

        // GET: Drinks/Create
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Drinks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Create([Bind(Include = "Id,Name,Price,Description")] Drink drink)
        {
            if (ModelState.IsValid)
            {
                db.Drinks.Add(drink);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(drink);
        }

        // GET: Drinks/Edit/5
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Drink drink = db.Drinks.Find(id);
            if (drink == null)
            {
                return HttpNotFound();
            }
            return View(drink);
        }

        // POST: Drinks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Edit([Bind(Include = "Id,Name,Price,Description")] Drink drink)
        {
            if (ModelState.IsValid)
            {
                db.Entry(drink).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(drink);
        }

        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Delete(int id)
        {
            Drink drink = db.Drinks.Find(id);
            db.Drinks.Remove(drink);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DailyDiscount()
        {
            var drink = GetDailyDiscount();
            return PartialView("_DailyDiscount", drink);
        }

        private Drink GetDailyDiscount()
        {
            var drink = db.Drinks.OrderBy(d => Guid.NewGuid()).First();
            float discount = drink.Price * 0.2f;
            drink.Price -= discount;
            return drink;
        }

        //[Authorize(Roles = "User, Admin, Manager")]
        public ActionResult MakeOrder(int id)
        {
            var model = new MakeOrder();
            model.DrinkId = id;
            model.Brands = db.Brands.ToList();
            model.Stores = db.Stores.ToList();
            var drink = db.Drinks.Find(model.DrinkId);
            var price = drink.Price;
            ViewBag.Drink = drink.Name;
            ViewBag.Price = price;
            return View(model);
        }

        [HttpPost]
        //[Authorize(Roles = "User, Admin, Manager")]
        public ActionResult MakeOrder(MakeOrder model)
        {
            if (ModelState.IsValid)
            {
                return View("ForDelivery");
            }
            return View("MakeOrder");
        }

        public ActionResult ForDelivery(MakeOrder model)
        {
            var drink = db.Drinks.Find(model.DrinkId);

            ViewBag.Phone = model.Phone;
            ViewBag.Address = model.Address;
            ViewBag.Drink = drink.Name;
            return View();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
