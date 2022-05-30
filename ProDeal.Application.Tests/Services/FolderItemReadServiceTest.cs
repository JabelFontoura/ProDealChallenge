using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProDeal.Application.AutoMapper;
using ProDeal.Application.Services;
using ProDeal.Infrastructure.Data;
using ProDealChallenge.Domain.Models;

namespace ProDeal.Application.Tests.Services
{
    [TestFixture]
    public class FolderItemReadServiceTest
    {
        private readonly ProDealDbContext _dbContext;
        private readonly IMapper _mapper;

        public FolderItemReadServiceTest()
        {
            var dbContextOptionsBuilder = new DbContextOptionsBuilder();
            dbContextOptionsBuilder.UseInMemoryDatabase("Testing");
            _dbContext = new ProDealDbContext(dbContextOptionsBuilder.Options);

            _dbContext.FolderItems.AddRange(
                new FolderItem(1, null, "heading 1", 3),
                new FolderItem(2, null, "heading 2", 1),
                new FolderItem(3, 1, "folder 1 1", 4),
                new FolderItem(4, 1, "folder 1 2", 2),
                new FolderItem(5, 2, "folder 2 1", 2),
                new FolderItem(6, 2, "folder 2 2", 3),
                new FolderItem(7, 2, "folder 2 3", 5),
                new FolderItem(8, 6, "subfolder 2 2 1", 2),
                new FolderItem(9, 7, "subfolder 2 3 1", 1),
                new FolderItem(10, 7, "subfolder 2 3 2", 5)
                );

            _dbContext.SaveChanges();

            _mapper = new Mapper(AutoMapperConfiguration.RegisterMappings());
        }

        [Test]
        public async Task GivenThatGetIsSendWithoutFiltersShouldReturnEverything()
        {
            // Arrange
            var service = new FolderItemReadService(_dbContext, _mapper);

            // Act
            var result = await service.Get(null, 1, 10, false);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.PageSize, Is.EqualTo(10));
            Assert.That(result.PageCount, Is.EqualTo(1));
            Assert.That(result.Results.Count, Is.EqualTo(10));
            Assert.That(result.Results[0].Id, Is.EqualTo(1));
            Assert.That(result.Results[0].ItemName, Is.EqualTo("heading 1"));
            Assert.That(result.Results[0].Id, Is.EqualTo(1));
            Assert.That(result.Results[0].Priority, Is.EqualTo(3));
            Assert.IsNull(result.Results[0].ParentId);
        }

        [Test]
        public async Task GivenThatGetIsSendWithItemNameFiltersShouldReturnOnlyOneMatch()
        {
            // Arrange
            var service = new FolderItemReadService(_dbContext, _mapper);

            // Act
            var result = await service.Get("heading 1", 1, 10, false);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.PageSize, Is.EqualTo(10));
            Assert.That(result.PageCount, Is.EqualTo(1));
            Assert.That(result.Results.Count, Is.EqualTo(1));
            Assert.That(result.Results[0].Id, Is.EqualTo(1));
            Assert.That(result.Results[0].ItemName, Is.EqualTo("heading 1"));
            Assert.That(result.Results[0].Id, Is.EqualTo(1));
            Assert.That(result.Results[0].Priority, Is.EqualTo(3));
            Assert.IsNull(result.Results[0].ParentId);
        }

        [Test]
        public async Task GivenThatGetIsSendWithSortByPriorityShouldReturnEverythingInPriorityOrder()
        {
            // Arrange
            var service = new FolderItemReadService(_dbContext, _mapper);

            // Act
            var result = await service.Get(null, 1, 10, true);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Results[0].Id, Is.EqualTo(2));
            Assert.That(result.Results[0].ItemName, Is.EqualTo("heading 2"));
            Assert.That(result.Results[0].Priority, Is.EqualTo(1));
            Assert.IsNull(result.Results[0].ParentId);
        }

        [Test]
        public async Task GivenThatGetIsSendWithQuantity5ShouldReturn2Pages()
        {
            // Arrange
            var service = new FolderItemReadService(_dbContext, _mapper);

            // Act
            var result = await service.Get(null, 1, 5, false);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.PageSize, Is.EqualTo(5));
            Assert.That(result.PageCount, Is.EqualTo(2));
            Assert.That(result.RowCount, Is.EqualTo(10));
            Assert.That(result.FirstRowOnPage, Is.EqualTo(1));
            Assert.That(result.LastRowOnPage, Is.EqualTo(5));
            Assert.That(result.Results.Count, Is.EqualTo(5));
            Assert.That(result.Results[0].Id, Is.EqualTo(1));
        }

        [Test]
        public async Task GivenThatGetIsSendWithQuantity5AndPage1ShouldReturnSecondPage()
        {
            // Arrange
            var service = new FolderItemReadService(_dbContext, _mapper);

            // Act
            var result = await service.Get(null, 2, 5, false);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.PageSize, Is.EqualTo(5));
            Assert.That(result.PageCount, Is.EqualTo(2));
            Assert.That(result.RowCount, Is.EqualTo(10));
            Assert.That(result.FirstRowOnPage, Is.EqualTo(6));
            Assert.That(result.LastRowOnPage, Is.EqualTo(10));
            Assert.That(result.Results.Count, Is.EqualTo(5));
            Assert.That(result.Results[0].Id, Is.EqualTo(6));
        }
    }
}
