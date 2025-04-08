using ArenaHub.DTOs;
using ArenaHub.Models;
using AutoMapper;

namespace ArenaHub.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Team mappings
            CreateMap<Team, TeamViewDTO>()
                .ForMember(dest => dest.PlayerCount, opt => opt.MapFrom(src => src.Players.Count));
            CreateMap<TeamCreateDTO, Team>();
            CreateMap<TeamUpdateDTO, Team>();

            // Player mappings
            CreateMap<Player, PlayerViewDTO>()
                .ForMember(dest => dest.TeamName, opt => opt.MapFrom(src => src.Team.Name));
            CreateMap<PlayerCreateDTO, Player>();
            CreateMap<PlayerUpdateDTO, Player>();

            // Match mappings
            CreateMap<Match, MatchViewDTO>()
                .ForMember(dest => dest.HomeTeamName, opt => opt.MapFrom(src => src.HomeTeam.Name))
                .ForMember(dest => dest.AwayTeamName, opt => opt.MapFrom(src => src.AwayTeam.Name))
                .ForMember(dest => dest.TournamentName, opt => opt.MapFrom(src => src.Tournament.Name));
            CreateMap<MatchCreateDTO, Match>();
            CreateMap<MatchUpdateDTO, Match>();

            // Tournament mappings
            CreateMap<Tournament, TournamentViewDTO>()
                .ForMember(dest => dest.MatchCount, opt => opt.MapFrom(src => src.Matches.Count));
            CreateMap<TournamentCreateDTO, Tournament>();
            CreateMap<TournamentUpdateDTO, Tournament>();

            // MatchResult mappings
            CreateMap<MatchResult, MatchResultViewDTO>()
                .ForMember(dest => dest.MVPPlayerName, opt => opt.MapFrom(src => src.MVPPlayer.Name));
            CreateMap<MatchResultCreateDTO, MatchResult>();
            CreateMap<MatchResultUpdateDTO, MatchResult>();
        }
    }
}