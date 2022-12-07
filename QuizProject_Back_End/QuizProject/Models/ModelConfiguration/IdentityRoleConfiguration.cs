using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QuizProject.Models.ModelConfiguration
{
    public class IdentityRoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                    new IdentityRole()
                    {
                        Id = "a83cb833-4019-510d-9f17-a9d0b83540ee",
                        Name = "Admin",
                        NormalizedName = "ADMIN",
                        ConcurrencyStamp = "1"
                    }
                );
        }
    }
}
