using CoreFitness.Domain.Entities.Common;
using CoreFitness.Domain.Entities.Users.ValueObjects;
using CoreFitness.Domain.Enums;
using CoreFitness.Domain.Interfaces;


namespace CoreFitness.Domain.Entities.Users
{
    public class User : BaseEntity<UserId>, IAggregateRoot
    {
        public AuthenticationId AuthenticationId { get; private set; }
        public UserEmail Email { get; private set; }
        public string EmailUnique { get; private set; } = string.Empty;
        public UserName UserName { get; private set; }
        public UserPhoneNumber? UserPhoneNumber { get; private set; }
        public string? PhotoUrl { get; private set; }
        public UserRole Role { get; protected set; }

        protected User(UserId id, AuthenticationId authenticationId, UserEmail email, UserName userName, UserPhoneNumber? phoneNumber, string? photoUrl, UserRole role)
        {
            Id = id;
            AuthenticationId = authenticationId;
            Email = email;
            EmailUnique = email.UniqueValue;
            UserName = userName;
            UserPhoneNumber = phoneNumber;
            PhotoUrl = photoUrl;
            Role = role;
        }

        public static User Create(AuthenticationId authenticationId, UserEmail email, UserName name, UserPhoneNumber? phonenumber, string? photoUrl, UserRole role)
            => new(UserId.New(), authenticationId, email, name, phonenumber, photoUrl, role);

        private User() { }

        public void UpdateEmail(UserEmail newUserEmail)
        {
            if (Email == newUserEmail) return;

            Email = newUserEmail;
            EmailUnique = newUserEmail.UniqueValue;
            UpdateTimeStamp();
        }

        public void UpdateName(UserName newUserName)
        {
            if (UserName == newUserName) return;

            UserName = newUserName;
            UpdateTimeStamp();
        }

        public void UpdateFirstName(string newFirstName) =>
            UpdateName(UserName.Create(newFirstName, UserName.LastName));

        public void UpdateLastName(string newLastName) =>
            UpdateName(UserName.Create(UserName.FirstName, newLastName));

        public void UpdatePhoneNumber(UserPhoneNumber? newPhoneNumber)
        {
            if (UserPhoneNumber == newPhoneNumber) return;

            UserPhoneNumber = newPhoneNumber;
            UpdateTimeStamp();
        }

        public void UpdatePhotoUrl(string? newPhotoUrl)
        {                
            if (PhotoUrl == newPhotoUrl) return;

            PhotoUrl = newPhotoUrl;
            UpdateTimeStamp();
        }

        public void UpdateAll(UserName name, UserEmail email, UserPhoneNumber? phoneNumber, string photoUrl)
        {
            UpdateEmail(email);
            UpdateName(name);
            UpdatePhoneNumber(phoneNumber);
            UpdatePhotoUrl(photoUrl);
        }
    }
}
