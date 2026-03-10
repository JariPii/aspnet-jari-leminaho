using CoreFitness.Domain.Entities.Users.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFitness.Domain.Exceptions
{
    public class InvalidNameException(string message) : DomainException($"Invalid naming format: {message}");
    public class UserEmailNotFoundException(UserEmail userEmail) : DomainException($"User email {userEmail.Value} is not registered as a member");
    public class InvalidUserEmailException(string message) : DomainException(message);
    public class InvalidUserPhoneNumberException(string message) : DomainException(message);
}
