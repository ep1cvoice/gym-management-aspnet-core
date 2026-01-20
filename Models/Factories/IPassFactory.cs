using GymApp.Models.Enums;

namespace GymApp.Models.Factories
{
    public interface IPassFactory
    {
        UserPass Create(PassType passType, string userId);
    }
}
