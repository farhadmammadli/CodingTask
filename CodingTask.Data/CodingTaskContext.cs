using CodingTask.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CodingTask.Data
{
    public class CodingTaskContext : DbContext
    {
        public CodingTaskContext(DbContextOptions<CodingTaskContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Submission> Submissions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User 
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable(nameof(User));
                entity.HasKey(u => u.Id);
                entity.Property(u => u.UserName)
                      .IsRequired()
                      .HasMaxLength(50);
                entity.Property(u => u.Password)
                      .IsRequired();

                // User - MatchesWon
                entity.HasMany(u => u.MatchesWon)
                      .WithOne(m => m.WinnerUser)
                      .HasForeignKey(m => m.WinnerUserId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Match 
            modelBuilder.Entity<Match>(entity =>
            {
                entity.ToTable(nameof(Match));
                entity.HasKey(m => m.Id);
                entity.Property(m => m.Id).ValueGeneratedOnAdd();

                entity.Property(m => m.ExpiryTimestamp)
                      .IsRequired();
                entity.Property(m => m.WinnerUserId)
                      .IsRequired(false);

                // One-to-Many between Match and Submissions
                entity.HasMany(m => m.Submissions)
                      .WithOne(s => s.Match)
                      .HasForeignKey(s => s.MatchId)
                      .HasPrincipalKey(m => m.Id);
            });

            // Submission 
            modelBuilder.Entity<Submission>(entity =>
            {
                entity.ToTable(nameof(Submission));
                entity.HasKey(s => s.Id);
                entity.Property(s => s.Id).ValueGeneratedOnAdd();

                entity.Property(s => s.GeneratedNumber)
                      .IsRequired();
                entity.Property(s => s.Timestamp)
                      .IsRequired();

                // One-to-Many between User and Submissions
                entity.HasOne(s => s.User)
                      .WithMany(u => u.Submissions)
                      .HasForeignKey(s => s.UserId)
                      .HasPrincipalKey(u => u.Id);

            });
        }
    }
}
