using Hotel.Models.Shared;

namespace Hotel.Models
{
    public class Order: BaseEntity
    {
        public string? Name { get; set; }    
        public string? Phone {  get; set; }
        
         public int RoomId { get; set; }   
    }
}
