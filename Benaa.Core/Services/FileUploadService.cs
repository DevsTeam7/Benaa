using Microsoft.AspNetCore.Http;
using Benaa.Core.Utils.FileUploadTypes;
using Benaa.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Hosting;

namespace Benaa.Core.Services
{
    public class FileUploadService : IFileUploadService
    {
        private readonly IHostingEnvironment _environment;
        public FileUploadService(IHostingEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<string> UploadFile(IFormFile file)
        {
            var filePath = "";
            var uniqueIdentifier = Guid.NewGuid().ToString();
            var timestamp = DateTime.Now.Ticks.ToString();

            if (file != null && file.Length > 0)
            {
                var extension = GetFileExtension(file.FileName);
                var fileName = $"{timestamp}{uniqueIdentifier}{extension}";

                //user image and certfication
                var path = DetermineFileTypeToSaveIt(extension);
                if (!string.IsNullOrEmpty(path))
                {
                    filePath = path;
                }
                CreateDirectory(filePath);
                var newFileDirectory = Path.Combine(filePath, fileName);
                await CareateFile(newFileDirectory, file);
                return newFileDirectory;
            }
            return string.Empty;
        }
        private void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
        private async Task CareateFile(string filePath, IFormFile file)
        {
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
               await file.CopyToAsync(stream);
            }
        }
        private string DetermineFileTypeToSaveIt(string extension)
        {
           // string wwwPath = Path.GetFileName(_environment.WebRootPath);
            if (PhoneUploadFile.FileExtensions.Contains(extension))
            {
                return Path.Combine(_environment.WebRootPath, "Upload", "TeacherCertification");
            }
            else if (PhoneUploadFile.ImageExtensions.Contains(extension))
            {
                return Path.Combine(_environment.WebRootPath, "Upload", "UserImge");
            }
            else if (PhoneUploadFile.VideoExtensions.Contains(extension))
            {
                return Path.Combine(_environment.WebRootPath, "Courses");
            }
            return string.Empty;
        }
        public string GetFileExtension(string fileName)
        {
            var lsitOfSplitPath = fileName.Split('.');
            var lastIndex = lsitOfSplitPath.Length - 1;
            return ("." + lsitOfSplitPath[lastIndex]).ToLower();
        }
    }
}
