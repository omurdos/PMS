using DashBoard.Models;
using Microsoft.Extensions.Hosting;
namespace JassimAPIs.Services
{

    public interface IFileUploader
    {
        public Task<FileUploadResult> Upload(IFormFile file, string folder, string version);
        public bool Remove(string fileName, string category);
    }


    public class FileUploader : IFileUploader
    {
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        private readonly string ImagesRootPath;
        public FileUploader(Microsoft.AspNetCore.Hosting.IHostingEnvironment webHostEnvironment)
        {
            _hostingEnvironment = webHostEnvironment;
            ImagesRootPath = Path.Combine(_hostingEnvironment.WebRootPath, "sources");

        }
        public async Task<FileUploadResult> Upload(IFormFile file, string folder, string version)
        {
            try
            {
                if (!Directory.Exists(ImagesRootPath))
                {
                    Directory.CreateDirectory(ImagesRootPath);

                }
                if (!Directory.Exists(Path.Combine(ImagesRootPath, "PayJM"))) {
                    Directory.CreateDirectory(Path.Combine(ImagesRootPath, "PayJM"));
                }
                if (!Directory.Exists(Path.Combine(ImagesRootPath, "updater")))
                {
                    Directory.CreateDirectory(Path.Combine(ImagesRootPath, "updater"));
                }             
                if (!Directory.Exists(Path.Combine(ImagesRootPath, "mdm")))
                {
                    Directory.CreateDirectory(Path.Combine(ImagesRootPath, "mdm"));
                }
                if (!Directory.Exists(Path.Combine(ImagesRootPath, "launcher")))
                {
                    Directory.CreateDirectory(Path.Combine(ImagesRootPath, "launcher"));
                }
                var fileSplit = file.FileName.Split(".");
                var fileExtention = fileSplit.LastOrDefault();
                string UniqueFileName = version + "." + fileExtention;
                string filePath = Path.Combine(ImagesRootPath, folder, UniqueFileName);
                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fs);
                    fs.Close();
                }
                if (File.Exists(filePath))
                {
                    return new FileUploadResult
                    {
                        Succeed = true,
                        Message = "Image uploaded successfully",
                        FileName = UniqueFileName 
                    };
                }
                else
                {
                    return new FileUploadResult
                    {
                        Succeed = false,
                        Message = "Failed to upload file"
                    };
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Remove(string fileName, string category)
        {
            try
            {
                string filePath = Path.Combine(ImagesRootPath, category, fileName);
                File.Delete(filePath);
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
    }
}
