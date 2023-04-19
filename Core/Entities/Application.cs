using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Application : Entity
    {
        public string FileName { get; set; }
        public string Version { get; set; }
        public string ApplicationCategoryId { get; set; }
        public ApplicationCategory ApplicationCategory { get; set; }

    }
}
