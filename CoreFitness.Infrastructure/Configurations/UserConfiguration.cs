using CoreFitness.Domain.Entities.Users;
using CoreFitness.Domain.Entities.Users.ValueObjects;
using CoreFitness.Domain.Enums;
using CoreFitness.Infrastructure.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreFitness.Infrastructure.Configurations
{
    public class UserConfiguration : BaseEntityConfiguration<User, UserId>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder.Property(u => u.AuthenticationId)
                .HasConversion(new AuthenticationIdConverter())
                .IsRequired();

            builder.Property(u => u.Email)
                .HasConversion(v => v.Value, v => UserEmail.Create(v))
                .HasMaxLength(254)
                .IsRequired();

            builder.Property(u => u.EmailUnique)
                .HasMaxLength(254)
                .IsRequired();

            builder.HasIndex(u => u.EmailUnique).IsUnique();

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

            builder.Property(u => u.PhotoUrl)
                .HasMaxLength(500);
        }
    }
}
