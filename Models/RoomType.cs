using Hotel.Models.Shared;
using System.ComponentModel.DataAnnotations;

namespace Hotel.Models
{
    public class RoomType : BaseEntity
    {

        [Required]
        public string? Type { get; set; }

        public ICollection<Room>? Rooms { get;}

    }
}
