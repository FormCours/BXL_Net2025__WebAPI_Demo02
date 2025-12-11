namespace Demo_WebAPI_02.Models
{
    // Model -> POCO (Plain old compiled object)
    // Classe avec uniquement les propriétées et le constructeur
    public class SolarSystem
    {
        // Champs
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Planet> Planets { get; set; } = [];
        public List<Star> Stars { get; set; } = [];

        // Ctor interne pour Dapper
        internal SolarSystem() { }

        // Ctor pour les éléments créer dans l'app
        public SolarSystem(string name)
        {
            Id = -1;
            Name = name;
        }

        // Ctor pour la récuperation depuis la DB
        public SolarSystem(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
