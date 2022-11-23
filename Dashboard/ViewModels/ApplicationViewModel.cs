using System.ComponentModel.DataAnnotations;

namespace Dashboard.ViewModels
{
    public class ApplicationViewModel
    {
        public string Name { get; set; }
        public string Version { get; set; }
    }

    public class CreateApplicationViewModel
    {
        [Required()]
        public string Name { get; set; }
        [Required()]

        public string Version { get; set; }
        [Required()]

        public IFormFile File { get; set; }

        public string ApplicationCategoryId { get; set; }
    }

    public class UpdateApplicationViewModel
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public IFormFile File { get; set; }
        public string ApplicationCategoryId { get; set; }

    }
}
