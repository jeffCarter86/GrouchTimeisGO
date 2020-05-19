using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrashMan.Data;
using TrashMan.Models;

namespace TrashPickUp_Project.Controllers
{
    [Authorize(Roles = "Customer")]
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context1;

        public CustomersController(ApplicationDbContext context1)
        {
            _context1 = context1;
        }
        // GET: Customer
        public ActionResult Index(Customer customer1)
        {

            var customer = _context1.Users.Where(u => u.Email == User.Identity.Name).SingleOrDefault();

            var id = customer.Id;
            customer1 = _context1.Customers.Where(c => c.IdentityUserId == id).SingleOrDefault();
            if (_context1.Customers.Where(e => e.IdentityUserId == id).SingleOrDefault() == null)
            {
                return View("Create");
            }
            else
            {
                return View(customer1);
            }
        }
        public ActionResult MyScheduledPickup()
        {
            CustomerSchedPickUp item = _context1.CustomerSchedPickUps.Where(c => c.IdentityUserId == this.User.FindFirstValue(ClaimTypes.NameIdentifier)).SingleOrDefault();
            if (item == null)
            {
                return View("CreateScheduledPickUp");
            }
            else
            {
                return View(item);
            }

        }
        public ActionResult Balance(Customer customer)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            customer = _context1.Customers.Where(c => c.IdentityUserId == userId).SingleOrDefault();
            return View(customer);
        }
        public ActionResult CreateScheduledPickUp()
        {


            return View();


        }

        // GET: Customer/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer item)
        {
            try
            {

                // TODO: Add insert logic here
                Customer newCustomer = new Customer();
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                item.IdentityUserId = userId;
                _context1.Customers.Add(item);
                _context1.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateScheduledPickUp(CustomerSchedPickUp customerSchedPickUp)
        {
            try
            {
                // TODO: Add insert logic here
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var customer = _context1.Customers.Where(c => c.IdentityUserId == userId).SingleOrDefault();

                customerSchedPickUp.IdentityUserId = userId;
                customerSchedPickUp.Address = customer.Address;
                customerSchedPickUp.City = customer.City;
                customerSchedPickUp.ZipCode = customer.ZipCode;
                _context1.CustomerSchedPickUps.Add(customerSchedPickUp);
                _context1.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Customer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CustomerSchedPickUp item)
        {
            try
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var temp = _context1.CustomerSchedPickUps.Where(c => c.IdentityUserId == userId).SingleOrDefault();
                temp.Address = item.Address;
                temp.DayOfWeek = item.DayOfWeek;
                temp.SuspendedStartDate = item.SuspendedStartDate;
                temp.SuspendedEndDate = item.SuspendedEndDate;

                _context1.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Customer/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}