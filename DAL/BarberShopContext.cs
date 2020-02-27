using Barbershop.Models;
using System.Data.Entity;

namespace Barbershop.DAL
{
    public class BarberShopContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Barber> Barbers { get; set; }
    }
}