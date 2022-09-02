using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Fotoblog.Utils.ViewModels.Photos
{
    public class NewPhotoDataVm
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        public string ? Description { get; set; }

        public List<int> ? Tags { get; set; }

        [Required]
        public IFormFile? File { get; set; }
    }
}