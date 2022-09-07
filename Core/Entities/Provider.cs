using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Provider : Entity
    {
        [Required(ErrorMessage = "Please provide the provider name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please provide the provider name")]
        public string ContactName { get; set; }
        [Required(ErrorMessage = "Please provide the provider name")]
        public string ContactPhoneNumber { get; set; }
        public ICollection<Manufacturer> Manufacturers { get; set; }
    }
}
