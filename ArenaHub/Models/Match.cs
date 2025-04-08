using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArenaHub.Models
{
    public class Match
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public DateTime MatchDate { get; set; }

        [Required]
        public Guid HomeTeamId { get; set; }
        public Team? HomeTeam { get; set; }

        [Required]
        public Guid AwayTeamId { get; set; }
        public Team? AwayTeam { get; set; }

        [StringLength(100)]
        public string? Venue { get; set; }

        public Guid? TournamentId { get; set; }
        public Tournament? Tournament { get; set; }

        // Navigation property
        public MatchResult? MatchResult { get; set; }
    }
}