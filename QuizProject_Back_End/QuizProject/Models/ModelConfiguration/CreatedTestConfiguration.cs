using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace QuizProject.Models.ModelConfiguration
{
    public class CreatedTestConfiguration : IEntityTypeConfiguration<UserCreatedTest>
    {
        public void Configure(EntityTypeBuilder<UserCreatedTest> builder)
        {
            builder.HasData(
                    new UserCreatedTest
                    {
                        Id = 1,
                        UserId = 1,
                        TestId = 1,
                    }
                );
        }
    }
}
