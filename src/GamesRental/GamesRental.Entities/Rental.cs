using GamesRental.Entities.Base;
using System;

namespace GamesRental.Entities
{
    public class Rental : BaseEntity
    {
        public DateTime Date { get; set; }

        public DateTime? DateEstimated { get; set; }

        public DateTime? DateFinish { get; set; }

        public int IdFriend { get; set; }

        public Friend Friend { get; set; }

        public int IdGame { get; set; }

        public Game Game { get; set; }
    }
}
