using GamesRental.Entities;
using System;

namespace GamesRental.Domain.Methods
{
    public static class RentalMethods
    {
        public static Boolean Finished(this Rental rental)
        {
            var date = DateTime.Now;
            return rental == null || (rental.DateFinish != null && rental.DateFinish <= date);
        }
    }
}
