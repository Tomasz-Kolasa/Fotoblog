using System.ComponentModel.DataAnnotations;

namespace Fotoblog.Utils.ViewModels.Tags
{
    public class NewTagVm
    {
        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string Name { get; set; } = string.Empty;
    }
}
