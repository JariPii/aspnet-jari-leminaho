namespace CoreFitness.Domain.Exceptions
{
    public class IdIsRequiredException(string message) : DomainException(message);
    public class InvalidNameException(string message) : DomainException($"Invalid naming format: {message}");
}
