namespace Restaurants.Domain.Interfaces;

public interface IBlobStorageService
{
    string? GetBlobSasUrl(string blobUrl);
    Task<string> UploadToBlogAsync(Stream data, string FileName);
}
