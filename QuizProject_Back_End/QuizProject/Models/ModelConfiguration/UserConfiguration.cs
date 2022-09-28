namespace QuizProject.Models.ModelConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    public class UserConfiguration : IEntityTypeConfiguration<QuizUser>
    {
        public void Configure (EntityTypeBuilder<QuizUser> builder)
        {
            builder.HasData(
                    new QuizUser
                    {
                        Id = 1,
                        Login = "Admin",
                    }
                );
        }
    }
}
