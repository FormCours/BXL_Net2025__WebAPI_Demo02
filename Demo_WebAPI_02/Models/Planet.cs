namespace Demo_WebAPI_02.Models;

public class Planet
{
    public required int Id {  get; set; }
    public required string Name { get; set; }
    public required string? Description { get; set; }
    public required int NbMoon { get; set; }
    public required DateTime DiscoveryDate {  get; set; }
}
