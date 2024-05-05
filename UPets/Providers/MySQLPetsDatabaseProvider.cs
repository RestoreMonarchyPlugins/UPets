using RestoreMonarchy.UPets.Models;
using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoreMonarchy.UPets.Providers
{
    public class MySQLPetsDatabaseProvider : IPetsDatabaseProvider
    {
        private PetsPlugin pluginInstance => PetsPlugin.Instance;
        private MySqlConnection connection => new MySqlConnection(connectionString);

        private string connectionString => string.Format("Server={0};Port={1};Database={2};Uid={3};Pwd={4};",
            pluginInstance.Configuration.Instance.DatabaseAddress,
            pluginInstance.Configuration.Instance.DatabasePort,
            pluginInstance.Configuration.Instance.DatabaseName,
            pluginInstance.Configuration.Instance.DatabaseUsername,
            pluginInstance.Configuration.Instance.DatabasePassword);

        private string Query(string sql) => sql.Replace("PlayersPetsTable", pluginInstance.Configuration.Instance.DatabaseTableName);

        public void AddPlayerPet(PlayerPet playerPet)
        {
            const string sql = "INSERT INTO PlayersPetsTable (PlayerId, AnimalId, PurchaseDate) VALUES (@PlayerId, @AnimalId, @PurchaseDate);";
            connection.Execute(Query(sql), playerPet);
        }

        public IEnumerable<PlayerPet> GetPlayerPets(string playerId)
        {
            const string sql = "SELECT * FROM PlayersPetsTable WHERE PlayerId = @playerId;";
            return connection.Query<PlayerPet>(Query(sql), new { playerId });
        }

        public void Reload()
        {
            const string sql = "CREATE TABLE IF NOT EXISTS PlayersPetsTable (AnimalId SMALLINT UNSIGNED NOT NULL, PlayerId CHAR(17) NOT NULL, " +
                "PurchaseDate DATETIME NOT NULL, CONSTRAINT PK_PlayersPetsTable PRIMARY KEY(AnimalId, PlayerId));";
            connection.Execute(Query(sql));
        }
    }
}
