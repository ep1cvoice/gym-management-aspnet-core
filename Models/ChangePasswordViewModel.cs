using System.ComponentModel.DataAnnotations;

namespace GymApp.Models.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [MinLength(8)]
        public string NewPassword { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Hasła nie są takie same")]
        public string ConfirmNewPassword { get; set; } = string.Empty;
    }
}
