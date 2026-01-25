using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GymApp.Models.Enums;

namespace GymApp.Models
{
    public class Package : BaseEntity
    {
        [Required]
        public int TrainerId { get; set; }

        [ForeignKey(nameof(TrainerId))]
        public Trainer Trainer { get; set; } = null!;

        [Required]
        public DietType DietType { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; } = null!;

        public bool IsActive { get; set; } = true;

    }
}
