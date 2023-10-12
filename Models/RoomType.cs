using Hotel.Atribute;
using Hotel.Models.Shared;
using System.ComponentModel.DataAnnotations;

namespace Hotel.Models
{
    public class RoomType : BaseEntity
    {

        [Required(ErrorMessage = "Cần Nhập Type")]
        [UniqueName(ErrorMessage = "Type đã tồn tại.")]
        public string? Type { get; set; }

        public ICollection<Room>? Rooms { get;}

    }
}
