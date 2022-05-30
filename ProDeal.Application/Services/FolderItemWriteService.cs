using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using ProDeal.Application.Exceptions;
using ProDeal.Application.Extensions;
using ProDeal.Application.Interfaces;
using ProDeal.Infrastructure.Data;
using ProDealChallenge.Domain.Models;

namespace ProDeal.Application.Services
{
    public class FolderItemWriteService : IFolderItemWriteService
    {
        private readonly ProDealDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public FolderItemWriteService(ProDealDbContext context, IConfiguration configuration)
        {
            _dbContext = context;
            _configuration = configuration;
        }

        public async Task ProccessFile(IFormFile file)
        {
            var result = new Queue<string>(await file.ReadAsList());

            ValidateFile(file, result);

            var data = new List<FolderItem>();
            while (result.Count != 0)
                data.Add(CreateFolderItem(result.Dequeue()));

            ValidateData(data);

            await _dbContext.FolderItems.AddRangeAsync(data);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAll()
        {
           _dbContext.FolderItems.RemoveRange(_dbContext.FolderItems.ToList());
           await _dbContext.SaveChangesAsync();
        }

        private void ValidateData(List<FolderItem> data)
        {
            var hasExistingValueInDb = _dbContext.FolderItems.Any(x => data.Select(d => d.ExternalId).Contains(x.ExternalId));

            if (hasExistingValueInDb)
                throw new BusinessException("File has duplicated values and can't be uploaded.");
        }

        private void ValidateFile(IFormFile file, Queue<string> result)
        {
            var headers = result.Dequeue();
            
            if (_configuration.GetValue<string>("FileContentType") != file.ContentType)
                throw new BusinessException("Incorrect file extension.");

            if (_configuration.GetValue<string>("CSVFileHeaderOrder") != headers)
                throw new BusinessException("Incorrect order in CSV file.");
        }

        private FolderItem CreateFolderItem(string value)
        {
            var splitValues = value.Split(',');
            var externalId = int.Parse(splitValues[0].Trim());
            var parentId = splitValues[1].ToNullableInt();
            var itemName = splitValues[2];
            var priority = int.Parse(splitValues[3].Trim());

            return new FolderItem(externalId, parentId, itemName, priority);
        }
    }
}
