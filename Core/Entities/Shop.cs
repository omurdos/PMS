using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class Shop : Entity
    {
        [Required(ErrorMessage = "Please provide the shop name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please provide the shop phone number")]
        public string PhoneNumber { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public ICollection<Device> Devices { get; set; }
    }
}
