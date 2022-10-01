using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class Shop : Entity
    {
        [Required(ErrorMessage = "Please provide the shop name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please provide the shop phone number")]
        public string PhoneNumber { get; set; }
        public ICollection<Device> Devices { get; set; }
    }
}
