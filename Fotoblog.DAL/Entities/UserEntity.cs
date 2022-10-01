namespace Fotoblog.DAL.Entities
{
    public class UserEntity:BaseEntity
    {
        public string UserName { get; set; } = string.Empty;
        public string Hash { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string EmailVerificationCode { get; set; } = string.Empty;
        public bool IsEmailConfirmed { get; set; } = false;
    }
}
