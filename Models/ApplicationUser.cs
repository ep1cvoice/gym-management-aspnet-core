using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace GymApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? DocumentNumber { get; set; }

        // NAVIGATION
        public ICollection<UserAddress> Addresses { get; set; } = new List<UserAddress>();
    }
}
