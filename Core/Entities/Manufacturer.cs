using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Manufacturer : Entity
    {
        [Required]
        public string Name { get; set; }
        public ICollection<Provider> Providers { get; set; }
        public ICollection<Model> Models { get; set; }
    }
}
