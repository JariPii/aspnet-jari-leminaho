using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFitness.Domain.Exceptions
{
    public class TrainingSessionExceptions(string message) : DomainException(message);
    public class StartDateException(string message) : DomainException(message);
    public class InvalidCapacityException(string message) : DomainException(message);
    public class InvalidDurationException(string message) : DomainException(message);
    public class InvalidDescriptionLengthException(string message) : DomainException(message);
}
