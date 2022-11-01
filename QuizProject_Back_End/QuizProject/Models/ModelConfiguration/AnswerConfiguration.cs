using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizProject.Models.Entity;

namespace QuizProject.Models.ModelConfiguration
{
    public class AnswerConfiguration : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.HasData(
                //Question 1
                    new Answer
                    {
                        Id = 101,
                        Ans = "Private",
                        QuestionId = 1
                    },
                    new Answer
                    {
                        Id = 102,
                        Ans = "Private Protected",
                        QuestionId = 1
                    },
                    new Answer
                    {
                        Id = 103,
                        Ans = "Protected",
                        QuestionId = 1
                    },
                    new Answer
                    {
                        Id = 104,
                        Ans = "Internal",
                        QuestionId = 1
                    },
                    new Answer
                    {
                        Id = 105,
                        Ans = "Public",
                        QuestionId = 1
                    },
                //Question 2
                    new Answer
                    {
                        Id = 106,
                        Ans = "Bool",
                        QuestionId = 2
                    },
                    new Answer
                    {
                        Id = 107,
                        Ans = "Integer",
                        QuestionId = 2
                    },
                    new Answer
                    {
                        Id = 108,
                        Ans = "Class",
                        QuestionId = 2
                    },
                    new Answer
                    {
                        Id = 109,
                        Ans = "Structure",
                        QuestionId = 2
                    },
                    new Answer
                    {
                        Id = 110,
                        Ans = "Char",
                        QuestionId = 2
                    },
                    //Question 3
                    new Answer
                    {
                        Id = 111,
                        Ans = "null",
                        QuestionId = 3
                    },
                    new Answer
                    {
                        Id = 112,
                        Ans = "ref",
                        QuestionId = 3
                    },
                    new Answer
                    {
                        Id = 113,
                        Ans = "public",
                        QuestionId = 3
                    },
                    new Answer
                    {
                        Id = 114,
                        Ans = "static",
                        QuestionId = 3
                    },
                    new Answer
                    {
                        Id = 115,
                        Ans = "<T>",
                        QuestionId = 3
                    }

                );
        }
    }
}
