using System.ComponentModel.DataAnnotations;

namespace Fotoblog.Utils.ViewModels.Settings.AccountSettings
{
    public class ChangePwdVm
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int UserId { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(20)]
        public string OldPwd { get; set; } = string.Empty;

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
