using Hotel.Models.Shared;
using System.ComponentModel.DataAnnotations;

namespace Hotel.Models
{
    public class Order: BaseEntity
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Phone {  get; set; }
        
         public int RoomId { get; set; }   
    }
}
