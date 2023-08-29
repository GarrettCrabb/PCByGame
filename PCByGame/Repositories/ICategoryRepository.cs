using PCByGame.Models;
using PCByGame.Models.ViewModels;
using System.Collections.Generic;

namespace PCByGame.Repositories
{
    public interface ICategoryRepository
    {
        List<Category> GetAll();
    }
}
