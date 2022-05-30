using Microsoft.AspNetCore.Http;
using Moq;
using ProDeal.Application.Dtos;
using ProDeal.Application.Dtos.Pagination;
using ProDeal.Application.Interfaces;
using ProDealChallenge.WebApi.Controllers;

namespace ProDealChallenge.WebApi.Tests.Controllers
{
    [TestFixture]
    public class FolderItemControllerTest
    {
        [Test]
        public async Task GivenThatGetRequestIsCorrectShouldReturnDataSuccessfully()
        {
            //  Arrange
            var mockIFolderItemWriteService = new Mock<IFolderItemWriteService>();
            var mockIFolderItemReadService = new Mock<IFolderItemReadService>();
            mockIFolderItemReadService
                .Setup(x => x.Get(It.IsAny<string?>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<bool>()))
                .Returns(Task.FromResult(new PagedResult<FolderItemDto>() { Results = new List<FolderItemDto>() { new FolderItemDto() { Id = 1, ItemName = "Test" } }, RowCount = 1, PageSize = 1 }));
            var controller = new FolderItemController(mockIFolderItemWriteService.Object, mockIFolderItemReadService.Object);

            // Act
            var result = await controller.Get(null);

            // Assert
            Assert.IsNotNull(result);
            var value = (PagedResult <FolderItemDto>) result.GetType().GetProperty("Value").GetValue(result);
            Assert.IsNotNull(value);
            Assert.That(value.RowCount, Is.EqualTo(1));
            Assert.That(value.PageSize, Is.EqualTo(1));
            Assert.That(value.Results.Count, Is.EqualTo(1));
            Assert.That(value.Results[0].Id, Is.EqualTo(1));
            Assert.That(value.Results[0].ItemName, Is.EqualTo("Test"));
        }

        [Test]
        public async Task GivenThatUploadRequestIsCorrectShouldSaveDataSuccessfully()
        {
            //  Arrange
            var mockIFolderItemWriteService = new Mock<IFolderItemWriteService>();
            var mockIFolderItemReadService = new Mock<IFolderItemReadService>();
            mockIFolderItemWriteService.Setup(x => x.ProccessFile(It.IsAny<IFormFile>()));
            var controller = new FolderItemController(mockIFolderItemWriteService.Object, mockIFolderItemReadService.Object);

            // Act
            var result = await controller.Upload(null);

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GivenThatDeleteRequestIsCorrectShouldSaveDataSuccessfully()
        {
            //  Arrange
            var mockIFolderItemWriteService = new Mock<IFolderItemWriteService>();
            var mockIFolderItemReadService = new Mock<IFolderItemReadService>();
            mockIFolderItemWriteService.Setup(x => x.DeleteAll());
            var controller = new FolderItemController(mockIFolderItemWriteService.Object, mockIFolderItemReadService.Object);

            // Act
            var result = await controller.DeteleAll();

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
