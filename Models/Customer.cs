using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        /// The person's phone number
        /// </summary>
        public string PhoneNumber { get; set; }

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
        [Display(Name="Preferred Barber")]
        public virtual Barber PreferredBarber { get; set; }

        /// <summary>
        /// Wait time
        /// </summary>
        [Display(Name ="Wait Time")]
        [NotMapped]
        public string waitTime { get; set; }
    }
}