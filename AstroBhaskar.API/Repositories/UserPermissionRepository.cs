using AstroBhaskar.API.Data;
using AstroBhaskar.API.Dto.BaseDto;
using AstroBhaskar.API.Models;
using AstroBhaskar.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBhaskar.API.Repositories
{
    public class UserPermissionRepository : IUserPermissionRepository
    {
        private readonly AstroBhaskarDbContext _context;
        private readonly ICommonRepository _commonRepository;

        public UserPermissionRepository(AstroBhaskarDbContext context, ICommonRepository commonRepository)
        {
            _context = context;
            _commonRepository = commonRepository;
        }

        public async Task<int> Add(UserPermission entity)
        {
            if (!await _commonRepository.IsPermissionExist(entity.UserId))
            {
                _context.UserPermissions.Add(entity);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<int> Delete(int userId)
        {
            var userPermission = await _context.UserPermissions.Where(x => x.UserId == userId).FirstOrDefaultAsync();
            if (userPermission != null)
            {
                _context.Entry(userPermission).State = EntityState.Deleted;
                return await _context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<UserPermission> Get(int id)
        {
            return await _context.UserPermissions.Where(x => x.UserId == id).FirstOrDefaultAsync();
        }

        public async Task<BasePagination<UserPermission>> GetAll(int pageNo, int pageSize)
        {
            var pagingModel = new BasePagination<UserPermission>();
            var data = await _context.UserPermissions.ToListAsync();
            pagingModel.TotalRecords = data.Count();
            pagingModel.PageNo = pageNo;
            pagingModel.PageSize = pageSize;
            pagingModel.Data = data.Skip(pageSize * (pageNo - 1)).Take(pageSize).ToList();
            return pagingModel;

        }

        public async Task<BasePagination<UserPermission>> Search(string searchTerm, int pageNo, int pageSize)
        {
            searchTerm = searchTerm.ToLower();
            var pagingModel = new BasePagination<UserPermission>();
            var data = await _context.UserPermissions.Include(x => x.AstroUser).Where(x => x.AstroUser.Email.ToLower().Contains(searchTerm) || x.AstroUser.FirstName.ToLower().Contains(searchTerm) || x.AstroUser.LastName.ToLower().Contains(searchTerm)).ToListAsync();
            pagingModel.TotalRecords = data.Count();
            pagingModel.PageNo = pageNo;
            pagingModel.PageSize = pageSize;
            pagingModel.Data = data.Skip(pageSize * (pageNo - 1)).Take(pageSize).ToList();
            return pagingModel;
        }

        public async Task<int> Update(UserPermission entity)
        {
            if (await Delete(entity.UserId) > 0)
                return await Add(entity);
            return 0;
        }
    }
}
