using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ArenaHub.Models;

namespace ArenaHub.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<MatchResult> MatchResults { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Player Configuration
            modelBuilder.Entity<Player>(entity =>
            {
                entity.HasOne(p => p.Team)
                    .WithMany(t => t.Players)
                    .HasForeignKey(p => p.TeamId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // Match Configuration - CRITICAL FIX
            modelBuilder.Entity<Match>(entity =>
            {
                // Home Team relationship
                entity.HasOne(m => m.HomeTeam)
                    .WithMany(t => t.HomeMatches)
                    .HasForeignKey(m => m.HomeTeamId)
                    .OnDelete(DeleteBehavior.Restrict) // Changed to Restrict
                    .IsRequired();

                // Away Team relationship
                entity.HasOne(m => m.AwayTeam)
                    .WithMany(t => t.AwayMatches)
                    .HasForeignKey(m => m.AwayTeamId)
                    .OnDelete(DeleteBehavior.Restrict) // Changed to Restrict
                    .IsRequired();

                // Tournament relationship
                entity.HasOne(m => m.Tournament)
                    .WithMany(t => t.Matches)
                    .HasForeignKey(m => m.TournamentId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // MatchResult Configuration
            modelBuilder.Entity<MatchResult>(entity =>
            {
                entity.HasOne(mr => mr.Match)
                    .WithOne(m => m.MatchResult)
                    .HasForeignKey<MatchResult>(mr => mr.MatchId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(mr => mr.MVPPlayer)
                    .WithMany(p => p.MatchResults)
                    .HasForeignKey(mr => mr.MVPPlayerId)
                    .OnDelete(DeleteBehavior.SetNull);
            });
        }
    
    }
    
}