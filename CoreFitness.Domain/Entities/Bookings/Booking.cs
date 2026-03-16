using CoreFitness.Domain.Entities.Bookings.ValueObjects;
using CoreFitness.Domain.Entities.Common;
using CoreFitness.Domain.Entities.Users.ValueObjects;

namespace CoreFitness.Domain.Entities.Bookings
{
    public class Booking : BaseEntity<BookingId>
    {
        public UserId UserId { get; private set; }

        private Booking() { }

        public Booking(BookingId id, UserId userId)
        {
            Id = id;
            UserId = userId;
        }

    }
}
