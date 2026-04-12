namespace CoreFitness.Domain.Exceptions
{
    public class TrainingSessionExceptions(string message) : DomainException(message);
    public class StartDateException(string message) : DomainException(message);
    public class InvalidCapacityException(string message) : DomainException(message);
    public class InvalidDurationException(string message) : DomainException(message);
    public class InvalidDescriptionLengthException(string message) : DomainException(message);
    public class TrainingSessionIsFullException(string message) : DomainException(message);
    public class DuplicateBookingException(string message) : DomainException(message);
    public class BookingNotFoundException(string message) : DomainException(message);
    public class InvalidTrainingSessionNameException(string message) : DomainException(message);
    public class InvalidSessionLimitException(string message) : DomainException(message);
}
