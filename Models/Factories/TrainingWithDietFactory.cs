using GymApp.Models;
using GymApp.Models.Enums;

namespace GymApp.Models.Factories.PersonalTrainings
{
    public class TrainingWithDietFactory : IPersonalTrainingFactory
    {
        public Package Create(int trainerId, string userId, DietType? dietType = null)
        {
            if (dietType == null)
                throw new ArgumentException("DietType is required");

            var now = DateTime.UtcNow;

            return new Package
            {
                TrainerId = trainerId,
                UserId = userId,
                DietType = dietType.Value,
                StartDate = now,
                EndDate = now.AddDays(30)
            };
        }
    }
}
