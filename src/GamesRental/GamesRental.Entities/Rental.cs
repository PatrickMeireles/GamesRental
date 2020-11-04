using GamesRental.Entities.Base;
using System;

namespace GamesRental.Entities
{
    public class Rental : BaseEntity
    {
        public virtual DateTime Date { get; set; }

        public virtual DateTime? DateEstimated { get; set; }

        public virtual DateTime? DateFinish { get; set; }

        public virtual int IdFriend { get; set; }

        public virtual Friend Friend { get; set; }

        public virtual int IdGame { get; set; }

        public virtual Game Game { get; set; }
    }
}
