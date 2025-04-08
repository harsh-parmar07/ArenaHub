namespace ArenaHub.DTOs
{
    public class PlayerCreateDTO
    {
        public string? Name { get; set; }
        public string? Nickname { get; set; }
        public string? Email { get; set; }
        public int Age { get; set; }
        public Guid? TeamId { get; set; }
    }

    public class PlayerUpdateDTO
    {
        public string? Name { get; set; }
        public string? Nickname { get; set; }
        public string? Email { get; set; }
        public int Age { get; set; }
        public Guid? TeamId { get; set; }
    }

    public class PlayerViewDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Nickname { get; set; }
        public string? Email { get; set; }
        public int Age { get; set; }
        public Guid? TeamId { get; set; }
        public string? TeamName { get; set; }
    }
}