using ProDeal.Application.Dtos;
using ProDeal.Application.Dtos.Pagination;

namespace ProDeal.Application.Interfaces
{
    public interface IFolderItemReadService
    {
        Task<PagedResult<FolderItemDto>> Get(string? filterByName, int page, int quantity, bool sortByPriority);
    }
}
