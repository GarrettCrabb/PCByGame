using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using PCByGame.Models;
using PCByGame.Utils;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Client.Extensions.Msal;
using System.Runtime.Intrinsics.Arm;
using PCByGame.Models.ViewModels;

/*Need a get PcByUserId method*/
namespace PCByGame.Repositories
{
    public class PcRepository : BaseRepository, IPcRepository
    {
        public PcRepository(IConfiguration config) : base(config) { }

        public List<PcAndPcPerformanceViewModel> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT pc.Id,
	                                    pc.UserProfileId,
                                        pc.Name,
	                                    pc.Motherboard,
	                                    pc.CPU,
	                                    pc.Ram,
	                                    pc.GPU,
	                                    pc.PSU,
	                                    pc.Storage,
	                                    pc.CaseName,
	                                    pc.Cost,
                                        pp.Id AS PCPerformanceId,
	                                    pp.PerformanceId AS PerformanceId,
	                                    pp.PCId AS PCId,
	                                    p.FPS,
	                                    p.QualityId,
	                                    p.GameId,
	                                    q.Name AS QualityName,
	                                    g.Name AS GameName,
	                                    g.CategoryId,
	                                    c.Name AS CategoryName,
	                                    u.Id AS UserId,
	                                    u.FirstName,
	                                    u.LastName,
	                                    u.CreateDateTime,
	                                    u.Email,
	                                    u.DisplayName,
                                        u.FirebaseUserId
	                                    FROM Pc pc
	                                    LEFT JOIN UserProfile u ON pc.UserProfileId = u.Id
	                                    LEFT JOIN PCPerformance pp ON pc.Id = pp.PCId
	                                    LEFT JOIN Performance p ON pp.PerformanceId = p.Id
	                                    LEFT JOIN Quality q ON p.QualityId = q.Id
	                                    LEFT JOIN Game g ON p.GameId = g.Id
	                                    LEFT JOIN Category c ON g.CategoryId = c.Id";
                    
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var pcs = new List<PcAndPcPerformanceViewModel>();
                        while (reader.Read())
                        {
                            PcAndPcPerformanceViewModel newViewModel = new PcAndPcPerformanceViewModel()
                            {
                                Pc = new Pc()
                                {
                                    Id = DbUtils.GetInt(reader, "Id"),
                                    Name = DbUtils.GetString(reader, "Name"),
                                    Motherboard = DbUtils.GetString(reader, "Motherboard"),
                                    CPU = DbUtils.GetString(reader, "CPU"),
                                    Ram = DbUtils.GetString(reader, "Ram"),
                                    GPU = DbUtils.GetString(reader, "GPU"),
                                    PSU = DbUtils.GetString(reader, "PSU"),
                                    Storage = DbUtils.GetString(reader, "Storage"),
                                    CaseName = DbUtils.GetString(reader, "CaseName"),
                                    Cost = DbUtils.GetString(reader, "Cost"),
                                    UserProfileId = DbUtils.GetInt(reader, "UserProfileId"),
                                        UserProfile = new UserProfile()
                                        {
                                            Id = DbUtils.GetInt(reader, "UserId"),
                                            FirstName = DbUtils.GetString(reader, "FirstName"),
                                            LastName = DbUtils.GetString(reader, "LastName"),
                                            DisplayName = DbUtils.GetString(reader, "DisplayName"),
                                            Email = DbUtils.GetString(reader, "Email"),
                                            CreateDateTime = DbUtils.GetDateTime(reader, "CreateDateTime"),
                                            FirebaseUserId = DbUtils.GetString(reader, "FirebaseUserId")
                                        }
                                }
                            };

                            if (!DbUtils.IsDbNull(reader, "PCPerformanceId") && !DbUtils.IsDbNull(reader, "PerformanceId") )
                            {

                                PcPerformance pcPerformance = new PcPerformance()
                                {
                                    Id = DbUtils.GetInt(reader, "PCPerformanceId"),
                                    PerformanceId = DbUtils.GetInt(reader, "PerformanceId"),
                                    PCId = DbUtils.GetInt(reader, "PCId")
                                };

                                newViewModel.PcPerformance = pcPerformance;
                            }

                            if (!DbUtils.IsDbNull(reader, "PerformanceId") && !DbUtils.IsDbNull(reader, "FPS") && !DbUtils.IsDbNull(reader, "QualityId") && !DbUtils.IsDbNull(reader, "GameId") )
                            {

                                Performance performance = new Performance()
                                {
                                    Id = DbUtils.GetInt(reader, "PerformanceId"),
                                    FPS = DbUtils.GetInt(reader, "FPS"),
                                    QualityId = DbUtils.GetInt(reader, "QualityId"),
                                    GameId = DbUtils.GetInt(reader, "GameId")
                                };

                                newViewModel.Performance = performance;
                            }

                            if (!DbUtils.IsDbNull(reader, "QualityId") && !DbUtils.IsDbNull(reader, "QualityName"))
                            {
                                Quality quality = new Quality()
                                {
                                    Id = DbUtils.GetInt(reader, "QualityId"),
                                    Name = DbUtils.GetString(reader, "QualityName")
                                };

                                newViewModel.Quality = quality;
                            }

                            if (!DbUtils.IsDbNull(reader, "GameId") && !DbUtils.IsDbNull(reader, "GameName") && !DbUtils.IsDbNull(reader, "CategoryId"))
                            {
                                Game game = new Game()
                                {
                                    Id = DbUtils.GetInt(reader, "GameId"),
                                    Name = DbUtils.GetString(reader, "GameName"),
                                    CategoryId = DbUtils.GetInt(reader, "CategoryId")
                                };

                                newViewModel.Game = game;
                            }

                            if (!DbUtils.IsDbNull(reader, "CategoryId") && !DbUtils.IsDbNull(reader, "CategoryName"))
                            {
                                Category category = new Category()
                                {
                                    Id = DbUtils.GetInt(reader, "CategoryId"),
                                    Name = DbUtils.GetString(reader, "CategoryName")
                                };

                                newViewModel.Category = category;
                            }

                            pcs.Add(newViewModel);
                        }
                        return pcs;
                    }
                }
            }
        }
        
        public void AddPc(Pc pc)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO PC
                                       (UserProfileId,
                                        Name,
                                        Motherboard,
                                        CPU,
                                        Ram,
                                        GPU,
                                        PSU,
                                        Storage,
                                        CaseName,
                                        Cost)
                                        OUTPUT INSERTED.Id
                                        VALUES
                                       (@UserProfileId,
                                        @Name,
                                        @Motherboard,
                                        @CPU,
                                        @Ram,
                                        @GPU,
                                        @PSU,
                                        @Storage,
                                        @CaseName,
                                        @Cost)";

                    DbUtils.AddParameter(cmd, "@UserProfileId", pc.UserProfileId);
                    DbUtils.AddParameter(cmd, "@Name", pc.Name);
                    DbUtils.AddParameter(cmd, "@Motherboard", pc.Motherboard);
                    DbUtils.AddParameter(cmd, "@CPU", pc.CPU);
                    DbUtils.AddParameter(cmd, "@Ram", pc.Ram);
                    DbUtils.AddParameter(cmd, "@GPU", pc.GPU);
                    DbUtils.AddParameter(cmd, "@PSU", pc.PSU);
                    DbUtils.AddParameter(cmd, "@Storage", pc.Storage);
                    DbUtils.AddParameter(cmd, "@CaseName", pc.CaseName);
                    DbUtils.AddParameter(cmd, "@Cost", pc.Cost);

                    pc.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void UpdatePc(Pc pc)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE PC
                                        SET
                                            Name = @name,
                                            Motherboard = @motherboard,
                                            CPU = @cpu,
                                            Ram = @ram,
                                            GPU = @gpu,
                                            PSU = @psu,
                                            Storage = @storage,
                                            CaseName = @caseName,
                                            Cost = @cost
                                        WHERE Id = @id
                                        AND UserProfileId = @userProfileId";

                    DbUtils.AddParameter(cmd, "@name", pc.Name);
                    DbUtils.AddParameter(cmd, "@motherboard", pc.Motherboard);
                    DbUtils.AddParameter(cmd, "@cpu", pc.CPU);
                    DbUtils.AddParameter(cmd, "@ram", pc.Ram);
                    DbUtils.AddParameter(cmd, "@gpu", pc.GPU);
                    DbUtils.AddParameter(cmd, "@psu", pc.PSU);
                    DbUtils.AddParameter(cmd, "@storage", pc.Storage);
                    DbUtils.AddParameter(cmd, "@caseName", pc.CaseName);
                    DbUtils.AddParameter(cmd, "@cost", pc.Cost);
                    DbUtils.AddParameter(cmd, "@id", pc.Id);
                    DbUtils.AddParameter(cmd, "@userProfileId", pc.UserProfileId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeletePc(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM PC WHERE Id = @id";

                    DbUtils.AddParameter(cmd, "@id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        /*Should I use the firebaseUserId or just the UserProfileId?*/
        public List<PcAndPcPerformanceViewModel> GetPcByUserId(string firebaseUserId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT pc.Id,
	                                    pc.UserProfileId,
                                        pc.Name,
	                                    pc.Motherboard,
	                                    pc.CPU,
	                                    pc.Ram,
	                                    pc.GPU,
	                                    pc.PSU,
	                                    pc.Storage,
	                                    pc.CaseName,
	                                    pc.Cost,
                                        pp.Id AS PCPerformanceId,
	                                    pp.PerformanceId AS PerformanceId,
	                                    pp.PCId AS PCId,
	                                    p.FPS,
	                                    p.QualityId,
	                                    p.GameId,
	                                    q.Name AS QualityName,
	                                    g.Name AS GameName,
	                                    g.CategoryId,
	                                    c.Name AS CategoryName,
	                                    u.Id AS UserId,
	                                    u.FirstName,
	                                    u.LastName,
	                                    u.CreateDateTime,
	                                    u.Email,
	                                    u.DisplayName,
                                        u.FirebaseUserId
	                                    FROM Pc pc
	                                    LEFT JOIN UserProfile u ON pc.UserProfileId = u.Id
	                                    LEFT JOIN PCPerformance pp ON pc.Id = pp.PCId
	                                    LEFT JOIN Performance p ON pp.PerformanceId = p.Id
	                                    LEFT JOIN Quality q ON p.QualityId = q.Id
	                                    LEFT JOIN Game g ON p.GameId = g.Id
	                                    LEFT JOIN Category c ON g.CategoryId = c.Id
										WHERE u.FirebaseUserId = @firebaseUserId";

                    DbUtils.AddParameter(cmd, "@FirebaseUserId", firebaseUserId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var pc = new List<PcAndPcPerformanceViewModel>();
                        while (reader.Read())
                        {
                            PcAndPcPerformanceViewModel newViewModel = new PcAndPcPerformanceViewModel()
                            {
                                Pc = new Pc()
                                {
                                    Id = DbUtils.GetInt(reader, "Id"),
                                    Name = DbUtils.GetString(reader, "Name"),
                                    Motherboard = DbUtils.GetString(reader, "Motherboard"),
                                    CPU = DbUtils.GetString(reader, "CPU"),
                                    Ram = DbUtils.GetString(reader, "Ram"),
                                    GPU = DbUtils.GetString(reader, "GPU"),
                                    PSU = DbUtils.GetString(reader, "PSU"),
                                    Storage = DbUtils.GetString(reader, "Storage"),
                                    CaseName = DbUtils.GetString(reader, "CaseName"),
                                    Cost = DbUtils.GetString(reader, "Cost"),
                                    UserProfileId = DbUtils.GetInt(reader, "UserProfileId"),
                                    UserProfile = new UserProfile()
                                    {
                                        Id = DbUtils.GetInt(reader, "UserId"),
                                        FirstName = DbUtils.GetString(reader, "FirstName"),
                                        LastName = DbUtils.GetString(reader, "LastName"),
                                        DisplayName = DbUtils.GetString(reader, "DisplayName"),
                                        Email = DbUtils.GetString(reader, "Email"),
                                        CreateDateTime = DbUtils.GetDateTime(reader, "CreateDateTime"),
                                        FirebaseUserId = DbUtils.GetString(reader, "FirebaseUserId")
                                    }
                                }
                            };

                            if (!DbUtils.IsDbNull(reader, "PCPerformanceId") && !DbUtils.IsDbNull(reader, "PerformanceId"))
                            {

                                PcPerformance pcPerformance = new PcPerformance()
                                {
                                    Id = DbUtils.GetInt(reader, "PCPerformanceId"),
                                    PerformanceId = DbUtils.GetInt(reader, "PerformanceId"),
                                    PCId = DbUtils.GetInt(reader, "PCId")
                                };

                                newViewModel.PcPerformance = pcPerformance;
                            }

                            if (!DbUtils.IsDbNull(reader, "PerformanceId") && !DbUtils.IsDbNull(reader, "FPS") && !DbUtils.IsDbNull(reader, "QualityId") && !DbUtils.IsDbNull(reader, "GameId"))
                            {

                                Performance performance = new Performance()
                                {
                                    Id = DbUtils.GetInt(reader, "PerformanceId"),
                                    FPS = DbUtils.GetInt(reader, "FPS"),
                                    QualityId = DbUtils.GetInt(reader, "QualityId"),
                                    GameId = DbUtils.GetInt(reader, "GameId")
                                };

                                newViewModel.Performance = performance;
                            }

                            if (!DbUtils.IsDbNull(reader, "QualityId") && !DbUtils.IsDbNull(reader, "QualityName"))
                            {
                                Quality quality = new Quality()
                                {
                                    Id = DbUtils.GetInt(reader, "QualityId"),
                                    Name = DbUtils.GetString(reader, "QualityName")
                                };

                                newViewModel.Quality = quality;
                            }

                            if (!DbUtils.IsDbNull(reader, "GameId") && !DbUtils.IsDbNull(reader, "GameName") && !DbUtils.IsDbNull(reader, "CategoryId"))
                            {
                                Game game = new Game()
                                {
                                    Id = DbUtils.GetInt(reader, "GameId"),
                                    Name = DbUtils.GetString(reader, "GameName"),
                                    CategoryId = DbUtils.GetInt(reader, "CategoryId")
                                };

                                newViewModel.Game = game;
                            }

                            if (!DbUtils.IsDbNull(reader, "CategoryId") && !DbUtils.IsDbNull(reader, "CategoryName"))
                            {
                                Category category = new Category()
                                {
                                    Id = DbUtils.GetInt(reader, "CategoryId"),
                                    Name = DbUtils.GetString(reader, "CategoryName")
                                };

                                newViewModel.Category = category;
                            }

                            pc.Add(newViewModel);
                        }

                        return pc;
                    }
                }
            }
        }

        public Pc GetPcById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT pc.Id,
	                                    pc.UserProfileId,
                                        pc.Name,
	                                    pc.Motherboard,
	                                    pc.CPU,
	                                    pc.Ram,
	                                    pc.GPU,
	                                    pc.PSU,
	                                    pc.Storage,
	                                    pc.CaseName,
	                                    pc.Cost,
                                        u.Id AS UserId,
	                                    u.FirstName,
	                                    u.LastName,
	                                    u.CreateDateTime,
	                                    u.Email,
	                                    u.DisplayName,
                                        u.FirebaseUserId
	                                    FROM Pc pc
	                                    LEFT JOIN UserProfile u ON pc.UserProfileId = u.Id
                                        WHERE pc.Id = @Id";

                    DbUtils.AddParameter(cmd, "@Id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        Pc pc = null;
                        if (reader.Read())
                        {
                            pc = new Pc()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Motherboard = DbUtils.GetString(reader, "Motherboard"),
                                Name = DbUtils.GetString(reader, "Name"),
                                CPU = DbUtils.GetString(reader, "CPU"),
                                Ram = DbUtils.GetString(reader, "Ram"),
                                GPU = DbUtils.GetString(reader, "GPU"),
                                PSU = DbUtils.GetString(reader, "PSU"),
                                Storage = DbUtils.GetString(reader, "Storage"),
                                CaseName = DbUtils.GetString(reader, "CaseName"),
                                Cost = DbUtils.GetString(reader, "Cost"),
                                UserProfileId = DbUtils.GetInt(reader, "UserProfileId"),
                                UserProfile = new UserProfile()
                                {
                                    Id = DbUtils.GetInt(reader, "UserId"),
                                    FirstName = DbUtils.GetString(reader, "FirstName"),
                                    LastName = DbUtils.GetString(reader, "LastName"),
                                    DisplayName = DbUtils.GetString(reader, "DisplayName"),
                                    Email = DbUtils.GetString(reader, "Email"),
                                    CreateDateTime = DbUtils.GetDateTime(reader, "CreateDateTime"),
                                    FirebaseUserId = DbUtils.GetString(reader, "FirebaseUserId")
                                }
                            };
                        }

                        return pc;
                    }
                }
            }
        }
    }
}
