namespace Dashboard.ViewModels
{
    public class BaseViewModel
    {
        public string Id { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedAt { get; set; }
        public ICollection<Action> Actions { get; set; }
    }
}
