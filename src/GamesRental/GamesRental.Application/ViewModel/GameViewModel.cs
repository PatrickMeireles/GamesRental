using GamesRental.Entities.Enuns;
using GamesRental.Util;
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

        public Boolean HaveRents { get; set; }

        public GameViewModel() { }

        public GameViewModel(int _id, string _name, string _description, int _genre, DateTime? _releaseDate, bool _avaliable, bool _haveRents)
        {
            this.Id = _id;
            this.Name = _name;
            this.Description = _description;
            this.Genre = _genre;
            this.GenreDescription = ((GenreGame)_genre).GetDescription();
            this.ReleaseDate = _releaseDate;
            this.Avaliable = _avaliable;
            this.HaveRents = _haveRents;
        }
    }
}
