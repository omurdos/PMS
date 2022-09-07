﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Emirate : Entity
    {
        public string Name { get; set; }
        public ICollection<Device> Devices { get; set; }
    }
}
