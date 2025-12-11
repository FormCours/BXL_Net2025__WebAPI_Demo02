using Demo_WebAPI_02.Models;

namespace Demo_WebAPI_02.DTO
{
    public class SolarSystemResponseDto
    {
        // Champs
        public int Id { get; set; }
        public string Name { get; set; }

        public int NbPlanets { get; set; }
        public int NbStars { get; set; } 
    }

    public class SolarSystemResponseAllDto
    {
        // Champs
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class SolarSystemRequestDto 
    {
        public required string Name { get; set; }
    }
}
