namespace QuizProject.Models.ModelConfiguration
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using QuizProject.Models.Entity;

    public class QuizUserConfiguration : IEntityTypeConfiguration<QuizUser>
    {

        public void Configure (EntityTypeBuilder<QuizUser> builder)
        {
            builder.HasData(
                    new QuizUser
                    {
                        Id = 1,
                        Login = "Administrator",
                    }
                );
        }
    }

}
