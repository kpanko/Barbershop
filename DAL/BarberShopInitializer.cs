using System;
using System.Data.Entity;
using System.Collections.Generic;
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
            var joe = new Barber { Name = "Joe" };
            var gary = new Barber { Name = "Gary" };

            var barbers = new List<Barber> { joe, gary };
            barbers.ForEach(b => context.Barbers.Add(b));
            context.SaveChanges();

            var customers = new List<Customer>
            {
                new Customer { FirstName = "Donald", LastName = "Duck",
                    PhoneNumber = "8605551212",
                    ArrivalTime = DateTime.Parse("2020-2-26 4:00PM") },
                new Customer { FirstName = "Mickey", LastName = "Mouse",
                    PhoneNumber = "7165551212",
                    ArrivalTime = DateTime.Parse("2020-2-26 4:01PM") },
                new Customer { FirstName = "Ilove", LastName = "Joe",
                    PhoneNumber = "2125551212",
                    ArrivalTime = DateTime.Parse("2020-2-26 4:02PM"),
                    PreferredBarber = joe },
                new Customer { FirstName = "Mister", LastName = "Jones",
                    PhoneNumber = "6125551212",
                    ArrivalTime = DateTime.Parse("2020-2-26 4:08PM"), },
                new Customer { FirstName = "Wile", LastName = "Coyote",
                    PhoneNumber = "3135551212",
                    ArrivalTime = DateTime.Parse("2020-2-26 4:10PM"), },
                new Customer { FirstName = "Ilove", LastName = "Gary",
                    PhoneNumber = "8005551212",
                    ArrivalTime = DateTime.Parse("2020-2-26 4:02PM"),
                    PreferredBarber = gary },
            };
            customers.ForEach(c => context.Customers.Add(c));
            context.SaveChanges();
        }
    }
}