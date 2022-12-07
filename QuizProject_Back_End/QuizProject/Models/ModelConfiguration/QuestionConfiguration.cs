using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizProject.Models.Entity;

namespace QuizProject.Models.ModelConfiguration
{
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.HasData(
                    new Question
                    {
                        Id = 1,
                        Quest = "What access modifier make component available only within it's class of struct?",
                        CorrectAnswer = "Private",
                        TestId = 1,
                    },
                    new Question
                    {
                        Id = 2,
                        Quest = "What of these types is Referense type?",
                        CorrectAnswer = "Class",
                        TestId = 1,
                    },
                    new Question
                    {
                        Id = 3,
                        Quest = "What modifier can pass parameters by reference?",
                        CorrectAnswer = "ref",
                        TestId = 1,
                    }
                );
        }
    }
}
