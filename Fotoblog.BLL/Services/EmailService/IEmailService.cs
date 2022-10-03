namespace Fotoblog.BLL.Services.EmailService
{
    public interface IEmailService
    {
        Task SendUserResetPasswordLink(string userName, string token, string userEmail);
        Task SendUserActivationLink(string userName, string token, string userEmail);
    }
}