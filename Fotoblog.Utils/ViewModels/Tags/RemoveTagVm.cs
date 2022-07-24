using System.ComponentModel.DataAnnotations;

namespace Fotoblog.Utils.ViewModels.Tags
{
    public class RemoveTagVm
    {
        [Required]
        [Range(1,int.MaxValue)]
        public int Id { get; set; }
    }
}
