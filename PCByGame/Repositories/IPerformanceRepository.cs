using PCByGame.Models;

namespace PCByGame.Repositories
{
    public interface IPerformanceRepository
    {
        int AddPerformance(Performance performance);
        void UpdatePerformance(Performance performance);
    }
}
