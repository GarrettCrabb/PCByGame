using PCByGame.Models;
using PCByGame.Models.ViewModels;
using System.Collections.Generic;

namespace PCByGame.Repositories
{
    public interface IPcPerformanceRepository
    {
        void AddPcPerformance(PcPerformance pcPerformance);
        List<PcPerformanceGetAllViewModel> GetAllByPcId(int id);
        void UpdatePcPerformance(PcPerformance pcPerformance);
    }
}
