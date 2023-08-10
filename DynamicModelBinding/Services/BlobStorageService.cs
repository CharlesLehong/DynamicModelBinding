using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using DynamicModelBinding.Model;
using Microsoft.Extensions.Options;

namespace DynamicModelBinding.Services
{
    public class BlobStorageService
    {
        private readonly BlobContainerClient blobContainerClient;

        public BlobStorageService(IOptions<BlobStorageOptions> options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            var storageOptions = options.Value;
            var blobServiceClient = new BlobServiceClient(storageOptions.ConnectionString);

            var containerName = storageOptions.ContainerName;
            this.blobContainerClient = blobServiceClient.GetBlobContainers()
                .FirstOrDefault(c => c.Name == containerName) == null
                ? blobServiceClient.CreateBlobContainer(containerName)
                : blobServiceClient.GetBlobContainerClient(containerName);
        }

        public async Task UploadBlobAsync(Stream stream, string fileName)
        {
            _ = await this.blobContainerClient.DeleteBlobIfExistsAsync(fileName, DeleteSnapshotsOption.IncludeSnapshots);
            _ = await this.blobContainerClient.UploadBlobAsync(fileName, await BinaryData.FromStreamAsync(stream));
        }


        public async Task<byte[]> DownloadBlobAsyc(string fileName)
        {
            var blobClient = this.blobContainerClient.GetBlobClient(fileName);
            var response = await blobClient.DownloadAsync();
            await using var memStream = new MemoryStream();
            await response.Value.Content.CopyToAsync(memStream);

            return memStream.ToArray();
        }

        public string GetBlobUri(string blobName)
        {
            return this.blobContainerClient.GetBlobClient(blobName).Uri.ToString();
        }
    }
}
