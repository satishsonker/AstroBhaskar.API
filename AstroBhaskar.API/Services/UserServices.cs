using AstroBhaskar.API.Constants;
using AstroBhaskar.API.Dto.Request;
using AstroBhaskar.API.Dto.Response;
using AstroBhaskar.API.Exceptions;
using AstroBhaskar.API.Models;
using AstroBhaskar.API.Repositories.Interfaces;
using AstroBhaskar.API.Services.Interfaces;
using AstroBhaskar.API.Utils;
using AutoMapper;
using System.Threading.Tasks;

namespace AstroBhaskar.API.Services
{
    public class UserServices : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly ICommonRepository _commonRepository;

        public UserServices(IUserRepository userRepository, ICommonRepository commonRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _commonRepository = commonRepository;
            _mapper = mapper;
        }

        public async Task<int> Add(UserRequest entity)
        {
            if (entity == null)
            {
                throw new BusinessRuleViolationException(StaticValues.UserRequired, StaticValues.UserRequiredMessage);
            }
            if (await _commonRepository.IsUserExist(entity.Email))
            {
                throw new BusinessRuleViolationException(StaticValues.UserAlreadyExist, StaticValues.UserAlreadyExistMessage);
            }
            var user = _mapper.Map<AstroUser>(entity);
            user.UserKey = CommonUtil.GetGUID(2);
            return await _userRepository.Add(user);
        }

        public async Task<int> Delete(int userId)
        {
            if (!await _commonRepository.IsUserExist(userId))
            {
                throw new BusinessRuleViolationException(StaticValues.UserNotFound, StaticValues.UserNotFoundMessage);
            }
            return await _userRepository.Delete(userId);
        }

        public async Task<UserResponse> Get(int id)
        {
            return _mapper.Map<UserResponse>(await _userRepository.Get(id));
        }

        public async Task<PagingResponse<UserResponse>> GetAll(int pageNo, int pageSize)
        {
            return _mapper.Map<PagingResponse<UserResponse>>(await _userRepository.GetAll(pageNo, pageSize));
        }

        public async Task<int> ResetPassword(string email)
        {
            if (!await _commonRepository.IsUserExist(email))
            {
                throw new BusinessRuleViolationException(StaticValues.UserNotFound, StaticValues.UserNotFoundMessage);
            }
            var resetCode = CommonUtil.GetGUID();
            return await _userRepository.ResetPassword(email, resetCode);
        }

        public async Task<PagingResponse<UserResponse>> Search(string searchTerm, int pageNo, int pageSize)
        {
            return _mapper.Map<PagingResponse<UserResponse>>(
                await _userRepository.Search(searchTerm, pageNo, pageSize));
        }

        public async Task<int> ToggleBlockUser(string email, bool isBlocked)
        {
            if (!await _commonRepository.IsUserExist(email))
            {
                throw new BusinessRuleViolationException(StaticValues.UserNotFound, StaticValues.UserNotFoundMessage);
            }
            return await _userRepository.ToggleBlockUser(email, isBlocked);
        }

        public async Task<int> ToggleLockUser(string email, bool isLocked)
        {
            if (!await _commonRepository.IsUserExist(email))
            {
                throw new BusinessRuleViolationException(StaticValues.UserNotFound, StaticValues.UserNotFoundMessage);
            }
            return await _userRepository.ToggleLockUser(email, isLocked);
        }

        public async Task<int> Update(UserRequest entity)
        {
            if (entity == null)
            {
                throw new BusinessRuleViolationException(StaticValues.UserRequired, StaticValues.UserRequiredMessage);
            }
            if (!await _commonRepository.IsUserExist(entity.Email))
            {
                throw new BusinessRuleViolationException(StaticValues.UserNotFound, StaticValues.UserNotFoundMessage);
            }
            var user = _mapper.Map<AstroUser>(entity);
            return await _userRepository.Update(user);
        }
    }
}