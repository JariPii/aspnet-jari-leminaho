using CoreFitness.Domain.Entities.Memberships.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFitness.Domain.Exceptions
{
    public class MembershipExceptions
    {
    }

    public class InvalidExtendMembershipException(string message) : DomainException(message);
}
