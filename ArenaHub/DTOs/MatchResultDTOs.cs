using System;

namespace ArenaHub.DTOs
{
    public class MatchResultCreateDTO
    {
        public Guid MatchId { get; set; }
        public int HomeTeamScore { get; set; }
        public int AwayTeamScore { get; set; }
        public string? Highlights { get; set; }
        public Guid? MVPPlayerId { get; set; }
    }

    public class MatchResultUpdateDTO
    {
        public int HomeTeamScore { get; set; }
        public int AwayTeamScore { get; set; }
        public string? Highlights { get; set; }
        public Guid? MVPPlayerId { get; set; }
    }

    public class MatchResultViewDTO
    {
        public Guid Id { get; set; }
        public int HomeTeamScore { get; set; }
        public int AwayTeamScore { get; set; }
        public string? Highlights { get; set; }
        public DateTime ResultDate { get; set; }
        public Guid? MVPPlayerId { get; set; }
        public string? MVPPlayerName { get; set; }
    }
}