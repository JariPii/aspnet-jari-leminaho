namespace CoreFitness.Domain.Exceptions
{
    public class MembershipExceptions
    {
    }

    public class InvalidExtendMembershipException(string message) : DomainException(message);
}
