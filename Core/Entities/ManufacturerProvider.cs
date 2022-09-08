using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class ManufacturerProvider
    {
        public string ManufacturerId { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public string ProviderId { get; set; }
        public Provider Provider { get; set; }
    }
}
