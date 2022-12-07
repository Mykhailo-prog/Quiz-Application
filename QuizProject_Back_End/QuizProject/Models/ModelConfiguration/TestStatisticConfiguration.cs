using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizProject.Models.Entity;

namespace QuizProject.Models.ModelConfiguration
{
    public class TestStatisticConfiguration : IEntityTypeConfiguration<TestStatistic>
    {
        public void Configure(EntityTypeBuilder<TestStatistic> builder)
        {
            builder.HasData(
                    new TestStatistic
                    {
                        Id = 1,
                        TestId = 1,
                    }
                );
        }
    }
}
