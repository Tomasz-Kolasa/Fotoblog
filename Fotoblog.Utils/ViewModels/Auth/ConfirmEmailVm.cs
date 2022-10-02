using System.ComponentModel.DataAnnotations;

namespace Fotoblog.Utils.ViewModels.Auth
{
    public class ConfirmEmailVm
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Token { get; set; } = string.Empty;
    }
}
