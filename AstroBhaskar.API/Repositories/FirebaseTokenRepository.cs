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
    public class FirebaseTokenRepository : IFirebaseTokenRepository
    {
        private readonly AstroBhaskarDbContext _context;
        private readonly ICommonRepository _commonRepository;
        public FirebaseTokenRepository(AstroBhaskarDbContext context, ICommonRepository commonRepository)
        {
            _context = context;
            _commonRepository = commonRepository;
        }

        public async Task<int> Add(FirebaseToken entity)
        {
            if (!await _commonRepository.IsUserExist(entity.UserEmail))
            {
                await _context.FirebaseTokens.AddAsync(entity);
                return await _context.SaveChangesAsync();
            }
            return await Update(entity);
        }

        public async Task<int> Delete(int id)
        {
            var firebaseToken = await _context.FirebaseTokens.Where(x => x.TokenId == id).FirstOrDefaultAsync();
            if (firebaseToken != null)
            {
                _context.Entry(firebaseToken).State = EntityState.Deleted;
                return await _context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<FirebaseToken> Get(int id)
        {
            return await _context.FirebaseTokens.Where(x => x.TokenId == id).FirstOrDefaultAsync();
        }

        public async Task<BasePagination<FirebaseToken>> GetAll(int pageNo, int pageSize)
        {
            var pagingModel = new BasePagination<FirebaseToken>();
            var data = await _context.FirebaseTokens.ToListAsync();
            pagingModel.TotalRecords = data.Count();
            pagingModel.PageNo = pageNo;
            pagingModel.PageSize = pageSize;
            pagingModel.Data = data.Skip(pageSize * (pageNo - 1)).Take(pageSize).ToList();
            return pagingModel;

        }

        public async Task<BasePagination<FirebaseToken>> Search(string searchTerm, int pageNo, int pageSize)
        {
            searchTerm = searchTerm.ToLower();
            var pagingModel = new BasePagination<FirebaseToken>();
            var data = await _context.FirebaseTokens.Where(x => x.Token.ToLower().Contains(searchTerm) || x.UserEmail.ToLower().Contains(searchTerm)).ToListAsync();
            pagingModel.TotalRecords = data.Count();
            pagingModel.PageNo = pageNo;
            pagingModel.PageSize = pageSize;
            pagingModel.Data = data.Skip(pageSize * (pageNo - 1)).Take(pageSize).ToList();
            return pagingModel;
        }

        public async Task<int> Update(FirebaseToken entity)
        {
            var firebaseToken = await _context.FirebaseTokens.Where(x => x.UserEmail == entity.UserEmail).FirstOrDefaultAsync();
            if (firebaseToken != null)
            {
                firebaseToken.Token = entity.Token;
                firebaseToken.UpdatedAt = DateTime.Now;
                return await _context.SaveChangesAsync();
            }
            return 0;
        }
    }
}
