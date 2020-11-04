using GamesRental.Entities.Base;

namespace GamesRental.Entities
{
    public class User : BaseEntity
    {
        public virtual string Hash { get; set; }

        public virtual string Name { get; set; }

        public virtual string Email { get; set; }

        public virtual string Password { get; set; }

        //Enum - RoleUser
        public virtual int Role { get; set; }
    }
}
