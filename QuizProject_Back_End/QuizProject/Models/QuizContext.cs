using Microsoft.EntityFrameworkCore;
using QuizProject.Models.ModelConfiguration;

namespace QuizProject.Models
{
    public class QuizContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<UserTestCount> UserTests { get; set; }
        public QuizContext(DbContextOptions<QuizContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new TestConfiguration());
            modelBuilder.ApplyConfiguration(new QuestionConfiguration());
            modelBuilder.ApplyConfiguration(new AnswerConfiguration());
            //modelBuilder.Entity<User>().Property(x => x.Id).ValueGeneratedOnAdd();
            //modelBuilder.Entity<Question>().Property(x => x.Id).ValueGeneratedOnAdd();
            /*modelBuilder.Entity<Answer>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Test>().Property(x => x.TestId).ValueGeneratedOnAdd();*/
            /*modelBuilder.Entity<Test>(e => {
                e.HasMany(q => q.Questions).WithOne(q => q.Test).HasForeignKey(q => q.TestId);
                e.HasKey(q => q.TestId);
            });
            modelBuilder.Entity<Question>(e => {
                e.HasOne(q => q.Test).WithMany(q => q.Questions).HasForeignKey(q => q.TestId).HasConstraintName("FK_Questions_Tests");
                e.HasKey(q => q.Id);
            });*/
        }
    }
}
