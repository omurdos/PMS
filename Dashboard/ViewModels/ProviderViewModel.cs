using System.ComponentModel.DataAnnotations;

namespace Dashboard.ViewModels
{
    public class ProviderViewModel : BaseViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string ContactName { get; set; }
        [Required]
        public string ContactPhoneNumber { get; set; }
        public string ContactEmailAddress { get; set; }
    }
    public class CreateProviderViewModel : BaseViewModel
    {
        [Required(ErrorMessage = "Please enter provider name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter provider's contact name")]
        public string ContactName { get; set; }
        [Required(ErrorMessage = "Please enter provider's contact phone number"), Phone]
        public string ContactPhoneNumber { get; set; }
        [Required(ErrorMessage = "Please enter provider's contact email address"), EmailAddress]
        public string ContactEmailAddress { get; set; }

    }
    public class UpdateProviderViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public string ContactName { get; set; }
        public string ContactPhoneNumber { get; set; }
        public string ContactEmailAddress { get; set; }
    }
}
