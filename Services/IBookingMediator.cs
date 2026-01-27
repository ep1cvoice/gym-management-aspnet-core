using GymApp.Models;
using GymApp.Services.Notifications;
using GymApp.Models.Notifications;


namespace GymApp.Services.Mediators
{
    public interface IBookingMediator
    {
        Task<bool> BookClassAsync(int trainingClassId, string userId
        );

        Task CancelBookingAsync(int bookingId, string userId);
    }
}
