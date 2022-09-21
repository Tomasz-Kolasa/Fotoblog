using System.ComponentModel.DataAnnotations;

namespace Fotoblog.Utils.ViewModels.Tags
{
    public class TagVm
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int Id { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(15)]
        public string Name { get; set; } = String.Empty;
    }
}
