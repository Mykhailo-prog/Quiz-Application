using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QuizProject.Models.ModelConfiguration;
using System;

namespace QuizProject.Models
{
    public class QuizContext: IdentityDbContext
    {
        public DbSet<TestStatistic> Statistics { get; set; }
        public DbSet<QuizUser> QuizUsers { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<UserStatistic> UserStatistic { get; set; }
        public DbSet<UserCreatedTest> CreatedTests { get; set; }

        public QuizContext(DbContextOptions<QuizContext> options)
            : base(options)
        {

            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new QuizUserConfiguration());
            modelBuilder.ApplyConfiguration(new TestConfiguration());
            modelBuilder.ApplyConfiguration(new QuestionConfiguration());
            modelBuilder.ApplyConfiguration(new AnswerConfiguration());
            modelBuilder.ApplyConfiguration(new CreatedTestConfiguration());
            modelBuilder.ApplyConfiguration(new TestStatisticConfiguration());
            modelBuilder.ApplyConfiguration(new IdentityRoleConfiguration());
            modelBuilder.ApplyConfiguration(new IdentityUserConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
        }
    }
}
