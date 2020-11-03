using GamesRental.Entities.Base;

namespace GamesRental.Entities
{
    public class User : BaseEntity
    {
        public string Hash { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        //Enum - RoleUser
        public int Role { get; set; }
    }
}
