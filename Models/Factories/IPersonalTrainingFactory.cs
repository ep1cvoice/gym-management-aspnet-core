using GymApp.Models;
using GymApp.Models.Enums;

namespace GymApp.Models.Factories.PersonalTrainings
{
    public interface IPersonalTrainingFactory
    {
        Package Create(
            int trainerId,
            string userId,
            DietType? dietType = null);
    }
}
