using GamesRental.Application.Interface;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace GamesRental.Test.User
{
    public class UserTest
    {
        private readonly Mock<IUserApplication> _user;

        public UserTest()
        {
            _user = new Mock<IUserApplication>();
        }

        public void Test()
        {


        }
    }
}
