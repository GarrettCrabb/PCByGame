using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using PCByGame.Models;
using PCByGame.Utils;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Client.Extensions.Msal;
using System.Runtime.Intrinsics.Arm;
using PCByGame.Models.ViewModels;
/*will be used in the drop downs for adding a gameId to Performance in the add or edit page*/
/*will be used in the drop down for searching for pcs by game*/
namespace PCByGame.Repositories
{
    public class GameRepository : BaseRepository, IGameRepository
    {
        public GameRepository(IConfiguration config) : base(config) { }

        public List<Game> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT * FROM Game";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var games = new List<Game>();
                        while (reader.Read())
                        {
                            games.Add(new Game
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Name = DbUtils.GetString(reader, "Name"),
                                CategoryId = DbUtils.GetInt(reader, "CategoryId")
                            });
                        }

                        return games;
                    }
                }
            }
        }
    }
}
