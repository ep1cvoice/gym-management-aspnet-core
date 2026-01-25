using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymApp.Models
{
    public class TrainingClass : TrainingBase
    {
        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public int MaxSlots { get; set; }

        public int TakenSlots { get; set; }

        [Required]
        public int TrainerId { get; set; }

        [ForeignKey(nameof(TrainerId))]
        public Trainer Trainer { get; set; } = null!;
    }
}
