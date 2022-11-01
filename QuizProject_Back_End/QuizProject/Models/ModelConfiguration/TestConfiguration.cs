using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizProject.Models.Entity;

namespace QuizProject.Models.ModelConfiguration
{
    public class TestConfiguration : IEntityTypeConfiguration<Test>
    {
        public void Configure(EntityTypeBuilder<Test> builder)
        {
            builder.HasData(
                    new Test
                    {
                        TestId = 1,
                        Name = "C# Beginers Test"
                    }
                );
        }
    }
}
