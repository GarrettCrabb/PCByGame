using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using PCByGame.Models;
using PCByGame.Utils;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Client.Extensions.Msal;
using System.Runtime.Intrinsics.Arm;

namespace PCByGame.Repositories
{
    public class PerformanceRepository : BaseRepository, IPerformanceRepository
    {
        public PerformanceRepository(IConfiguration config) : base(config) { }
        /*same question as UpdatePcPerformance method*/
        /*still needs to be tested*/
        /*look in pictures for example*/
        public int AddPerformance(Performance performance)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Performance
                                       (FPS,
                                        QualityId,
                                        GameId)
                                        OUTPUT INSERTED.Id
                                        VALUES
                                       (@FPS,
                                        @QualityId,
                                        @GameId)";

                    DbUtils.AddParameter(cmd, "@FPS", performance.FPS);
                    DbUtils.AddParameter(cmd, "@QualityId", performance.QualityId);
                    DbUtils.AddParameter(cmd, "@GameId", performance.GameId);

                    performance.Id = (int)cmd.ExecuteScalar();

                    return performance.Id;
                }
            }
        }
        /*will need multiple update queries*/
        /*still needs to be tested*/
        public void UpdatePerformance(Performance performance)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE Performance
                                        SET
                                            FPS = @fps,
                                            QualityId = qualityId,
                                            GameId = gameId
                                        WHERE Id = @id";

                    DbUtils.AddParameter(cmd, "@fps", performance.FPS);
                    DbUtils.AddParameter(cmd, "@qualityId", performance.QualityId);
                    DbUtils.AddParameter(cmd, "@gameId", performance.GameId);
                }
            }
        }
    }
}
