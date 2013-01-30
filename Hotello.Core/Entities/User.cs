using System;
using System.Collections.Generic;

namespace Hotello.Core.Entities
{
    public class User : Entity
    {
        public virtual string Forename { get; set; }
        public virtual string Surname { get; set; }
        public virtual string Email { get; set; }
        public virtual string Address { get; set; }
        public virtual string PostCode { get; set; }
        public virtual DateTime Dob { get; set; }
        public virtual DateTime Created { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
