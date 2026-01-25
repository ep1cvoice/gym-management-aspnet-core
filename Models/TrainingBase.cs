using System.ComponentModel.DataAnnotations;

namespace GymApp.Models
{
    public abstract class TrainingBase : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public TimeSpan Duration { get; set; }
    }
}
