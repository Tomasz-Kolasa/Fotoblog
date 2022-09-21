using Fotoblog.Utils.ViewModels.Tags;
using System.ComponentModel.DataAnnotations;
using AutoMapper.Configuration.Annotations;
using Fotoblog.DAL.Entities;
using AutoMapper;

namespace Fotoblog.Utils.ViewModels.Photos
{
    public class PhotoVm
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int Id { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(15)]
        public string Title { get; set; } = string.Empty;
        [MaxLength(30)]
        public string? Description { get; set; } = string.Empty;
        public string OriginalUrl { get; set; } = string.Empty;
        public string ThumbnailUrl { get; set; } = string.Empty;
        public List<TagVm> Tags { get; set; } = new List<TagVm>();
    }
}
