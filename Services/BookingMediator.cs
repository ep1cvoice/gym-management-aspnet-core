using Microsoft.EntityFrameworkCore;
using GymApp.Data;
using GymApp.Models;
using GymApp.Models.Enums;
using GymApp.Services.Notifications;
using GymApp.Models.Notifications;


namespace GymApp.Services.Mediators
{
    public class BookingMediator : IBookingMediator
    {
        private readonly AppDbContext _context;

        public BookingMediator(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> BookClassAsync(int trainingClassId, string userId)
        {
            var trainingClass = await _context.TrainingClasses
                .FirstOrDefaultAsync(tc => tc.Id == trainingClassId);

            if (trainingClass == null)
                return false;

            if (trainingClass.TakenSlots >= trainingClass.MaxSlots)
                return false;

            var alreadyBooked = await _context.Bookings.AnyAsync(b =>
                b.TrainingClassId == trainingClassId &&
                b.UserId == userId &&
                b.Status == BookingStatus.Active);

            if (alreadyBooked)
                return false;

            var booking = new Booking
            {
                TrainingClassId = trainingClassId,
                UserId = userId,
                Status = BookingStatus.Active
            };

            trainingClass.TakenSlots++;

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            var notificationManager = NotificationManager.Instance;
            var emailNotification = new EmailNotification();

            notificationManager.Send(
                emailNotification,
                userId,
                "Zostałeś pomyślnie zapisany na zajęcia."
            );


            return true;
        }

        public async Task CancelBookingAsync(int bookingId, string userId)
        {
            var booking = await _context.Bookings
                .Include(b => b.TrainingClass)
                .FirstOrDefaultAsync(b =>
                    b.Id == bookingId &&
                    b.UserId == userId &&
                    b.Status == BookingStatus.Active);

            if (booking == null)
                return;

            booking.Status = BookingStatus.Cancelled;
            booking.TrainingClass.TakenSlots--;

            await _context.SaveChangesAsync();

            // ===== SINGLETON – NOTYFIKACJA =====
            var notificationManager = NotificationManager.Instance;
            var emailNotification = new EmailNotification();

            notificationManager.Send(
                emailNotification,
                userId,
                "Twoja rezerwacja zajęć została anulowana.");
        }
    }
}
