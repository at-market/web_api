using market.Data.Domain;
using market.Extensions;
using market.Models.Common;
using market.Models.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using File = market.Data.Domain.File;

namespace market.Models.Domain;

public class User : Entity
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public string? MobileNumber { get; set; }
    public UserState UserState { get; set; } = UserState.Pending;
    public Guid? AvatarFileId { get; set; }
    public string? EmailVerifiedAt { get; set; }
    public string? Setting { get; set; }
    public UserType UserType { get; set; }
    public virtual File? AvatarFile { get; set; }
    public virtual Customer? Customer { get; set; }
    public virtual Staff? Staff { get; set; }
    public virtual Manager? Manager { get; set; }

}

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {

        builder.HasTimestamps();
        builder.HasSoftDelete();
        builder.HasUuid();

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Email).IsRequired();
        builder.Property(x => x.Password).IsRequired();
        builder.Property(x => x.FirstName);
        builder.Property(x => x.LastName);
        builder.Property(x => x.UserType).HasDefaultValue(UserType.Customer);
        builder.Property(x => x.UserState).HasDefaultValue(UserState.Pending);
        builder.Property(x => x.AvatarFileId);
        builder.Property(x => x.Setting);
        builder.Property(x => x.EmailVerifiedAt);
        builder.Property(x => x.MobileNumber);

        builder
            .HasOne(x => x.AvatarFile)
            .WithOne()
            .HasForeignKey<User>(x => x.AvatarFileId);
    }
}