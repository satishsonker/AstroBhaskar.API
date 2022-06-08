using AstroBhaskar.API.Data;
using AstroBhaskar.API.Dto.BaseDto;
using AstroBhaskar.API.Models;
using AstroBhaskar.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBhaskar.API.Repositories
{
    public class SubscribeNewsLetterRepository : ISubscribeNewsLetterRepository
    {
        private readonly AstroBhaskarDbContext _context;
        public SubscribeNewsLetterRepository(AstroBhaskarDbContext appDbContext)
        {
            _context = appDbContext;
        }
        public async Task<int> Add(SubscribeNewsLetter entity)
        {
            if (await _context.SubscribeNewsLetters.Where(x => x.Email == entity.Email && !x.IsDeleted).CountAsync() > 0)
                return 2;
            _context.SubscribeNewsLetters.Add(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(SubscribeNewsLetter subscribeNewsLetter)
        {
            try
            {
                var oldSubscribeNewsLetter = await _context.SubscribeNewsLetters.Where(x => x.Email.ToLower().Contains(subscribeNewsLetter.Email.ToLower())).FirstOrDefaultAsync();
                if (oldSubscribeNewsLetter != null)
                {
                    oldSubscribeNewsLetter.UpdatedAt = DateTime.Now;
                    oldSubscribeNewsLetter.UnsubscribeReason = subscribeNewsLetter.UnsubscribeReason;
                    oldSubscribeNewsLetter.IsDeleted = true;
                    _context.Entry(oldSubscribeNewsLetter).State = EntityState.Modified;
                    return await _context.SaveChangesAsync();
                }
                return 0;
            }
            catch (Exception ex)
            {

                return 0;
            }
        }

        public async Task<int> Delete(int id)
        {
            var subscribeNewsLetter = await _context.SubscribeNewsLetters.Where(x => x.SubscribeNewsLetterId == id).FirstOrDefaultAsync();
            if (subscribeNewsLetter != null)
            {
                subscribeNewsLetter.UpdatedAt = DateTime.Now;
                subscribeNewsLetter.IsDeleted = true;
                _context.Entry(subscribeNewsLetter).State = EntityState.Modified;
                return await _context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<SubscribeNewsLetter> Get(int id)
        {
            return await _context.SubscribeNewsLetters.Where(x => x.SubscribeNewsLetterId == id).FirstOrDefaultAsync();
        }
        public async Task<SubscribeNewsLetter> Get(string email)
        {
            return await _context.SubscribeNewsLetters.Where(x => x.Email == email).FirstOrDefaultAsync();
        }

        public async Task<BasePagination<SubscribeNewsLetter>> GetAll(int pageNo, int pageSize)
        {
            var pagingModel = new BasePagination<SubscribeNewsLetter>();
            var data = await _context.SubscribeNewsLetters.ToListAsync();
            pagingModel.TotalRecords = data.Count();
            pagingModel.PageNo = pageNo;
            pagingModel.PageSize = pageSize;
            pagingModel.Data = data.Skip(pageSize * (pageNo - 1)).Take(pageSize).ToList();
            return pagingModel;
        }

        public async Task<BasePagination<SubscribeNewsLetter>> Search(string searchTerm, int pageNo, int pageSize)
        {
            searchTerm = searchTerm == null ? string.Empty : searchTerm.ToLower();
            var pagingModel = new BasePagination<SubscribeNewsLetter>();
            var data = await _context.SubscribeNewsLetters.Where(x => string.IsNullOrEmpty(searchTerm) || x.Email.ToLower().Contains(searchTerm)).ToListAsync();
            pagingModel.TotalRecords = data.Count();
            pagingModel.PageNo = pageNo;
            pagingModel.PageSize = pageSize;
            pagingModel.Data = data.Skip(pageSize * (pageNo - 1)).Take(pageSize).ToList();
            return pagingModel;
        }

        public async Task<int> Update(SubscribeNewsLetter entity)
        {
            var subscribeNewsLetter = await _context.SubscribeNewsLetters.Where(x => x.Email == entity.Email).FirstOrDefaultAsync();
            if (subscribeNewsLetter != null)
            {
                subscribeNewsLetter.Email = entity.Email;
                subscribeNewsLetter.UpdatedAt = DateTime.Now;
                _context.Entry(subscribeNewsLetter).State = EntityState.Modified;
                return await _context.SaveChangesAsync();
            }
            return 0;
        }
        public async Task<IEnumerable<MasterCollection>> GetUnsubscribeReason(string collectionName)
        {
            return await _context.MasterCollections.Where(x => x.CollectionName.ToLower().Contains(collectionName.ToLower())).ToListAsync();
        }
    }
}
