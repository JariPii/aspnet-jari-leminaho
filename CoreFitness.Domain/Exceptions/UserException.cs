using CoreFitness.Domain.Entities.Users.ValueObjects;

namespace CoreFitness.Domain.Exceptions
{    
    public class UserEmailNotFoundException(UserEmail userEmail) : DomainException($"User email {userEmail.Value} is not registered as a member");
    public class InvalidUserEmailException(string message) : DomainException(message);
    public class InvalidUserPhoneNumberException(string message) : DomainException(message);
    public class InvalidPhotoUrlException(string message) : DomainException(message);
    public class InvalidWeightException(string message) : DomainException(message);
    public class InvalidHeightException(string message) : DomainException(message);
}
