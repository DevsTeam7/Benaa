using Microsoft.AspNetCore.Http;

namespace Benaa.Core.Services
{
    public class FileUploadService
    {
        public async Task UploadFile(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var extension = ("." + file.FileName.Split('.')[file.FileName.Split('.').Length-1]).ToLower();
                var fileName = "userId+name" + extension;

                if (extension == ".pdf")
                    fileName = "jjj";

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Upload", "UserImge");

                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                var newFileDirectory = Path.Combine(filePath, fileName);
                using (var stream = new FileStream(newFileDirectory, FileMode.Create))
                {
                   await file.CopyToAsync(stream);
                }
                //save the url or return it to be saved
                //item1.Image_1 = "~/UserImg/" + file.FileName;
            }
        }
    }
}
