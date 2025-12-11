namespace Demo_WebAPI_02.Models
{
    public class Star
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required bool IsDead { get; set; }
        public required int SolarSystemId { get; set; }
    }
}
