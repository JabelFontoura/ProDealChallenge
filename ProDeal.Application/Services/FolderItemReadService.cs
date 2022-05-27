using AutoMapper;
using ProDeal.Application.Dtos;
using ProDeal.Application.Dtos.Pagination;
using ProDeal.Application.Extensions;
using ProDeal.Application.Interfaces;
using ProDeal.Infrastructure.Data;

namespace ProDeal.Application.Services
{
    public class FolderItemReadService : IFolderItemReadService
    {
        private readonly ProDealDbContext _context;
        private readonly IMapper _mapper;

        public FolderItemReadService(ProDealDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PagedResult<FolderItemDto>> Get(string? filterByName, int page, int quantity, bool sortByPriority)
        {
            var filteredResult = _context.FolderItems
                 .Where(x => string.IsNullOrEmpty(filterByName) || x.ItemName == filterByName);

            if (sortByPriority)
            {
                filteredResult = filteredResult
                    .OrderBy(x => x.Priority)
                    .AsQueryable();
            }

            var result = await filteredResult.GetPaged(page, quantity);

            return _mapper.Map<PagedResult<FolderItemDto>>(result);
        }
    }
}
