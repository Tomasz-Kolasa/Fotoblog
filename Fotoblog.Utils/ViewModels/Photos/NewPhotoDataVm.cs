using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System.ComponentModel.DataAnnotations;

namespace Fotoblog.Utils.ViewModels.Photos
{
    public class NewPhotoDataVm
    {
        [Required]
        public string title { get; set; } = string.Empty;

        public string ? description { get; set; }

        public int[] ? tags { get; set; }

        public IFormFile? file { get; set; }
    }
}