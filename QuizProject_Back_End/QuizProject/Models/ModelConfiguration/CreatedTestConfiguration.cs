﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizProject.Models.Entity;

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
                        QuizUserId = 1,
                        TestId = 1,
                    }
                );
        }
    }
}
