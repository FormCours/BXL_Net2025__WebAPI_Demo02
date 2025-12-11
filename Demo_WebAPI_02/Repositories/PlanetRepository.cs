using Dapper;
using Demo_WebAPI_02.Models;
using System.Data.Common;

namespace Demo_WebAPI_02.Repositories;

public class PlanetRepository
{
    protected DbConnection _dbConnection;

    public PlanetRepository(DbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public int GetCount()
    {
        return _dbConnection.ExecuteScalar<int>("SELECT COUNT(*) FROM [planet];");
    }

    public Planet? GetById(int id)
    {
        return _dbConnection.QuerySingleOrDefault<Planet>("SELECT [name], [desc], [nb_moon] AS [nbmoon], [discovery_date] AS [discoverydate], [solar_system_id] AS [solarsystemid] FROM [planet] WHERE id = @id;", new {id});
    }

    public IEnumerable<Planet> GetAll()
    {
        return _dbConnection.Query<Planet>("SELECT [name], [desc], [nb_moon] AS [nbmoon], [discovery_date] AS [discoverydate], [solar_system_id] AS [solarsystemid] FROM [planet];");
    }

    public Planet Insert(Planet planet)
    {
        string query = "INSERT INTO [planet] ([name], [desc], [nb_moon], [discovery_date], [solar_system_id])" +
                       " OUTPUT [inserted].[id], [inserted].[name], [inserted].[desc], [inserted].[nb_moon], [inserted].[discovery_date], [inserted].[solar_system_id] AS [SolarSystemId]" +
                       " VALUES (@Name, @Desc, @NbMoon, @DiscoveryDate, @SolarSystemId);";

        return _dbConnection.QuerySingle<Planet>(query, planet);
    }
}
