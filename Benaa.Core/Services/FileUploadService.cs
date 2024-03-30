using Microsoft.AspNetCore.Http;
using Benaa.Core.Utils.FileUploadTypes;
using Benaa.Core.Interfaces.IServices;

namespace Benaa.Core.Services
{
    public class FileUploadService : IFileUploadService
    {
        public async Task<string> UploadFile(IFormFile file)
        {
            var filePath = "";
            var uniqueIdentifier = Guid.NewGuid().ToString();
            var timestamp = DateTime.Now.Ticks.ToString();
            if (file != null && file.Length > 0)
            {
                var extension = GetFileExtension(file.FileName);
                var fileName = $"{uniqueIdentifier}{timestamp}" + extension;

                var _filePath = DetermineFileTypeToSaveIt(extension);
                if (!string.IsNullOrEmpty(_filePath))
                {
                    filePath = _filePath;
                }
                CreateDirectory(filePath);
                var newFileDirectory = Path.Combine(filePath, fileName);
                CareateFile(newFileDirectory, file);
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
        private void CareateFile(string filePath, IFormFile file)
        {
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyToAsync(stream);
            }
        }

        private string DetermineFileTypeToSaveIt(string extension)
        {
            if (PhoneUploadFile.FileExtensions.Contains(extension))
            {
                return Path.Combine(Directory.GetCurrentDirectory(), "Upload", "TeacherCertification");
            }
            else if (PhoneUploadFile.ImageExtensions.Contains(extension))
            {
                return Path.Combine(Directory.GetCurrentDirectory(), "Upload", "UserImge");
            }
            //TODO : Video Upload?
            //filePath = Path.Combine(Directory.GetCurrentDirectory(), "Upload", "");]
            return string.Empty;
        }

        public string GetFileExtension(string fileName)
        {
            var lsitOfSplitPath = fileName.Split('.');
            var lastIndex = lsitOfSplitPath.Length -1;
            return ("." + lsitOfSplitPath[lastIndex]).ToLower();
        }
    }
}
