namespace Fotoblog.DAL.Entities
{
    public class PhotoEntity:BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string ?Description { get; set; }
        public string Path { get; set; } = string.Empty;
        public string Extension { get; set; } = string.Empty;
        public string OriginalUrl { get; set; } = string.Empty;
        public string ThumbnailUrl { get; set; } = string.Empty;

        public List<TagEntity> Tags { get; set; } = new List<TagEntity>();

    }
}
