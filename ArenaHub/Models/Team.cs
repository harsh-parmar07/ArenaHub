using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArenaHub.Models
{
    public class Team
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(100)]
        public string? Name { get; set; }

        [StringLength(100)]
        public string? Location { get; set; }

        [StringLength(1000)]
        public string? LogoUrl { get; set; }

        // Navigation properties
        public ICollection<Player>? Players { get; set; }
        public ICollection<Match>? HomeMatches { get; set; }
        public ICollection<Match>? AwayMatches { get; set; }
    }
}