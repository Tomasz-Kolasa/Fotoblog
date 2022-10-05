using System.ComponentModel.DataAnnotations;

namespace Fotoblog.Utils.ViewModels.Auth
{
    public class ResetPasswordVm
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Token { get; set; } = string.Empty;

        [Required]
        [MinLength(6)]
        [MaxLength(20)]
        public string NewPwd { get; set; } = string.Empty;
        [Required]
        [MinLength(6)]
        [MaxLength(20)]
        public string ConfirmNewPwd { get; set; } = string.Empty;

    }
}
