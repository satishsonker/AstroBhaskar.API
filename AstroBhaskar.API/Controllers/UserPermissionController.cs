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
    public class UserPermissionController : ControllerBase
    {
        private readonly IUserPermission _userPermission;
        public UserPermissionController(IUserPermission userPermission)
        {
            _userPermission = userPermission;
        }

        [HttpPut]
        [Route(StaticValues.UserPermissionPath)]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<int> Add(UserPermissionRequest entity)
        {
            return await _userPermission.Add(entity);
        }

        [HttpDelete]
        [Route(StaticValues.UserPermissionPath)]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<int> Delete(int id)
        {
            return await _userPermission.Delete(id);
        }

        [HttpGet]
        [Route(StaticValues.UserPermissionGetByIdPath)]
        [ProducesResponseType(typeof(UserPermissionResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<UserPermissionResponse> Get(int id)
        {
            return await _userPermission.Get(id);
        }

        [HttpGet]
        [Route(StaticValues.UserPermissionPath)]
        [ProducesResponseType(typeof(PagingResponse<UserPermissionResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<PagingResponse<UserPermissionResponse>> GetAll(int pageNo, int pageSize)
        {
            return await _userPermission.GetAll(pageNo, pageSize);
        }

        [HttpGet]
        [Route(StaticValues.UserPermissionSearchPath)]
        [ProducesResponseType(typeof(PagingResponse<UserPermissionResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<PagingResponse<UserPermissionResponse>> Search(string searchTerm, int pageNo, int pageSize)
        {
            return await _userPermission.Search(searchTerm, pageNo, pageSize);
        }

        [HttpPost]
        [Route(StaticValues.UserPermissionPath)]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<int> Update(UserPermissionRequest entity)
        {
            return await _userPermission.Update(entity);
        }
    }
}
