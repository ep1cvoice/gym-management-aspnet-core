using GymApp.Models;
using GymApp.Models.Enums;

namespace GymApp.Models.Factories.PersonalTrainings
{
    public class TrainingOnlyFactory : IPersonalTrainingFactory
    {
        public Package Create(int trainerId, string userId, DietType? dietType = null)
        {
            var now = DateTime.UtcNow;

            return new Package
            {
                TrainerId = trainerId,
                UserId = userId,
                DietType = DietType.Standard, // ignorowane
                StartDate = now,
                EndDate = now.AddDays(30)
            };
        }
    }
}
