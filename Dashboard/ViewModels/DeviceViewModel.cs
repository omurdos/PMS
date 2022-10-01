namespace Dashboard.ViewModels
{
    public class DeviceViewModel : BaseViewModel
    {
        public string IMEI1 { get; set; }
        public string IMEI2 { get; set; }
        public string SIMCardSerialNumber { get; set; }
        public string ApplicationVersion { get; set; }
        public bool IsDeployed { get; set; }
        public string Remarks { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public string EmirateId { get; set; }
        public EmirateViewModel Emirate { get; set; }
        public string ModelId { get; set; }
        public DeviceModelViewModel Model { get; set; }
        public string ShopId { get; set; }
        public ShopViewModel Shop { get; set; }
        public string UserId { get; set; }
        public UserViewModel User { get; set; }
        public string StatusId { get; set; }
        public StatusViewModel Status { get; set; }
    }
    public class CreateDeviceViewModel
    {
        public string IMEI1 { get; set; }
        public string IMEI2 { get; set; }
        public string SIMCardSerialNumber { get; set; }
        public string ApplicationVersion { get; set; }
        public bool IsDeployed { get; set; }
        public string Remarks { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public string EmirateId { get; set; }
        public string ModelId { get; set; }
        public string ShopId { get; set; }
        public string UserId { get; set; }
        public string StatusId { get; set; }
    }
    public class UpdateDeviceViewModel
    {
        public string IMEI1 { get; set; }
        public string IMEI2 { get; set; }
        public string SIMCardSerialNumber { get; set; }
        public string ApplicationVersion { get; set; }
        public bool IsDeployed { get; set; }
        public string Remarks { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public string EmirateId { get; set; }
        public string ModelId { get; set; }
        public string ShopId { get; set; }
        public string UserId { get; set; }
        public string StatusId { get; set; }
    }
}
