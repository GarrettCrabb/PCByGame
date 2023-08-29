using PCByGame.Models;
using System.Collections.Generic;

namespace PCByGame.Repositories
{
    public interface IGameRepository
    {
        List<Game> GetAll();
    }
}
