using PCByGame.Models;
using System.Collections.Generic;

namespace PCByGame.Repositories
{
    public interface IUserProfileRepository
    {
        void Add(UserProfile userProfile);
        List<UserProfile> GetAll();
        UserProfile GetByFirebaseUserId(string firebaseUserId);
        UserProfile GetById(int id);
        void Update(UserProfile userProfile);
    }
}
