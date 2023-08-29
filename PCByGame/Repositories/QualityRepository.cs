using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using PCByGame.Models;
using PCByGame.Utils;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Client.Extensions.Msal;
using System.Runtime.Intrinsics.Arm;
using PCByGame.Models.ViewModels;
using System.Runtime.InteropServices;
/*will be used in the drop downs for add and edit on Performance*/
namespace PCByGame.Repositories
{
    public class QualityRepository : BaseRepository, IQualityRepository
    {
        public QualityRepository(IConfiguration config) : base(config) { }

        public List<Quality> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT * FROM Quality";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var qualities = new List<Quality>();
                        while (reader.Read())
                        {
                            qualities.Add(new Quality
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Name = DbUtils.GetString(reader, "Name")
                            });
                        }

                        return qualities;
                    }
                }
            }
        }
    }
}
