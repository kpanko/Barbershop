using System;
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
                .OrderBy(c => c.ArrivalTime);

            return View(waitingCustomers.ToList());
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

                // Hardcoding barbers' names is not ideal but it works for now
                // TODO: we can remove this by looping over all barbers and checking if any match
                //

                var joe = db.Barbers.Where(b => b.Name.Equals("Joe")).FirstOrDefault();
                var gary = db.Barbers.Where(b => b.Name.Equals("Gary")).FirstOrDefault();

                if(string.Equals(preferredBarber, "Joe"))
                {
                    customer.PreferredBarber = joe;
                }
                else if(string.Equals(preferredBarber, "Gary"))
                {
                    customer.PreferredBarber = gary;
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
