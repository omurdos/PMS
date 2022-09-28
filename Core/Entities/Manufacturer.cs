using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class Manufacturer : Entity
    {
        [Required]
        public string Name { get; set; }
        public string ProviderId { get; set; }
        public Provider Provider { get; set; }
        public ICollection<DeviceModel> DeviceModels { get; set; }
    }
}
