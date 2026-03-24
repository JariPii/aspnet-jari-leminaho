namespace CoreFitness.Domain.Exceptions
{
    public class InvalidMembershipTypeException(string message) : DomainException(message);

    public class InvalidMembershipTypeNameException(string message) : DomainException(message);
    public class InvalidDescriptionException(string message) : DomainException(message);
    public class InvalidMembershipTypeDescriptionException(string message) : DomainException(message);
    public class InvalidPriceException(string message) : DomainException(message);
    public class InvalidMembershipTypeDurationException(string message) : DomainException(message);
}
