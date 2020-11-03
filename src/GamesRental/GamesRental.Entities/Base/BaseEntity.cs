using System.ComponentModel.DataAnnotations;

namespace GamesRental.Entities.Base
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
