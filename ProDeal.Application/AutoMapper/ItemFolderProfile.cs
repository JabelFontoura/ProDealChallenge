using AutoMapper;
using ProDeal.Application.Dtos;
using ProDeal.Application.Dtos.Pagination;
using ProDealChallenge.Domain.Models;

namespace ProDeal.Application.AutoMapper
{
    public class ItemFolderProfile : Profile
    {
        public ItemFolderProfile()
        {
            CreateMap<PagedResult<FolderItem>, PagedResult<FolderItemDto>>();
            CreateMap<FolderItem, FolderItemDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ExternalId))
                .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.ParentExternalId))
                .ForMember(dest => dest.ItemName, opt => opt.MapFrom(src => src.ItemName))
                .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority))
                .ForMember(dest => dest.PathName, opt => opt.MapFrom(src => new PathNameResolver().Resolve(src)));
        }
    }
}
