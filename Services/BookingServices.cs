using Microsoft.EntityFrameworkCore;
using GymApp.Models;
using GymApp.Models.Enums;

namespace GymApp.Services
{
    public class BookingService : IBookingService
    {
        private readonly AppDbContext _context;

        public BookingService(AppDbContext context)
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

            var alreadyBooked = await _context.Bookings
                .AnyAsync(b => b.TrainingClassId == trainingClassId 
                            && b.UserId == userId
                            && b.Status == BookingStatus.Active);

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

            return true;
        }

        public async Task<List<Booking>> GetUserBookingsAsync(string userId)
        {
            return await _context.Bookings
                .Include(b => b.TrainingClass)
                .ThenInclude(tc => tc.Trainer)
                .Where(b => b.UserId == userId)
                .ToListAsync();
        }

        public async Task CancelBookingAsync(int bookingId, string userId)
        {
            var booking = await _context.Bookings
                .Include(b => b.TrainingClass)
                .FirstOrDefaultAsync(b => b.Id == bookingId && b.UserId == userId);

            if (booking == null)
                return;

            booking.Status = BookingStatus.Cancelled;
            booking.TrainingClass.TakenSlots--;

            await _context.SaveChangesAsync();
        }
    }
}
