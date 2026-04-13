using CoreFitness.Domain.Entities.Bookings;
using CoreFitness.Domain.Entities.Bookings.ValueObjects;
using CoreFitness.Domain.Entities.Users.ValueObjects;

namespace CoreFitness.Domain.Interfaces.Bookings
{
    public interface IBookingRepository : IBaseRepository<Booking, BookingId>
    {
        Task<IEnumerable<Booking>> GetByUserIdAsync(UserId userId, CancellationToken ct = default);
    }
}
