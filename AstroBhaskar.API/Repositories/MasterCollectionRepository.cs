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
    public class MasterCollectionRepository : IMasterCollectionRepository
    {
        private readonly AstroBhaskarDbContext _context;
        public MasterCollectionRepository(AstroBhaskarDbContext context)
        {
            _context = context;
        }
        public async Task<int> Add(MasterCollection entity)
        {
            if (await _context.MasterCollections.Where(x =>
                                                            x.Key == entity.Key &&
                                                            x.Value == entity.Value &&
                                                            x.DisplayValue == entity.DisplayValue &&
                                                            x.CollectionName == entity.CollectionName).CountAsync() > 0)
                return 2;
            await _context.MasterCollections.AddAsync(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(int id)
        {
            MasterCollection subscribeNewsLetter = await _context.MasterCollections.Where(x => x.CollectionId == id).FirstOrDefaultAsync();
            if (subscribeNewsLetter != null)
            {
                _context.Entry(subscribeNewsLetter).State = EntityState.Modified;
                return await _context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<int> Delete(string collectionName)
        {
            MasterCollection subscribeNewsLetter = await _context.MasterCollections.Where(x => x.CollectionName.ToLower().Trim() == collectionName.ToLower().Trim()).FirstOrDefaultAsync();
            if (subscribeNewsLetter != null)
            {
                _context.Entry(subscribeNewsLetter).State = EntityState.Modified;
                return await _context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<int> Delete(string collectionName, string key)
        {
            MasterCollection subscribeNewsLetter = await _context.MasterCollections
                .Where(x =>
                    x.CollectionName.ToLower().Trim() == collectionName.ToLower().Trim() &&
                    x.Key.ToLower().Trim() == key.ToLower().Trim())
                .FirstOrDefaultAsync();
            if (subscribeNewsLetter != null)
            {
                _context.Entry(subscribeNewsLetter).State = EntityState.Modified;
                return await _context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<MasterCollection> Get(int id)
        {
            return await _context.MasterCollections.Where(x => x.CollectionId == id).FirstOrDefaultAsync();
        }
        public async Task<BasePagination<MasterCollection>> Get(string collectionName, int pageNo, int pageSize)
        {
            BasePagination<MasterCollection> pagingModel = new BasePagination<MasterCollection>();
            var data = await _context.MasterCollections
                .Where(x => x.CollectionName.ToLower().Trim() == collectionName.ToLower().Trim())
                .ToListAsync();
            pagingModel.TotalRecords = data.Count();
            pagingModel.PageNo = pageNo;
            pagingModel.PageSize = pageSize;
            pagingModel.Data = data.Skip(pageSize * (pageNo - 1)).Take(pageSize).ToList();
            return pagingModel;
        }

        public async Task<BasePagination<MasterCollection>> GetAll(int pageNo, int pageSize)
        {
            BasePagination<MasterCollection> pagingModel = new BasePagination<MasterCollection>();
            var data = await _context.MasterCollections.ToListAsync();
            pagingModel.TotalRecords = data.Count();
            pagingModel.PageNo = pageNo;
            pagingModel.PageSize = pageSize;
            pagingModel.Data = data.Skip(pageSize * (pageNo - 1)).Take(pageSize).ToList();
            return pagingModel;
        }



        public async Task<BasePagination<MasterCollection>> Search(string searchTerm, int pageNo, int pageSize)
        {
            searchTerm = searchTerm == null ? string.Empty : searchTerm.ToLower().Trim();
            BasePagination<MasterCollection> pagingModel = new BasePagination<MasterCollection>();
            var data = await _context.MasterCollections.Where(x => string.IsNullOrEmpty(searchTerm) ||
            x.Key.ToLower().Trim().Contains(searchTerm) ||
             x.Value.ToLower().Trim().Contains(searchTerm) ||
              x.DisplayValue.ToLower().Trim().Contains(searchTerm) ||
              x.CollectionName.ToLower().Trim().Contains(searchTerm)
            ).ToListAsync();
            pagingModel.TotalRecords = data.Count();
            pagingModel.PageNo = pageNo;
            pagingModel.PageSize = pageSize;
            pagingModel.Data = data.Skip(pageSize * (pageNo - 1)).Take(pageSize).ToList();
            return pagingModel;
        }

        public async Task<int> Update(MasterCollection entity)
        {
            MasterCollection subscribeNewsLetter;
            subscribeNewsLetter = await _context.MasterCollections.Where(x => x.CollectionId == entity.CollectionId).FirstOrDefaultAsync();
            if (subscribeNewsLetter != null)
            {
                subscribeNewsLetter.Key = entity.Key;
                subscribeNewsLetter.Value = entity.Value;
                subscribeNewsLetter.DisplayValue = entity.DisplayValue;
                subscribeNewsLetter.CollectionName = entity.CollectionName;
                subscribeNewsLetter.UpdatedAt = DateTime.Now;
                _context.Entry(subscribeNewsLetter).State = EntityState.Modified;
                return await _context.SaveChangesAsync();
            }
            return 0;
        }
    }
}
