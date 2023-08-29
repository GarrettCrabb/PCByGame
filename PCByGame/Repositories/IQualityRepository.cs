using PCByGame.Models;
using System.Collections.Generic;

namespace PCByGame.Repositories
{
    public interface IQualityRepository
    {
        List<Quality> GetAll();
    }
}
