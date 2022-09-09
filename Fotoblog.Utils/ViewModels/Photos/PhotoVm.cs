using Fotoblog.Utils.ViewModels.Tags;

namespace Fotoblog.Utils.ViewModels.Photos
{
    public class PhotoVm
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }
        public string OriginalUrl { get; set; } = string.Empty;
        public string ThumbnailUrl { get; set; } = string.Empty;

        public List<TagVm>? Tags { get; set; }
    }
}
