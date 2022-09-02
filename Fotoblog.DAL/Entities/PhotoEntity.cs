namespace Fotoblog.DAL.Entities
{
    public class PhotoEntity:BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string ?Description { get; set; }
        public string ImagePath { get; set; } = string.Empty;

        public List<TagEntity> Tags { get; set; } = new List<TagEntity>();

    }
}
