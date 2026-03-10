using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFitness.Domain.Exceptions
{
    public class IdIsRequiredException(string message) : DomainException(message);
}
