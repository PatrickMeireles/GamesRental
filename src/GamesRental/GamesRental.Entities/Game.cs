using GamesRental.Entities.Base;
using System;
using System.Collections.Generic;

namespace GamesRental.Entities
{
    public class Game : BaseEntity
    {
        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        //Enum - GenreGame
        public virtual int Genre { get; set; }

        public virtual DateTime? ReleaseDate { get; set; }

        public virtual ICollection<Rental> Rents { get; set; }
    }
}
