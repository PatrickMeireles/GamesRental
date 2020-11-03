using GamesRental.Entities.Base;
using System;
using System.Collections.Generic;

namespace GamesRental.Entities
{
    public class Friend : BaseEntity
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public ICollection<Rental> Rents { get; set; }
    }
}
