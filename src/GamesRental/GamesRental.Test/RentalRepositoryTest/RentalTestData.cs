using GamesRental.Application.ViewModel;
using System;
using System.Collections.Generic;

namespace GamesRental.Test.RentalRepositoryTest
{
    public class RentalTestData
    {
        public static List<RentalViewModel> GetAll()
        {
            return new List<RentalViewModel>()
            {
                new RentalViewModel(1, DateTime.Now.AddDays(-10), null, null, 1, "Test Friend", 1, "Test", "Closed"),
                new RentalViewModel(2, DateTime.Now, null, null, 2, "Test Friend 2", 1, "Test", "Closed"),
                new RentalViewModel(3, DateTime.Now.AddDays(-20), null, DateTime.Now.AddDays(-2), 2, "Test Friend", 2, "Test 22", "Open")                
            };
        }

        public static RentalViewModel Get()
        {
            return new RentalViewModel(1, DateTime.Now.AddDays(-10), null, null, 1, "Test Friend", 1, "Test 22", "Open");
        }

        public static RentalCreateViewModel GetCreate()
        {
            return new RentalCreateViewModel(1, 1);
        }
    }
}
