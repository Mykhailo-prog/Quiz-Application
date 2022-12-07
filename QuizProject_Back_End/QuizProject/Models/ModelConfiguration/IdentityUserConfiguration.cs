using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QuizProject.Models.ModelConfiguration
{
    public class IdentityUserConfiguration : IEntityTypeConfiguration<IdentityUser>
    {
        
        public void Configure(EntityTypeBuilder<IdentityUser> builder)
        {
            IdentityUser user = new IdentityUser
            {
                Id = "d82cb833-4019-410d-9f17-a9d0b83247ee",
                UserName = "Administrator",
                NormalizedUserName = "ADMINISTRATOR",
                NormalizedEmail = "MONSTERCATTOP@GMAIL.COM",
                Email = "monstercattop@gmail.com",
                EmailConfirmed = true,

            };
            PasswordHasher<IdentityUser> ph = new PasswordHasher<IdentityUser>();
            user.PasswordHash = ph.HashPassword(user, "Admin123");

            builder.HasData(user);
        }
    }
}
