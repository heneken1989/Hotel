using Hotel.Models.Shared;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel.Models
{
    public class RoomPropertyDetail: BaseEntity
    {
        [ForeignKey("RoomProperty")]
        public int RoomPropertyId { get; set; }
        public string? Detail { get; set; }  
    }
}
