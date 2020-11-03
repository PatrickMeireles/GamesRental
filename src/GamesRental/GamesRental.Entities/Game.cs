using GamesRental.Entities.Base;
using System;
using System.Collections.Generic;

namespace GamesRental.Entities
{
    public class Game : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        //Enum - GenreGame
        public int Genre { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public ICollection<Rental> Rents { get; set; }
    }
}
