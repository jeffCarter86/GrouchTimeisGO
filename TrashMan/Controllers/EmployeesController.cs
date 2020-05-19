using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using TrashMan.Models;
using TrashMan.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace TrashMan.Controllers

{

    [Authorize(Roles = "Employee")]
    public class EmployeesController : Controller
    {
        private ApplicationDbContext _context1;
        public IEnumerable<CustomerSchedPickUp> PUs;


        public EmployeesController(ApplicationDbContext context1)
        {
            _context1 = context1;






        }
        public ActionResult CustomerProfile(string item)
        {
            Customer customer = _context1.Customers.Where(c => c.IdentityUserId == item).SingleOrDefault();
            return View(customer);
        }
        public ActionResult Confirm(string item)
        {
            Customer customer = _context1.Customers.Where(c => c.IdentityUserId == item).SingleOrDefault();
            customer.Balance += 30;
            _context1.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult MondayPickUps()
        {

            var id = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var employee = _context1.Employees.Where(e => e.IdentityUserId == id).SingleOrDefault();
            var zip = employee.ZipCode;
            var Dow = DayOfWeek.Monday.ToString();
            PUs = _context1.CustomerSchedPickUps.Where(e => e.ZipCode == zip && e.DayOfWeek == Dow);
            return View(PUs);

        }
        public ActionResult TuesdayPickUps()
        {

            var id = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var employee = _context1.Employees.Where(e => e.IdentityUserId == id).SingleOrDefault();
            var zip = employee.ZipCode;
            var Dow = DayOfWeek.Tuesday.ToString();
            PUs = _context1.CustomerSchedPickUps.Where(e => e.ZipCode == zip && e.DayOfWeek == Dow);
            return View(PUs);
        }
        public ActionResult WednesdayPickUps()
        {

            var id = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var employee = _context1.Employees.Where(e => e.IdentityUserId == id).SingleOrDefault();
            var zip = employee.ZipCode;
            var Dow = DayOfWeek.Wednesday.ToString();
            PUs = _context1.CustomerSchedPickUps.Where(e => e.ZipCode == zip && e.DayOfWeek == Dow);
            return View(PUs);
        }
        public ActionResult ThursdayPickUps()
        {

            var id = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var employee = _context1.Employees.Where(e => e.IdentityUserId == id).SingleOrDefault();
            var zip = employee.ZipCode;
            var Dow = DayOfWeek.Thursday.ToString();
            PUs = _context1.CustomerSchedPickUps.Where(e => e.ZipCode == zip && e.DayOfWeek == Dow);
            return View(PUs);
        }
        public ActionResult FridayPickUps()
        {

            var id = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var employee = _context1.Employees.Where(e => e.IdentityUserId == id).SingleOrDefault();
            var zip = employee.ZipCode;
            var Dow = DayOfWeek.Friday.ToString();
            PUs = _context1.CustomerSchedPickUps.Where(e => e.ZipCode == zip && e.DayOfWeek == Dow);
            return View(PUs);
        }
        public ActionResult SaturdayPickUps()
        {

            var id = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var employee = _context1.Employees.Where(e => e.IdentityUserId == id).SingleOrDefault();
            var zip = employee.ZipCode;
            var Dow = DayOfWeek.Saturday.ToString();
            PUs = _context1.CustomerSchedPickUps.Where(e => e.ZipCode == zip && e.DayOfWeek == Dow);
            return View(PUs);
        }
        public ActionResult SundayPickUps()
        {

            var id = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var employee = _context1.Employees.Where(e => e.IdentityUserId == id).SingleOrDefault();
            var zip = employee.ZipCode;
            var Dow = DayOfWeek.Sunday.ToString();
            PUs = _context1.CustomerSchedPickUps.Where(e => e.ZipCode == zip && e.DayOfWeek == Dow);
            return View(PUs);
        }

        public ActionResult ScheduledPickUps()
        {
            return View();
        }

        // GET: Employees
        public ActionResult Index()
        {





            var id = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (_context1.Employees.Where(e => e.IdentityUserId == id).SingleOrDefault() == null)
            {
                return View("Create");
            }
            else
            {

                var employee = _context1.Employees.Where(e => e.IdentityUserId == id).SingleOrDefault();
                var zip = employee.ZipCode;
                var Dow = DateTime.Today.DayOfWeek.ToString();
                PUs = _context1.CustomerSchedPickUps.Where(e => e.ZipCode == zip && e.DayOfWeek == Dow);
                return View(PUs);
            }

        }

        // GET: Employees/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee item)
        {
            try
            {
                // TODO: Add insert logic here
                Employee newEmployee = new Employee();
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                newEmployee.IdentityUserId = userId;
                newEmployee.Name = item.Name;
                newEmployee.Address = item.Address;
                newEmployee.City = item.City;
                newEmployee.ZipCode = item.ZipCode;
                _context1.Employees.Add(newEmployee);
                _context1.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Employees/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Employees/Delete/5
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

