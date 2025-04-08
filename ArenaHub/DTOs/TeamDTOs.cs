namespace ArenaHub.DTOs
{
    public class TeamCreateDTO
    {
        public string? Name { get; set; }
        public string? Location { get; set; }
        public string? LogoUrl { get; set; }
    }

    public class TeamUpdateDTO
    {
        public string? Name { get; set; }
        public string? Location { get; set; }
        public string? LogoUrl { get; set; }
    }

    public class TeamViewDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Location { get; set; }
        public string? LogoUrl { get; set; }
        public int PlayerCount { get; set; }
    }
}