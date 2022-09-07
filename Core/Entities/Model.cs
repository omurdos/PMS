﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Model : Entity
    {
        public string ManufacturerId { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public string Name { get; set; }
        public ICollection<Device> Devices { get; set; }
    }
}
