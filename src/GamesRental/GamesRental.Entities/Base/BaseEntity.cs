using System.ComponentModel.DataAnnotations;

namespace GamesRental.Entities.Base
{
    public class BaseEntity
    {
        [Key]
        public virtual int Id { get; set; }
    }
}
