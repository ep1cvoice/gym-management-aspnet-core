using GymApp.Models;

namespace GymApp.Services
{
    public interface IBookingService
    {
        Task<bool> BookClassAsync(int trainingClassId, string userId);
        Task<List<Booking>> GetUserBookingsAsync(string userId);
        Task CancelBookingAsync(int bookingId, string userId);

    }
}
