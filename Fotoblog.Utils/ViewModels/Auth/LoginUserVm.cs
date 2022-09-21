using System.ComponentModel.DataAnnotations;

namespace Fotoblog.Utils.ViewModels.Auth
{
    public class LoginUserVm
    {
        [Required]
        [MinLength(6)]
        [MaxLength(20)]
        public string UserName { get; set; } = string.Empty;
        [Required]
        [MinLength(6)]
        [MaxLength(20)]
        public string Password { get; set; } = string.Empty;
    }
}
