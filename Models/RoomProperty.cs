using Hotel.Models.Shared;
using System.ComponentModel.DataAnnotations;

namespace Hotel.Models
{
    public class RoomProperty : BaseEntity
    {
        [Required]
        public string? Name { get; set; }

        public ICollection<RoomPropertyDetail>? Details { get; set; }
      
    }
}
