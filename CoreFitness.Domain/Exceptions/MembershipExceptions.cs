namespace CoreFitness.Domain.Exceptions
{
    public class InvalidExtendMembershipException(string message) : DomainException(message);
    public class NoSessionsLeftException(string message) : DomainException(message);
    public class MembershipExpiredException(string message) : DomainException(message);
    public class MembershipAlreadyDeactivatedException(string message) : DomainException(message);
}
