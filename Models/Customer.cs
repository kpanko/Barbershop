using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Barbershop.Models
{
    /// <summary>
    ///  Represents a waiting customer for the queue
    /// </summary>
    public class Customer
    {
        public int ID { get; set; }

        /// <summary>
        /// The person's last name
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// The person's first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Represents when they arrived and began waiting
        /// </summary>
        public DateTime ArrivalTime { get; set; }

        /// <summary>
        /// False if waiting, true if no longer waiting
        /// </summary>
        public bool served { get; set; }

        /// <summary>
        /// references the Barber this person is waiting for;
        /// null if anybody is fine
        /// </summary>
        public Barber PreferredBarber { get; set; }
    }
}