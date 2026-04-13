namespace CoreFitness.Domain.Exceptions
{
    public class MembershipExceptions
    {
    }

    public class InvalidExtendMembershipException(string message) : DomainException(message);
    public class NoSessionsLeftException(string message) : DomainException(message);
    public class MembershipExpiredException(string message) : DomainException(message);
    public class InvalidWeightException(string message) : DomainException(message);
    public class InvalidHeightException(string message) : DomainException(message);
}
