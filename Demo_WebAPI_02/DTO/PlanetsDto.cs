namespace Demo_WebAPI_02.DTO
{
    public class PlanetsResponseAllDto
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
    }

    public class PlanetRequestDto
    {
    
        public required string Name { get; set; }
        public required int StelarSystemId { get; set; }
        public required string? Description { get; set; }
        public required int NbMoon { get; set; }
        public required DateTime DiscoveryDate { get; set; }
       
    }

    public class PlanetResponseDto
    {

        public required int Id { get; set; }
        public required string Name { get; set; }
        public required int StelarSystemId { get; set; }
        public required string? Description { get; set; }
        public required int NbMoon { get; set; }
        public required DateTime DiscoveryDate { get; set; }

    }
}
