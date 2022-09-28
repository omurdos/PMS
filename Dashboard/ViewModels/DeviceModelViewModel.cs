using Core.Entities;

namespace Dashboard.ViewModels
{
    public class DeviceModelViewModel : BaseViewModel
    {
        public string ManufacturerId { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public string Name { get; set; }
        public ICollection<Device> Devices { get; set; }
    }

    public class CreateDeviceModelViewModel 
    {
        public string ManufacturerId { get; set; }
        public ManufacturerViewModel Manufacturer { get; set; }
        public string Name { get; set; }
    }
    public class UpdateDeviceModelViewModel
    {
        public string ManufacturerId { get; set; }
        public ManufacturerViewModel Manufacturer { get; set; }
        public string Name { get; set; }

    }
}
