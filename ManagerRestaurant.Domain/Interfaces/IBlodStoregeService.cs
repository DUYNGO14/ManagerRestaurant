namespace ManagerRestaurant.Domain.Interfaces
{
    public interface IBlodStoregeService
    {
        //Task<string> UploadToBlodAsync(string fileName,Stream data);
        Task<string> UploadFileToBlodAsync(string fileName,Stream data);
        //public string? GetBlobSasUrl(string? blobUrl);
    }
}
