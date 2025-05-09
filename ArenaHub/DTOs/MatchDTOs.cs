﻿using System;

namespace ArenaHub.DTOs
{
    public class MatchCreateDTO
    {
        public DateTime MatchDate { get; set; }
        public Guid HomeTeamId { get; set; }
        public Guid AwayTeamId { get; set; }
        public string? Venue { get; set; }
        public Guid? TournamentId { get; set; }
    }

    public class MatchUpdateDTO
    {
        public DateTime MatchDate { get; set; }
        public Guid HomeTeamId { get; set; }
        public Guid AwayTeamId { get; set; }
        public string? Venue { get; set; }
        public Guid? TournamentId { get; set; }
    }

    public class MatchViewDTO
    {
        public Guid Id { get; set; }
        public DateTime MatchDate { get; set; }

        // Home Team
        public Guid HomeTeamId { get; set; }
        public string HomeTeamName { get; set; }
        public string HomeTeamLogo { get; set; }  // Changed from HomeTeamLogoUrl

        // Away Team
        public Guid AwayTeamId { get; set; }
        public string AwayTeamName { get; set; }
        public string AwayTeamLogo { get; set; }  // Changed from AwayTeamLogoUrl

        public string Venue { get; set; }

        // Tournament
        public Guid? TournamentId { get; set; }
        public string TournamentName { get; set; }

        // Result
        public MatchResultViewDTO MatchResult { get; set; }

        // Helper properties for display
        public string HomeScoreDisplay => MatchResult?.HomeTeamScore.ToString() ?? "-";
        public string AwayScoreDisplay => MatchResult?.AwayTeamScore.ToString() ?? "-";
    }
}