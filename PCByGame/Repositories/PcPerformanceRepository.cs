using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using PCByGame.Models;
using PCByGame.Utils;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Client.Extensions.Msal;
using System.Runtime.Intrinsics.Arm;
using PCByGame.Models.ViewModels;

namespace PCByGame.Repositories
{
    public class PcPerformanceRepository : BaseRepository, IPcPerformanceRepository
    {
        public PcPerformanceRepository(IConfiguration config) : base(config) { }
        
        public List<PcPerformanceGetAllViewModel> GetAllByPcId(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT pp.Id AS PCPerformanceId,
	                                    pp.PerformanceId AS PerformanceId,
	                                    pp.PCId AS PCId,
	                                    p.FPS,
	                                    p.QualityId,
	                                    p.GameId,
	                                    q.Name AS QualityName,
	                                    g.Name AS GameName,
	                                    g.CategoryId,
	                                    c.Name AS CategoryName
										FROM PCPerformance pp
                                        LEFT JOIN Performance p ON pp.PerformanceId = p.Id
	                                    LEFT JOIN Quality q ON p.QualityId = q.Id
	                                    LEFT JOIN Game g ON p.GameId = g.Id
	                                    LEFT JOIN Category c ON g.CategoryId = c.Id
                                        LEFT JOIN PC pc ON pp.PCId = pc.Id
                                        WHERE pc.Id = @id";

                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var pcPerformances = new List<PcPerformanceGetAllViewModel>();
                        while (reader.Read())
                        {
                            pcPerformances.Add(new PcPerformanceGetAllViewModel()
                            {
                                PcPerformance = new PcPerformance()
                                {
                                    Id = DbUtils.GetInt(reader, "PCPerformanceId"),
                                    PerformanceId = DbUtils.GetInt(reader, "PerformanceId"),
                                    PCId = DbUtils.GetInt(reader, "PCId")
                                },
                                Performance = new Performance()
                                {
                                    Id = DbUtils.GetInt(reader, "PerformanceId"),
                                    FPS = DbUtils.GetInt(reader, "FPS"),
                                    QualityId = DbUtils.GetInt(reader, "QualityId"),
                                    GameId = DbUtils.GetInt(reader, "GameId"),
                                },
                                Quality = new Quality()
                                {
                                    Id = DbUtils.GetInt(reader, "QualityId"),
                                    Name = DbUtils.GetString(reader, "QualityName"),
                                },
                                Game = new Game()
                                {
                                    Id = DbUtils.GetInt(reader, "GameId"),
                                    Name = DbUtils.GetString(reader, "CategoryName"),
                                    CategoryId = DbUtils.GetInt(reader, "CategoryId"),
                                },
                                Category = new Category()
                                {
                                    Id = DbUtils.GetInt(reader, "CategoryId"),
                                    Name = DbUtils.GetString(reader, "CategoryName")
                                }
                            });
                        }

                        return pcPerformances;
                    }
                }
            }
        }
        /*How can I chain 2 adds on the submit button for the PCPerformance table or is there a better way to save the information from my other 4 tables to this one */
        /*Look in pictures for example*/
        /*Still needs to be tested*/
        public void AddPcPerformance(PcPerformance pcPerformance)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO PCPerformance
                                       (PerformanceId,
                                        PCId)
                                        OUTPUT INSERTED.Id
                                        VALUES
                                       (@PerformanceId,
                                        @PCId)";

                    DbUtils.AddParameter(cmd, "@PerformanceId", pcPerformance.PerformanceId);
                    DbUtils.AddParameter(cmd, "@PCId", pcPerformance.PCId);

                    pcPerformance.Id = (int)cmd.ExecuteScalar();
                }
            }
        }
        /*Ask about if i need a join or how to do this to display game quality and FPS for editing with drop downs*/
        /*will need multiple update queries*/
        /*Still needs to be tested*/
        public void UpdatePcPerformance(PcPerformance pcPerformance)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE PCPerformance
                                        SET
                                            PerformanceId = @performanceId,
                                            PCId = @pcId
                                        WHERE Id = @id";

                    DbUtils.AddParameter(cmd, "@performanceId", pcPerformance.PerformanceId);
                    DbUtils.AddParameter(cmd, "@pcId", pcPerformance.PCId);

                    cmd.ExecuteScalar();
                }
            }
        }
    }
}
