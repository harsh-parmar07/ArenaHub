using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArenaHub.Models
{
    public class Player
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(100)]
        public string? Name { get; set; }

        [StringLength(100)]
        public string? Nickname { get; set; }

        [EmailAddress]
        [StringLength(100)]
        public string? Email { get; set; }

        [Range(0, 100)]
        public int Age { get; set; }

        // Navigation properties
        public Guid? TeamId { get; set; }
        public Team? Team { get; set; }

        public ICollection<MatchResult>? MatchResults { get; set; }
    }
}