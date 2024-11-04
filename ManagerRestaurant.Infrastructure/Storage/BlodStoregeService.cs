using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using Microsoft.Extensions.Options;
using ManagerRestaurant.Domain.Interfaces;

namespace ManagerRestaurant.Infrastructure.Storage
{
    public class BlodStoregeService(/*IOptions<BlodStorageSettings> options*/) : IBlodStoregeService
    {
        //private readonly BlodStorageSettings _storageSettings= options.Value;
        public async Task<string> UploadFileToBlodAsync(string fileName, Stream data)
        {
            
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "D:\\Restaurants\\Restaurant.UI\\wwwroot\\Upload");
            // Tạo thư mục nếu chưa tồn tại
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await data.CopyToAsync(stream);
            }
            return fileName;
        }
        //public async Task<string> UploadToBlodAsync(string fileName, Stream data)
        //{
        //    var blodServiceClient = new BlobServiceClient(_storageSettings.ConnectionString);
        //    var containerClient = blodServiceClient.GetBlobContainerClient(_storageSettings.LogosContainerName);

        //    var blodClient = containerClient.GetBlobClient(fileName);
        //    await blodClient.UploadAsync(data);
        //    var bloUrl = blodClient.Uri.ToString();
        //    return bloUrl;
        //}

        //public string? GetBlobSasUrl(string? blobUrl)
        //{
        //    if (blobUrl == null) return null;
        //    var sasBuilder = new BlobSasBuilder() 
        //    { 
        //        BlobContainerName = _storageSettings.LogosContainerName,
        //        Resource = "b",
        //        StartsOn = DateTimeOffset.UtcNow,
        //        ExpiresOn = DateTimeOffset.UtcNow.AddMinutes(30),
        //        BlobName = GetBlobFromUrl(blobUrl)

        //    };

        //    sasBuilder.SetPermissions(BlobSasPermissions.Read);
        //    var blodServiceClient = new BlobServiceClient(_storageSettings.ConnectionString);
        //    var sasToken = sasBuilder.ToSasQueryParameters
        //        (new Azure.Storage.StorageSharedKeyCredential
        //        (blodServiceClient.AccountName, _storageSettings.AccountKey)).ToString();
        //    return $"{blobUrl}?{sasToken}";
        //}

        //private string GetBlobFromUrl(string blobUrl)
        //{
        //    var uri = new Uri(blobUrl);
        //    return uri.Segments.Last();
        //}
    }
}





