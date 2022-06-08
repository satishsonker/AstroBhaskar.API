using AstroBhaskar.API.Constants;
using AstroBhaskar.API.Dto.Request;
using AstroBhaskar.API.Dto.Response;
using AstroBhaskar.API.Exceptions;
using AstroBhaskar.API.Models;
using AstroBhaskar.API.Repositories.Interfaces;
using AstroBhaskar.API.Services.Interfaces;
using AutoMapper;
using System.Threading.Tasks;

namespace AstroBhaskar.API.Services
{
    public class MasterCollectionService : IMasterCollectionService
    {
        private readonly IMasterCollectionRepository _masterCollectionRepository;
        private readonly ICommonRepository _commonRepository;
        private readonly IMapper _mapper;
        public MasterCollectionService(IMapper mapper, IMasterCollectionRepository masterCollectionRepository, ICommonRepository commonRepository)
        {
            _masterCollectionRepository = masterCollectionRepository;
            _commonRepository = commonRepository;
            _mapper = mapper;
        }

        public async Task<int> Add(MasterCollectionRequest entity)
        {
            MasterCollection masterCollection = _mapper.Map<MasterCollection>(entity);
            if (await _commonRepository.IsMasterCollectionExist(masterCollection.CollectionName, masterCollection.Key))
            {
                throw new BusinessRuleViolationException(StaticValues.MasterCollectionAlreadyExist, StaticValues.MasterCollectionAlreadyExistMessage);
            }
            return await _masterCollectionRepository.Add(masterCollection);
        }

        public async Task<int> Delete(int collectionId)
        {
            if (!await _commonRepository.IsMasterCollectionExist(collectionId))
            {
                throw new BusinessRuleViolationException(StaticValues.MasterCollectionNotExist, StaticValues.MasterCollectionNotExistMessage);
            }
            return await _masterCollectionRepository.Delete(collectionId);
        }

        public async Task<int> Delete(string collectionName)
        {
            if (!await _commonRepository.IsMasterCollectionExist(collectionName))
            {
                throw new BusinessRuleViolationException(StaticValues.MasterCollectionNotExist, StaticValues.MasterCollectionNotExistMessage);
            }
            return await _masterCollectionRepository.Delete(collectionName);
        }
        public async Task<int> Delete(string collectionName, string key)
        {
            if (!await _commonRepository.IsMasterCollectionExist(collectionName, key))
            {
                throw new BusinessRuleViolationException(StaticValues.MasterCollectionNotExist, StaticValues.MasterCollectionNotExistMessage);
            }
            return await _masterCollectionRepository.Delete(collectionName, key);
        }

        public async Task<PagingResponse<MasterCollectionResponse>> Get(string collectionName, int pageNo, int pageSize)
        {
            if (await _commonRepository.IsMasterCollectionExist(collectionName))
            {
                throw new BusinessRuleViolationException(StaticValues.MasterCollectionNotExist, StaticValues.MasterCollectionNotExistMessage);
            }
            return _mapper.Map<PagingResponse<MasterCollectionResponse>>(await _masterCollectionRepository.Get(collectionName, pageNo, pageSize));
        }

        public async Task<MasterCollectionResponse> Get(int id)
        {
            return !await _commonRepository.IsMasterCollectionExist(id)
                ? new MasterCollectionResponse()
                : _mapper.Map<MasterCollectionResponse>(await _masterCollectionRepository.Get(id));
        }

        public async Task<PagingResponse<MasterCollectionResponse>> GetAll(int pageNo, int pageSize)
        {
            return _mapper.Map<PagingResponse<MasterCollectionResponse>>(await _masterCollectionRepository.GetAll(pageNo, pageSize));
        }

        public async Task<PagingResponse<MasterCollectionResponse>> Search(string searchTerm, int pageNo, int pageSize)
        {
            return _mapper.Map<PagingResponse<MasterCollectionResponse>>(await _masterCollectionRepository.Search(searchTerm, pageNo, pageSize));
        }

        public async Task<int> Update(MasterCollectionRequest entity)
        {
            if (!await _commonRepository.IsMasterCollectionExist(entity.CollectionId))
            {
                throw new BusinessRuleViolationException(StaticValues.MasterCollectionNotExist, StaticValues.MasterCollectionNotExistMessage);
            }
            MasterCollection masterCollection = _mapper.Map<MasterCollection>(entity);
            return await _masterCollectionRepository.Update(masterCollection);
        }
    }
}
