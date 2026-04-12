using CoreFitness.Domain.Entities.Users;
using CoreFitness.Domain.Entities.Users.ValueObjects;
using CoreFitness.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreFitness.Infrastructure.Configurations
{
    public class UserConfiguration : BaseEntityConfiguration<User, UserId>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder.ComplexProperty(u => u.Email, email =>
            {
                email.Property(e => e.Value)
                .HasColumnName("Email")
                .IsRequired();

                email.Property(e => e.UniqueValue)
                .HasColumnName("NormalizedEmail")
                .IsRequired();
            });

            builder.HasIndex("NormalizedEmail").IsUnique();

            builder.ComplexProperty(u => u.UserName, name =>
            {
                name.Property(n => n.FirstName)
                .HasColumnName("FirstName")
                .HasMaxLength(UserName.MaxLength)
                .IsRequired();

                name.Property(n => n.LastName)
                .HasColumnName("LastName")
                .HasMaxLength(UserName.MaxLength)
                .IsRequired();
            });

            builder.Property(u => u.UserPhoneNumber)
                .HasConversion(p => p.HasValue ? p.Value.Value : null, v => v != null ? UserPhoneNumber.Create(v) : null);

            builder.Property(u => u.Role)
                .HasConversion<string>()
                .HasMaxLength(UserRoleConstants.MaxLength)
                .IsRequired();
        }
    }
}
