using Microsoft.AspNetCore.Http;

namespace Benaa.Core.Interfaces.IServices
{
    public interface IFileUploadService
    {
        Task<string> UploadFile(IFormFile file);
        string GetFileExtension(string fileName);
    }
}