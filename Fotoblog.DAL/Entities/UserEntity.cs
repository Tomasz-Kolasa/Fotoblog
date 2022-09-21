namespace Fotoblog.DAL.Entities
{
    public class UserEntity:BaseEntity
    {
        public string UserName { get; set; } = string.Empty;
        public string Hash { get; set; } = string.Empty;
    }
}
