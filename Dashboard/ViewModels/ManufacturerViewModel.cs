namespace Dashboard.ViewModels
{
    public class ManufacturerViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public ICollection<ProviderViewModel> Providers { get; set; }
        public ICollection<DeviceModelViewModel> Models { get; set; }
    }

    public class CreateManufacturerViewModel 
    {
        public string Name { get; set; }
        public string ProviderId { get; set; }
    }
    public class UpdateManufacturerViewModel
    {
        public string Name { get; set; }
        public string ProviderId { get; set; }

    }
}
