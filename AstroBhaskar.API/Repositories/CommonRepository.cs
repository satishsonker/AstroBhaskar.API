using AstroBhaskar.API.Data;
using AstroBhaskar.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBhaskar.API.Repositories
{
    public class CommonRepository : ICommonRepository
    {
        private readonly AstroBhaskarDbContext _context;
        public CommonRepository(AstroBhaskarDbContext context)
        {
            _context = context;
        }
        public async Task<bool> IsUserExist(string email)
        {
            return await _context.AstroUsers.Where(x => x.Email == email).CountAsync() > 0;
        }

        public async Task<bool> IsPermissionExist(int userId)
        {
            return await _context.UserPermissions.Where(x => x.UserId == userId).CountAsync() > 0;
        }

        public async Task<bool> IsUserExist(int userId)
        {
            return await _context.AstroUsers.Where(x => x.UserId == userId).CountAsync() > 0;
        }

        public async Task<bool> IsMasterCollectionExist(string collectionName)
        {
            return await _context.MasterCollections
                .Where(x => x.CollectionName.ToLower().Trim() == collectionName.ToLower().Trim())
                .CountAsync() > 0;
        }

        public async Task<bool> IsMasterCollectionExist(string collectionName, string key)
        {
            return await _context.MasterCollections
               .Where(x =>
               x.CollectionName.ToLower().Trim() == collectionName.ToLower().Trim() &&
               x.Key.ToLower().Trim() == key.ToLower().Trim())
               .CountAsync() > 0;
        }

        public async Task<bool> IsMasterCollectionExist(int collectionId)
        {
            return await _context.MasterCollections
              .Where(x =>
              x.CollectionId == collectionId)
              .CountAsync() > 0;
        }
    }
}
