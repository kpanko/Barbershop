using Barbershop.Models;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Barbershop.DAL
{
    public class BarberShopContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Barber> Barbers { get; set; }
    }
}