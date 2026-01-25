using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GymApp.Models.Enums;

namespace GymApp.Models
{
    public class Booking : BaseEntity
    {
        [Required]
        public int TrainingClassId { get; set; }

        [ForeignKey(nameof(TrainingClassId))]
        public TrainingClass TrainingClass { get; set; } = null!;

        [Required]
        public string UserId { get; set; } = string.Empty;

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; } = null!;

        [Required]
        public BookingStatus Status { get; set; } = BookingStatus.Active;
    }
}
