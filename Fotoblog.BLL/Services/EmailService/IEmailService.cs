namespace Fotoblog.BLL.Services.EmailService
{
    public interface IEmailService
    {
        Task SendUserActivationLink(string userName, string token, string userEmail);
    }
}