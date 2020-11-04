using System;
using System.Collections.Generic;
using System.Text;

namespace GamesRental.Application.ViewModel
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Hash { get; set; }
        public string Name { get; set; }
        public int Role { get; set; }
    }
}
