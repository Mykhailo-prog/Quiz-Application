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

        //public DbSet<TestStatistic> TestStats { get; set; }
        public QuizContext(DbContextOptions<QuizContext> options)
            : base(options)
        {

            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);
            this.SeedUsers(modelBuilder);
            this.SeedRoles(modelBuilder);
            this.SeedUserRole(modelBuilder);

            modelBuilder.ApplyConfiguration(new QuizUserConfiguration());
            modelBuilder.ApplyConfiguration(new TestConfiguration());
            modelBuilder.ApplyConfiguration(new QuestionConfiguration());
            modelBuilder.ApplyConfiguration(new AnswerConfiguration());
            modelBuilder.ApplyConfiguration(new CreatedTestConfiguration());
            modelBuilder.ApplyConfiguration(new TestStatisticConfiguration());

            
        }

        private void SeedUsers(ModelBuilder modelBuilder)
        {
            IdentityUser user = new IdentityUser
            {
                Id = "d82cb833-4019-410d-9f17-a9d0b83247ee",
                UserName = "Administrator",
                NormalizedUserName = "ADMINISTRATOR",
                NormalizedEmail = "MONSTERCATTOP@GMAIL.COM",
                Email = "monstercattop@gmail.com",
                EmailConfirmed = true,
                
            };

            PasswordHasher<IdentityUser> ph = new PasswordHasher<IdentityUser>();
            user.PasswordHash = ph.HashPassword(user, "Admin123");

            modelBuilder.Entity<IdentityUser>().HasData(user);
        }
        private void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
                    new IdentityRole()
                    {
                        Id = "a83cb833-4019-510d-9f17-a9d0b83540ee",
                        Name = "Admin",
                        NormalizedName = "ADMIN",
                        ConcurrencyStamp = "1"
                    }
                );
        }
        private void SeedUserRole(ModelBuilder builder)
        {
            builder.Entity<IdentityUserRole<string>>().HasData(
                    new IdentityUserRole<string>()
                    {
                        RoleId = "a83cb833-4019-510d-9f17-a9d0b83540ee",
                        UserId = "d82cb833-4019-410d-9f17-a9d0b83247ee"
                    }
                );
        }
    }
}
