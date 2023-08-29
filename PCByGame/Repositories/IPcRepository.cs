using PCByGame.Models;
using PCByGame.Models.ViewModels;
using System.Collections.Generic;

namespace PCByGame.Repositories
{
    public interface IPcRepository
    {
        void AddPc(Pc pc);
        void DeletePc(int id);
        List<PcAndPcPerformanceViewModel> GetAll();
        Pc GetPcById(int id);
        List<PcAndPcPerformanceViewModel> GetPcByUserId(string firebaseUserId);
        void UpdatePc(Pc pc);
    }
}
