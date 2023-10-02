using Hotel.Models.Shared;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel.Models
{
    public class Room: BaseEntity
    {
    
      

        [ForeignKey("RoomType")]
        public int RoomTypeID { get; set; }


       public ICollection<RoomProperty>? roomProperties { get; set; }


        public ICollection<Image>? Images { get;}
        public ICollection<RoomUnity>? Unities { get; }



    }
}
