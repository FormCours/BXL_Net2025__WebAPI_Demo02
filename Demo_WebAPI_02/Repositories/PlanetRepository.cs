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
        return _dbConnection.QuerySingle<Planet>("SELECT * FROM [planet] WHERE id = @id;", new {id});
    }

    public IEnumerable<Planet> GetAll()
    {
        return _dbConnection.Query<Planet>("SELECT * FROM [planet];");
    }

    public Planet Insert(Planet planet)
    {
        string query = "INSERT INTO [planet] ([name], [desc], [nb_moon], [discovery_date])" +
                       " OUTPUT [inserted].[id], [inserted].[name], [inserted].[desc], [inserted].[nb_moon], [inserted].[discovery_date]" +
                       " VALUES (@Name, @Desc, @NbMoon, @DiscoveryDate);";

        return _dbConnection.QuerySingle<Planet>(query, planet);
    }
}
