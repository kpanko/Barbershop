using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Barbershop.Models
{
    /// <summary>
    /// Represents either Joe or Gary, our two barbers
    /// </summary>
    public class Barber
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}