using Hotel.Models.Shared;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel.Models
{
    public class RoomUnity : BaseEntity
    {
        public string? Name { get; set; }

        [ForeignKey("Room")]
        public int RoomId { get; set; }
        
    }
}
