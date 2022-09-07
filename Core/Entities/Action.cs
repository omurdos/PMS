using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Action
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow.AddHours(4.0); //GULF Time.
        public string ModifiedBy { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
