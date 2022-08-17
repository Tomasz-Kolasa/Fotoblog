namespace Fotoblog.DAL.Entities
{
    public class PhotoEntity
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ?Description { get; set; }
        public string ImagePath { get; set; } = string.Empty;
        public DateTime CreateDate { get; set; }

        public ICollection<TagEntity> ?Tags { get; set; }

    }
}
