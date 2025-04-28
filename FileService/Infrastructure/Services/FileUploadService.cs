using Azure.Storage.Blobs;
using FileService.Application.Interfaces;

namespace FileService.Infrastructure.Services
{
    public class FileUploadService : IFileUploadService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly IConfiguration _configuration;

        public FileUploadService(BlobServiceClient blobServiceClient, IConfiguration configuration)
        {
            _blobServiceClient = blobServiceClient;
            _configuration = configuration;
        }

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            if (file == null) throw new ArgumentNullException(nameof(file), "Uploaded file cannot be null.");

            var containerName = _configuration["AzureBlob:ContainerName"];
            if (string.IsNullOrEmpty(containerName))
                throw new ArgumentNullException(nameof(containerName), "Container name is not configured.");

            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(file.FileName);

            await using var stream = file.OpenReadStream();
            await blobClient.UploadAsync(stream, true);

            return blobClient.Uri.AbsoluteUri;
        }


    }
}
