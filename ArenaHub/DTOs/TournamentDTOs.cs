﻿using System;

namespace ArenaHub.DTOs
{
    public class TournamentCreateDTO
    {
        public string? Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Description { get; set; }
        public string? LogoUrl { get; set; }
    }

    public class TournamentUpdateDTO
    {
        public string? Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Description { get; set; }
        public string? LogoUrl { get; set; }
    }

    public class TournamentViewDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Description { get; set; }
        public string? LogoUrl { get; set; }
        public int MatchCount { get; set; }
    }
}