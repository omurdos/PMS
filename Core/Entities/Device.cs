using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class Device : Entity
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
        public Emirate Emirate { get; set; }
        public string ModelId { get; set; }
        public Model Model { get; set; }
        public string ShopId { get; set; }
        public Shop Shop { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string StatusId { get; set; }
        public Status Status { get; set; }
    }
}
