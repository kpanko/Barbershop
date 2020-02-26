using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Barbershop.Models;

namespace Barbershop.DAL
{
    public class BarberShopInitializer
        : DropCreateDatabaseIfModelChanges<BarberShopContext>
    {
        /// <summary>
        /// Set up the database with some example data
        /// </summary>
        /// <param name="context">the barbershop database context</param>
        protected override void Seed(BarberShopContext context)
        {
            var barbers = new List<Barber>
            {
                new Barber { Name = "Joe" },
                new Barber { Name = "Gary" },
            };
            barbers.ForEach(b => context.Barbers.Add(b));
            context.SaveChanges();

            var customers = new List<Customer>
            {
                new Customer { FirstName = "Donald", LastName = "Duck",
                    ArrivalTime = DateTime.Parse("2020-2-26 4:00PM") },
                new Customer { FirstName = "Mickey", LastName = "Mouse",
                    ArrivalTime = DateTime.Parse("2020-2-26 4:01PM") },
            };
            customers.ForEach(c => context.Customers.Add(c));
            context.SaveChanges();
        }
    }
}