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
    public class UserPermissionService : IUserPermission
    {
        private readonly ICommonRepository _commonRepository;
        private readonly IMapper _mapper;
        private readonly IUserPermissionRepository _userPermissionRepository;
        private readonly IUserRepository _userRepository;

        public UserPermissionService(IMapper mapper, IUserPermissionRepository userPermissionRepository,
            IUserRepository userRepository, ICommonRepository commonRepository)
        {
            _userPermissionRepository = userPermissionRepository;
            _userRepository = userRepository;
            _commonRepository = commonRepository;
            _mapper = mapper;
        }

        public async Task<int> Add(UserPermissionRequest entity)
        {
            var hasUser = await _commonRepository.IsUserExist(entity.UserId);
            if (!hasUser) throw new NotFoundException(StaticValues.UserNotFound, StaticValues.UserNotFoundMessage);

            var existingPermission = _userPermissionRepository.Get(entity.UserId);

            if (existingPermission != null)
                throw new BusinessRuleViolationException(StaticValues.UserNotFound, StaticValues.UserNotFoundMessage);

            return await _userPermissionRepository.Add(_mapper.Map<UserPermission>(entity));
        }

        public async Task<int> Delete(int userId)
        {
            var hasUser = await _commonRepository.IsUserExist(userId);
            if (!hasUser) throw new NotFoundException(StaticValues.UserNotFound, StaticValues.UserNotFoundMessage);

            return await _userPermissionRepository.Delete(userId);
        }

        public async Task<UserPermissionResponse> Get(int userId)
        {
            var hasPermission = await _commonRepository.IsPermissionExist(userId);
            if (!hasPermission) throw new NotFoundException(StaticValues.UserPermissionNotFound, StaticValues.UserPermissionNotFoundMessage);
            UserPermission userPermission = await _userPermissionRepository.Get(userId);
            return _mapper.Map<UserPermissionResponse>(userPermission);
        }

        public async Task<PagingResponse<UserPermissionResponse>> GetAll(int pageNo, int pageSize)
        {
            return _mapper.Map<PagingResponse<UserPermissionResponse>>(await _userPermissionRepository.GetAll(pageNo, pageSize));
        }

        public async Task<PagingResponse<UserPermissionResponse>> Search(string searchTerm, int pageNo, int pageSize)
        {
            return _mapper.Map<PagingResponse<UserPermissionResponse>>(await _userPermissionRepository.Search(searchTerm, pageNo, pageSize));
        }

        public async Task<int> Update(UserPermissionRequest entity)
        {
            if (entity == null)
            {
                throw new BusinessRuleViolationException(StaticValues.UserPermissionRequired, StaticValues.UserPermissionRequiredMessage);
            }
            if (!await _commonRepository.IsPermissionExist(entity.UserId))
            {
                throw new BusinessRuleViolationException(StaticValues.UserPermissionRequired, StaticValues.UserPermissionRequiredMessage);
            }
            UserPermission userPermission = _mapper.Map<UserPermission>(entity);
            return await _userPermissionRepository.Update(userPermission);
        }
    }
}