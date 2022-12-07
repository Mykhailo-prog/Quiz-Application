using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QuizProject.Models.ModelConfiguration
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                    new IdentityUserRole<string>()
                    {
                        RoleId = "a83cb833-4019-510d-9f17-a9d0b83540ee",
                        UserId = "d82cb833-4019-410d-9f17-a9d0b83247ee"
                    }
                );
        }
    }
}
