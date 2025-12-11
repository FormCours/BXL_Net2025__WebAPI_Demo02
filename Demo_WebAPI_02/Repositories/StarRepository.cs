using Dapper;
using Demo_WebAPI_02.Models;
using System.Data.Common;

namespace Demo_WebAPI_02.Repositories
{
    public class StarRepository
    {
        protected DbConnection _dbConnection;

        public StarRepository(DbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public int GetCount()
        {
            return _dbConnection.ExecuteScalar<int>("SELECT COUNT(*) FROM [star];");
        }

        public IEnumerable<Star> GetAll()
        {
            return _dbConnection.Query<Star>("SELECT [id], [name], [is_dead] AS [isdead], [solar_system_id] AS [solarsystemid] FROM [star];");
        }

        public Star Insert(Star star)
        { 
            string query = "INSERT INTO [star] ([name], [is_dead], [solar_system_id])" +
                           " OUTPUT [inserted].[id], [inserted].[name], [inserted].[is_dead] AS [isdead]" +
                           " VALUES (@Name, @IsDead, @SolarSystemId);";

            return _dbConnection.QuerySingle<Star>(query, star);
        }
    }
}
