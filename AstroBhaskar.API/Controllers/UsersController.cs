using AstroBhaskar.API.Constants;
using AstroBhaskar.API.Dto.Request;
using AstroBhaskar.API.Dto.Response;
using AstroBhaskar.API.Dto.Response.GlobalResponse;
using AstroBhaskar.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AstroBhaskar.API.Controllers
{
    [Route(StaticValues.ApiRoutePrefix)]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route(StaticValues.UserPath)]
        [ProducesResponseType(typeof(PagingResponse<UserResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<PagingResponse<UserResponse>> GetAll([FromQuery] int pageNo, [FromQuery] int pageSize)
        {
            return await _userService.GetAll(pageNo, pageSize);
        }

        [HttpGet]
        [Route(StaticValues.UserGetByIdPath)]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<UserResponse> Get(int id)
        {
            return await _userService.Get(id);
        }

        [HttpPut]
        [Route(StaticValues.UserPath)]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<int> Add([FromBody] UserRequest entity)
        {
            return await _userService.Add(entity);
        }

        [HttpPost]
        [Route(StaticValues.UserPath)]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<int> Update(UserRequest entity)
        {
            return await _userService.Update(entity);
        }

        [HttpDelete]
        [Route(StaticValues.UserPath)]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<int> Delete([FromQuery] int id)
        {
            return await _userService.Delete(id);
        }

        [HttpGet]
        [Route(StaticValues.UserSearchPath)]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<PagingResponse<UserResponse>> Search([FromQuery] string searchTerm, [FromQuery] int pageNo, [FromQuery] int pageSize)
        {
            return await _userService.Search(searchTerm, pageNo, pageSize);
        }

        [HttpPost]
        [Route(StaticValues.UserResetPasswordPath)]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<int> ResetPassword(string email)
        {
            return await _userService.ResetPassword(email);
        }

        [HttpPost]
        [Route(StaticValues.UserToggleLockPath)]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<int> ToggleLockUser(string email, bool isLocked)
        {
            return await _userService.ToggleLockUser(email, isLocked);
        }

        [HttpPost]
        [Route(StaticValues.UserToggleBlockPath)]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<int> ToggleBlockUser(string email, bool isBlocked)
        {
            return await _userService.ToggleBlockUser(email, isBlocked);
        }
    }
}
