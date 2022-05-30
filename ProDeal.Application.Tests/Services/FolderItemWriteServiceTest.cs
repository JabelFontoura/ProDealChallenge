using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProDeal.Application.Exceptions;
using ProDeal.Application.Services;
using ProDeal.Infrastructure.Data;
using ProDealChallenge.Domain.Models;

namespace ProDeal.Application.Tests.Services
{
    [TestFixture]
    public class FolderItemWriteServiceTest
    {
        private readonly ProDealDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public FolderItemWriteServiceTest()
        {
            var dbContextOptionsBuilder = new DbContextOptionsBuilder();
            dbContextOptionsBuilder.UseInMemoryDatabase("Testing");
            _dbContext = new ProDealDbContext(dbContextOptionsBuilder.Options);

            _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "FileContentType", "text/csv" },
                    { "CSVFileHeaderOrder", "id,parent_id,item_name,priority" },
                })
                .Build();
        }

        [Test]
        public async Task GivenThatDataInFileIsCorrectShouldSaveSuccesfully()
        {
            // Arrange
            _dbContext.FolderItems.RemoveRange(_dbContext.FolderItems.ToList());
            await _dbContext.SaveChangesAsync();

            var service = new FolderItemWriteService(_dbContext, _configuration);
            var fileContent =
                "id,parent_id,item_name,priority\n" +
                "1,nil,heading 1,3\n" +
                "2,nil,heading 2,1\n" +
                "3,1,folder 1 1,4\n" +
                "4,1,folder 1 2,2\n" +
                "5,2,folder 2 1,2\n" +
                "6,2,folder 2 2,3\n" +
                "7,2,folder 2 3,5\n" +
                "8,6,subfolder 2 2 1,2\n" +
                "9,7,subfolder 2 3 1,1\n" +
                "10,7,subfolder 2 3 2,5";
            var formFile = SetupFormFile(fileContent, "text/csv");

            // Act
            await service.ProccessFile(formFile);

            // Assert
            Assert.That(_dbContext.FolderItems.Count() == 10);

        }

        [Test]
        public void GivenThatDataInFileHasWrongHeaderShouldThrowsBusinessException()
        {
            // Arrange
            var service = new FolderItemWriteService(_dbContext, _configuration);
            var fileContent =
                "parent_id,id,item_name,priority\n" +
                "1,nil,heading 1,3\n" +
                "2,nil,heading 2,1\n";
            var formFile = SetupFormFile(fileContent, "text/csv");

            // Act
            var result = Assert.ThrowsAsync<BusinessException>(() => service.ProccessFile(formFile));

            // Assert
            Assert.That(result.Message == "Incorrect order in CSV file.");

        }

        [Test]
        public async Task GivenThatDeleteWasExecutedShouldExcludeAllData()
        {
            // Arrange
            _dbContext.FolderItems.RemoveRange(_dbContext.FolderItems.ToList());
            await _dbContext.SaveChangesAsync();

            _dbContext.FolderItems.Add(new FolderItem(1, null, "heading 1", 3));
            await _dbContext.SaveChangesAsync();

            var service = new FolderItemWriteService(_dbContext, _configuration);

            // Act
            await service.DeleteAll();

            // Assert
            Assert.That(_dbContext.FolderItems.Count() == 0);
        }

        [Test]
        public void GivenThatDataInFileHasrongContentTypeShouldThrowsBusinessException()
        {
            // Arrange
            var service = new FolderItemWriteService(_dbContext, _configuration);
            var fileContent =
                "parent_id,id,item_name,priority\n" +
                "1,nil,heading 1,3\n" +
                "2,nil,heading 2,1\n";
            var formFile = SetupFormFile(fileContent, "text/cs");

            // Act
            var result = Assert.ThrowsAsync<BusinessException>(() => service.ProccessFile(formFile));

            // Assert
            Assert.That(result.Message == "Incorrect file extension.");

        }

        private FormFile SetupFormFile(string content, string contentType)
        {
            byte[] bytes = null;
            using (var ms = new MemoryStream())
            {
                using (var tw = new StreamWriter(ms))
                {
                    tw.Write(content);
                    tw.Flush();
                    ms.Position = 0;
                    bytes = ms.ToArray();
                }
            }

            var stream = new MemoryStream(bytes);
            return new FormFile(stream, 0, stream.Length, "streamFile", "testFile.csv")
            {
                Headers = new HeaderDictionary(),
                ContentType = contentType
            };
        }
    }
}
