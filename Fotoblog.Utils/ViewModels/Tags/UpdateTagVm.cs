using System.ComponentModel.DataAnnotations;

namespace Fotoblog.Utils.ViewModels.Tags
{
    public class UpdateTagVm
    {
        [Required]
        [Range(1,int.MaxValue)]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string NewName { get; set; } = string.Empty;
    }
}
