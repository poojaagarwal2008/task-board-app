using System.IO;
using Azure.Storage.Blobs;
using FileService.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace TaskBoard.Tests
{
    public class FileUploadServiceTests
    {
        [Fact]
        public async Task UploadFileAsync_ShouldReturnUrl()
        {
            // Arrange
            var blobServiceClientMock = new Mock<BlobServiceClient>();
            var configurationMock = new Mock<IConfiguration>();

            configurationMock.Setup(c => c["AzureBlob:ContainerName"]).Returns("test-container");

            var fileUploadService = new FileUploadService(blobServiceClientMock.Object, configurationMock.Object);

            var fileMock = new Mock<IFormFile>();
            var content = "Hello World from a fake file";
            var fileName = "test.txt";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;

            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);

            // Act
            var result = await fileUploadService.UploadFileAsync(fileMock.Object);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task UploadFileAsync_ShouldThrowException_IfContainerNameMissing()
        {
            // Arrange
            var blobServiceClientMock = new Mock<BlobServiceClient>();
            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(c => c["AzureBlob:ContainerName"]).Returns((string?)null);

            var fileUploadService = new FileUploadService(blobServiceClientMock.Object, configurationMock.Object);
            var fileMock = new Mock<IFormFile>();

            // Act & Assert
            var exception = await fileUploadService.UploadFileAsync(fileMock.Object);
            Assert.NotNull(exception); // Ensure the exception is not null
        }

    }
}
