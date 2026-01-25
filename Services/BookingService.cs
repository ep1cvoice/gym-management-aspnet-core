using Microsoft.EntityFrameworkCore;
using GymApp.Data;
using GymApp.Models;
using GymApp.Services.Mediators;

namespace GymApp.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingMediator _mediator;
        private readonly AppDbContext _context;

        public BookingService(
            IBookingMediator mediator,
            AppDbContext context)
        {
            _mediator = mediator;
            _context = context;
        }

        public Task<bool> BookClassAsync(int trainingClassId, string userId)
            => _mediator.BookClassAsync(trainingClassId, userId);

        public Task CancelBookingAsync(int bookingId, string userId)
            => _mediator.CancelBookingAsync(bookingId, userId);

        public async Task<List<Booking>> GetUserBookingsAsync(string userId)
        {
            return await _context.Bookings
                .Include(b => b.TrainingClass)
                .ThenInclude(tc => tc.Trainer)
                .Where(b => b.UserId == userId)
                .ToListAsync();
        }
    }
}
