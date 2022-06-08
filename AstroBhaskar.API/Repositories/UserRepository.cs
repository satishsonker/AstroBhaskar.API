

using AstroBhaskar.API.Data;
using AstroBhaskar.API.Dto.BaseDto;
using AstroBhaskar.API.Models;
using AstroBhaskar.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBhaskar.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AstroBhaskarDbContext _context;
        private readonly ICommonRepository _commonRepository;

        public UserRepository(AstroBhaskarDbContext context, ICommonRepository commonRepository)
        {
            _context = context;
            _commonRepository = commonRepository;
        }

        public async Task<int> Add(AstroUser entity)
        {
            if (!await _commonRepository.IsUserExist(entity.Email))
            {
                var permission = new UserPermission
                {
                    CanView = true
                };
                entity.Email = entity.Email.Trim();
                entity.FirstName = entity.FirstName.Trim();
                entity.LastName = entity.LastName.Trim();
                _context.AstroUsers.Add(entity);
                if (await _context.SaveChangesAsync() > 0)
                {
                    permission.UserId = entity.UserId;
                    permission.UserKey = entity.UserKey;
                    _context.UserPermissions.Add(permission);
                    return await _context.SaveChangesAsync();
                }
            }
            return 0;
        }

        public async Task<int> Delete(int id)
        {
            var user = await _context.AstroUsers
                .Where(x => x.UserId == id)
                .FirstOrDefaultAsync();
            if (user != null)
            {
                _context.Entry(user).State = EntityState.Deleted;
                return await _context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<AstroUser> Get(int id)
        {
            return await _context.AstroUsers.Where(x => x.UserId == id).FirstOrDefaultAsync();
        }

        public async Task<BasePagination<AstroUser>> GetAll(int pageNo, int pageSize)
        {
            var pagingModel = new BasePagination<AstroUser>();
            var data = await _context.AstroUsers.ToListAsync();
            pagingModel.TotalRecords = data.Count();
            pagingModel.PageNo = pageNo;
            pagingModel.PageSize = pageSize;
            pagingModel.Data = data.Skip(pageSize * (pageNo - 1)).Take(pageSize).ToList();
            return pagingModel;
        }

        public async Task<AstroUser> GetUserPermission(string email)
        {
            return await _context.AstroUsers.Include(x => x.UserPermission).Where(x => x.Email == email).FirstOrDefaultAsync();
        }

        public async Task<int> ResetPassword(string email, string resetCode)
        {
            var user = await _context.AstroUsers.Where(x => x.Email.ToLower().Trim() == email.ToLower().Trim()).FirstOrDefaultAsync();
            if (user != null)
            {
                user.isReset = true;
                user.ResetCode = resetCode;
                user.ResetExpireAt = DateTime.Now.AddMinutes(120);
                _context.Entry(user).State = EntityState.Modified;
                return await _context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<BasePagination<AstroUser>> Search(string searchTerm, int pageNo, int pageSize)
        {
            searchTerm = searchTerm == null ? string.Empty : searchTerm.ToLower().Trim();
            var pagingModel = new BasePagination<AstroUser>();
            var data = await _context.AstroUsers
                .Where(x =>
                    string.IsNullOrEmpty(searchTerm) ||
                    x.LastName.ToLower().Trim().Contains(searchTerm) ||
                    x.FirstName.ToLower().Trim().Contains(searchTerm) ||
                    x.Email.ToLower().Trim().Contains(searchTerm))
                .ToListAsync();
            pagingModel.TotalRecords = data.Count();
            pagingModel.PageNo = pageNo;
            pagingModel.PageSize = pageSize;
            pagingModel.Data = data.Skip(pageSize * (pageNo - 1)).Take(pageSize).ToList();
            return pagingModel;
        }

        public async Task<int> ToggleBlockUser(string email, bool isBlocked)
        {
            var user = await _context.AstroUsers.Where(x => x.Email.ToLower().Trim() == email.ToLower().Trim()).FirstOrDefaultAsync();
            if (user != null)
            {
                user.isBlocked = isBlocked;
                _context.Entry(user).State = EntityState.Modified;
                return await _context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<int> ToggleLockUser(string email, bool isLocked)
        {
            var user = await _context.AstroUsers.Where(x => x.Email.ToLower().Trim() == email.ToLower().Trim()).FirstOrDefaultAsync();
            if (user != null)
            {
                user.isLocked = isLocked;
                _context.Entry(user).State = EntityState.Modified;
                return await _context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<int> Update(AstroUser entity)
        {
            var user = await _context.AstroUsers.Where(x => x.Email.ToLower().Trim() == entity.Email.ToLower().Trim()).FirstOrDefaultAsync();
            if (user != null)
            {
                user.FirstName = entity.FirstName;
                user.LastName = entity.LastName;
                _context.Entry(user).State = EntityState.Modified;
                return await _context.SaveChangesAsync();
            }
            return 0;
        }
    }
}
