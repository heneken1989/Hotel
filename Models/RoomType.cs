using Hotel.Models.Shared;

namespace Hotel.Models
{
    public class RoomType : BaseEntity
    {
     
        public string? Type { get; set; }

        public ICollection<Room>? Rooms { get;}

    }
}
