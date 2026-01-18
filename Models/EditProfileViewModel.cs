using System.ComponentModel.DataAnnotations;

namespace GymApp.Models.ViewModels
{
    public class EditProfileViewModel
    {
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Phone]
        public string? PhoneNumber { get; set; }

        [MaxLength(50)]
        public string? DocumentNumber { get; set; }
    }
}
