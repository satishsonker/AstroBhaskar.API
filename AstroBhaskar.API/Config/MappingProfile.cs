using AstroBhaskar.API.Dto.BaseDto;
using AstroBhaskar.API.Dto.Request;
using AstroBhaskar.API.Dto.Response;
using AstroBhaskar.API.Models;
using AutoMapper;

namespace AstroBhaskar.API.Config
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserRequest, AstroUser>();
            CreateMap<UserPermissionRequest, UserPermission>();
            CreateMap<AstroUser, UserResponse>();
            CreateMap<UserPermissionRequest, UserPermission>();
            CreateMap<BasePagination<AstroUser>, PagingResponse<UserResponse>>();
            CreateMap<PagingResponse<UserResponse>, BasePagination<AstroUser>>();
            CreateMap<BasePagination<UserPermission>, PagingResponse<UserPermissionResponse>>();
            CreateMap<PagingResponse<UserPermissionResponse>, BasePagination<UserPermission>>();
            CreateMap<MasterCollectionRequest, MasterCollection>();
            CreateMap<MasterCollection, MasterCollectionResponse>();
            CreateMap<BasePagination<MasterCollection>, PagingResponse<MasterCollectionResponse>>();
        }
    }
}
