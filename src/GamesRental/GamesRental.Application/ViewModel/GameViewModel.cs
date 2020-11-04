using System;
using System.Collections.Generic;
using System.Text;

namespace GamesRental.Application.ViewModel
{
    public class GameViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        ////Enum - GenreGame
        public int Genre { get; set; }

        public string GenreDescription { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public bool Avaliable { get; set; }
    }
}
