using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Barbershop.DAL;
using Barbershop.Models;

namespace Barbershop.Controllers
{
    public class CustomersController : Controller
    {
        private readonly BarberShopContext db = new BarberShopContext();

        // GET: Customers
        public ActionResult Index(string PhoneNumber)
        {
            // If user has put in a phone number, look that user up
            //
            if(! string.IsNullOrWhiteSpace(PhoneNumber)) {
                var user = db.Customers.Where(c => c.PhoneNumber == PhoneNumber).FirstOrDefault();

                return RedirectToAction("Details", new { user.ID });
            }

            // Show a list of all the unserved customers
            // in order by who arrived first
            //
            var waitingCustomers = db.Customers
                .Where(c => c.served == false)
                .OrderBy(c => c.ArrivalTime)
                .ToList();

            AddWaitTimes(waitingCustomers);

            return View(waitingCustomers);
        }

        private void AddWaitTimes(List<Customer> waitingCustomers)
        {
            int waitTime = 0;

            foreach (var cust in waitingCustomers)
            {
                if(waitTime == 0)
                {
                    cust.waitTime = "No wait";
                }
                else
                {
                    cust.waitTime = string.Format("{0} minutes", waitTime);
                }

                waitTime += 15;
            }
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "LastName,FirstName,PhoneNumber")] Customer customer,
            string preferredBarber)
        {
            if (ModelState.IsValid)
            {
                // Incoming customer begins waiting now
                //
                customer.ArrivalTime = DateTime.Now;
                customer.served = false;

                var barbers = db.Barbers.ToList();

                foreach (var barber in barbers)
                {
                    if(string.Equals(preferredBarber, barber.Name))
                    {
                        customer.PreferredBarber = barber;
                    }
                }

                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LastName,FirstName,PhoneNumber")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/InChair/5
        public ActionResult InChair(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Customer customer = db.Customers.Find(id);

            if (customer == null)
            {
                return HttpNotFound();
            }

            customer.served = true;
            db.SaveChanges();

            return RedirectToAction("Index");
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
