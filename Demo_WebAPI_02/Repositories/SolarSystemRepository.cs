using Dapper;
using Demo_WebAPI_02.Models;
using System.Data;
using System.Data.Common;

namespace Demo_WebAPI_02.Repositories
{
    public class SolarSystemRepository
    {
        protected DbConnection _dbConnection;

        public SolarSystemRepository(DbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }


        public int GetCount()
        {
            using DbCommand cmd = _dbConnection.CreateCommand();

            cmd.CommandText = "SELECT COUNT(*) FROM [solar_system];";
            cmd.CommandType = CommandType.Text;

            return (int)cmd.ExecuteScalar()!;
        }

        //public IEnumerable<SolarSystem> GetAll()
        //{
        //    using DbCommand cmd = _dbConnection.CreateCommand();
        //
        //    cmd.CommandText = "SELECT [id], [name] FROM [solar_system]";
        //    cmd.CommandType = CommandType.Text;
        //
        //    using DbDataReader reader = cmd.ExecuteReader();
        //
        //    List<SolarSystem> result = [];
        //
        //    while (reader.Read())
        //    {
        //        int id = (int)reader["id"];
        //        string name = (string)reader["name"];
        //
        //        result.Add(new SolarSystem(id, name));
        //    }
        //
        //    return result;
        //}

        public IEnumerable<SolarSystem> GetAll()
        {
            using DbCommand cmd = _dbConnection.CreateCommand();

            cmd.CommandText = "SELECT [id], [name] FROM [solar_system]";
            cmd.CommandType = CommandType.Text;

            using DbDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                // Récuperation des données de la ligne du resultat SQL
                //int id = reader.GetInt32(reader.GetOrdinal("Id"));
                int id = (int)reader["id"];
                string name = (string)reader["name"];

                // Renvoi chaque éléments, un par un dans "objet parcourable"
                yield return new SolarSystem(id, name);
            }
        }

        public SolarSystem Insert(SolarSystem solarSystem)
        {
            using DbCommand cmd = _dbConnection.CreateCommand();

            cmd.CommandText = "INSERT INTO [solar_system] ([name])" +
                              " OUTPUT [inserted].[id], [inserted].[name]" +
                              " VALUES (@NameParam);";
            cmd.CommandType = CommandType.Text;

            DbParameter dbParamName = cmd.CreateParameter();
            dbParamName.ParameterName = "NameParam";
            dbParamName.Value = solarSystem.Name;

            cmd.Parameters.Add(dbParamName);

            using DbDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new SolarSystem(
                    (int)reader["id"],
                    (string)reader["name"]
                );
            }

            throw new Exception("Error on solar system inserted");
        }

        public SolarSystem? GetById(int id)
        {
            // Objectif : Récuperer les infos d'un système solaire avec ses planètes et ses étoiles
            // ************************************************************************************

            // Requete SQL avec les jointures et les colonnes necessaire à la liste généré par Dapper
            string query = "SELECT ss.*, p.*, s.* " +
                           "FROM [solar_system] ss " +
                                " LEFT JOIN [planet] p ON ss.id = p.solar_system_id " +
                                " LEFT JOIN [star] s ON ss.id = s.solar_system_id " +
                           "WHERE ss.id = @id;";

            // Execution de la requete (via Dapper)
            //  - Configuration de la requete generique -> <TypeTable1, TypeTable2, ..., TypeResultat>
            //  - Mapping des données récupéré (TypeTable1, TypeTable2, ...) vers une liste (TypeResultat)
            //  - Attention, un ligne est créer pour chaque résultat !!!
            IEnumerable<SolarSystem> result = _dbConnection.Query<SolarSystem, Planet, Star, SolarSystem>(
                query,
                (solarSystem, planet, star) =>
                {
                    // Mapping du resultat de la requete vers une list
                    solarSystem.Planets.Add(planet);
                    solarSystem.Stars.Add(star);
                    return solarSystem;
                },
                new { id }
            );

            // Utilisation de LinQ pour transformer le resultat "brut" de Dapper vers notre objet
            //  - "GroupBy" pour combiner les resultats
            //  - "Select" pour fusion les données en un seul objet
            var temp = result
                .GroupBy(ss => ss.Id)
                .Select(
                    grp =>
                    {
                        SolarSystem sol = grp.First();
                        sol.Planets = grp.Select(ss => ss.Planets.Single()).DistinctBy(s => s.Id).ToList();
                        sol.Stars = grp.Select(ss => ss.Stars.Single()).DistinctBy(s => s.Id).ToList();
                        return sol;
                    }
                );

            // Renvoi de l'élément (dans notre cas, il ne doit avoir qu'un seul resultat !)
            return temp.SingleOrDefault();
        }
    }
}
