using System.ComponentModel.DataAnnotations;

namespace Dashboard.ViewModels
{
    public class StatusViewModel : BaseViewModel
    {
        public string Name { get; set; }
    }

    public class CreateStatusViewModel {
        [Required(ErrorMessage = "Status Can't be empty")]
        public string Name { get; set; }
    }
    public class UpdateStatusViewModel
    {
        [Required(ErrorMessage = "Status Can't be empty")]
        public string Name { get; set; }
    }

}
