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
    public class MasterCollectionController : ControllerBase
    {
        private readonly IMasterCollectionService _masterCollectionService;
        public MasterCollectionController(IMasterCollectionService masterCollectionService)
        {
            _masterCollectionService = masterCollectionService;
        }

        [HttpPut]
        [Route(StaticValues.MasterCollectionPath)]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MasterCollectionResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<int> Add([FromBody] MasterCollectionRequest entity)
        {
            return await _masterCollectionService.Add(entity);
        }

        [HttpDelete]
        [Route(StaticValues.MasterCollectionDeleteByCollectionPath)]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MasterCollectionResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<int> Delete([FromRoute] string collectionName)
        {
            return await _masterCollectionService.Delete(collectionName);
        }

        [HttpDelete]
        [Route(StaticValues.MasterCollectionDeleteByCollectionKeyPath)]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MasterCollectionResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<int> Delete([FromRoute] string collectionName, [FromRoute] string key)
        {
            return await _masterCollectionService.Delete(collectionName, key);
        }

        [HttpDelete]
        [Route(StaticValues.MasterCollectionDeleteByIdPath)]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MasterCollectionResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<int> Delete([FromRoute] int collectionId)
        {
            return await _masterCollectionService.Delete(collectionId);
        }

        [HttpGet]
        [Route(StaticValues.MasterCollectionGetByCollectionPath)]
        [ProducesResponseType(typeof(PagingResponse<MasterCollectionResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MasterCollectionResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<PagingResponse<MasterCollectionResponse>> Get(string collectionName, int pageNo, int pageSize)
        {
            return await _masterCollectionService.Get(collectionName, pageNo, pageSize);
        }

        [HttpGet]
        [Route(StaticValues.MasterCollectionGetByIdPath)]
        [ProducesResponseType(typeof(MasterCollectionResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MasterCollectionResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<MasterCollectionResponse> Get([FromRoute] int collectionId)
        {
            return await _masterCollectionService.Get(collectionId);
        }

        [HttpGet]
        [Route(StaticValues.MasterCollectionPath)]
        [ProducesResponseType(typeof(PagingResponse<MasterCollectionResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MasterCollectionResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<PagingResponse<MasterCollectionResponse>> GetAll(int pageNo, int pageSize)
        {
            return await _masterCollectionService.GetAll(pageNo, pageSize);
        }

        [HttpGet]
        [Route(StaticValues.MasterCollectionSearchPath)]
        [ProducesResponseType(typeof(PagingResponse<MasterCollectionResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MasterCollectionResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<PagingResponse<MasterCollectionResponse>> Search([FromRoute] string searchTerm, [FromRoute] int pageNo, [FromRoute] int pageSize)
        {
            return await _masterCollectionService.Search(searchTerm, pageNo, pageSize);
        }

        [HttpPost]
        [Route(StaticValues.MasterCollectionPath)]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MasterCollectionResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<int> Update(MasterCollectionRequest entity)
        {
            return await _masterCollectionService.Update(entity);
        }
    }
}
