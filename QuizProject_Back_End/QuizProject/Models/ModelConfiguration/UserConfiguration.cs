namespace QuizProject.Models.ModelConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure (EntityTypeBuilder<User> builder)
        {
            builder.HasData(
                    new User
                    {
                        Id = 1,
                        Login = "Admin",
                        Password = "admin"
                    }
                );
        }
    }
}
