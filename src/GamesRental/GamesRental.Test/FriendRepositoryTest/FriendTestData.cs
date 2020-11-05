using GamesRental.Application.ViewModel;
using System.Collections.Generic;

namespace GamesRental.Test.FriendRepositoryTest
{
    public static class FriendTestData
    {
        public static List<FriendViewModel> GetAll()
        {
            return new List<FriendViewModel>()
            {                
                new FriendViewModel(1, "teste um", "teste@teste.com"),
                new FriendViewModel(2, "teste dois", "testedius@teste.com"),
                new FriendViewModel(3, "teste tres", "testetres@teste.com")
            };
        }

        public static FriendViewModel Get(int id = 0)
        {
            return new FriendViewModel(id, "teste um", "teste@teste.com");
        }
    }
}
