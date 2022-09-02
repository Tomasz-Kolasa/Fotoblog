namespace Fotoblog.DAL.Entities
{
    public class TagEntity:BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public List<PhotoEntity> Photos { get; set; } = new List<PhotoEntity>();
    }
}
