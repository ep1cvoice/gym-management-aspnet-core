using System.ComponentModel.DataAnnotations;
using GymApp.Models.Enums;

namespace GymApp.Models
{
    public class Trainer : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        public TrainerType TrainerType { get; set; }

        public string ImageUrl { get; set; } = string.Empty;

    }

    
}
