using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArenaHub.Models
{
    public class MatchResult
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid MatchId { get; set; }
        public Match? Match { get; set; }

        public int HomeTeamScore { get; set; }
        public int AwayTeamScore { get; set; }

        [StringLength(1000)]
        public string? Highlights { get; set; }

        public DateTime ResultDate { get; set; } = DateTime.UtcNow;

        // MVP (Player who performed best)
        public Guid? MVPPlayerId { get; set; }
        public Player? MVPPlayer { get; set; }
    }
}