using System;

namespace API.DTOs
{
    public class ActionDTO
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow.AddHours(4.0); //GULF Time.
        public string ModifiedBy { get; set; }
        public DateTime ModifiedAt { get; set; }
    }

    public class BaseActionDTO {
        public string Text { get; set; }
    }
}
