using GamesRental.Entities.Base;
using System;
using System.Collections.Generic;

namespace GamesRental.Entities
{
    public class Friend : BaseEntity
    {
        public virtual string Name { get; set; }

        public virtual string Email { get; set; }

        public virtual ICollection<Rental> Rents { get; set; }
    }
}
