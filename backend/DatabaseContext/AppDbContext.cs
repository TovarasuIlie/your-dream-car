using backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend.DatabaseContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<QuizAnswer> QuizAnswers { get; set; }
        public DbSet<QuizQuestion> QuizQuestions { get; set; }
        public DbSet<QuizOption> QuizOptions { get; set; }
    }
}
