using CoreFitness.Domain.Entities.Common;
using CoreFitness.Domain.Entities.Users.ValueObjects;
using CoreFitness.Domain.Enums;
using CoreFitness.Domain.Interfaces;


namespace CoreFitness.Domain.Entities.Users
{
    public class User : BaseEntity<UserId>, IAggregateRoot
    {
        public UserEmail Email { get; private set; }
        public string EmailUnique => Email.UniqueValue;
        public UserName UserName { get; private set; }
        public UserPhoneNumber? UserPhoneNumber { get; private set; }
        public string? PhotoUrl { get; private set; }
        public UserRole Role { get; protected set; }

        protected User(UserId id, UserEmail email, UserName userName, UserPhoneNumber? phoneNumber, string photoUrl, UserRole role)
        {
            Id = id;
            Email = email;
            UserName = userName;
            UserPhoneNumber = phoneNumber;
            PhotoUrl = photoUrl;
            Role = role;
        }

        public static User Create(UserEmail email, UserName name, UserPhoneNumber? phonenumber, string photoUrl, UserRole role)
            => new(UserId.New(), email, name, phonenumber, photoUrl, role);

        private User() { }

        public void UpdateEmail(UserEmail newUserEmail)
        {
            if (Email == newUserEmail) return;

            Email = newUserEmail;
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

        public void UpdateAll(UserName name, UserEmail email, UserPhoneNumber phoneNumber)
        {
            UpdateEmail(email);
            UpdateName(name);
            UpdatePhoneNumber(phoneNumber);
        }
    }
}
