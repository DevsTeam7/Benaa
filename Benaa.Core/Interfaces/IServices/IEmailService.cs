namespace Benaa.Core.Interfaces.IServices
{
    public interface IEmailService
    {
        void SendEmailAsync(string email, string content);
    }
}