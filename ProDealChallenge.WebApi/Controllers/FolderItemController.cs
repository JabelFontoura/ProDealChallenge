using Microsoft.AspNetCore.Mvc;
using ProDeal.Application.Interfaces;

namespace ProDealChallenge.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FolderItemController : ControllerBase
    {
        private readonly IFolderItemWriteService _folderItemWriteService;
        private readonly IFolderItemReadService _folderItemReadService;
        public FolderItemController(IFolderItemWriteService folderItemWriteService, IFolderItemReadService folderItemReadService)
        {
            _folderItemWriteService = folderItemWriteService;
            _folderItemReadService = folderItemReadService;
        }

        [HttpGet]
        public async Task<ActionResult> Get([FromQuery] string? filterByName, int page = 1, int quantity = 10, bool sortByPriority = false)
        {
            return Ok(await _folderItemReadService.Get(filterByName, page, quantity, sortByPriority));
        }

        [HttpPost]
        public async Task<ActionResult> Upload(IFormFile file)
        {
            await _folderItemWriteService.ProccessFile(file);
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> DeteleAll()
        {
            await _folderItemWriteService.DeleteAll();
            return Ok();
        }
    } 
}
