using Microsoft.AspNetCore.Http;

namespace ProDeal.Application.Interfaces
{
    public interface IFolderItemWriteService
    {
        Task ProccessFile(IFormFile file);
        Task DeleteAll();
    }
}
