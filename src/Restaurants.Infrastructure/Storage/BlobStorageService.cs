using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using Microsoft.Extensions.Options;
using Restaurants.Domain.Interfaces;
using Restaurants.Infrastructure.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Infrastructure.Storage;

internal class BlobStorageService(IOptions<BlobStorageSettings> blobStorage) : IBlobStorageService
{
    private readonly BlobStorageSettings _blobStorageSettings= blobStorage.Value;
    public async Task<string> UploadToBlogAsync(Stream data, string FileName)
    {
        //Getting a blobserviceclient
        var blobServiceClient = new BlobServiceClient(_blobStorageSettings.ConnectionString);
        //Get container
        var containerClient = blobServiceClient.GetBlobContainerClient(_blobStorageSettings.LogosContainerName);

        //Get Blob Client
        var blobClient= containerClient.GetBlobClient(FileName);

        //Upload file
        await blobClient.UploadAsync(data);

        //Get Url
        var blobUrl =  blobClient.Uri.ToString();
        return blobUrl;
    }

    public string? GetBlobSasUrl(string blobUrl)
    {
        if(string.IsNullOrEmpty(blobUrl)) return null;

        //Get BlobSasBuilder
        var sasBuilder = new BlobSasBuilder()
        {
            BlobContainerName = _blobStorageSettings.LogosContainerName,
            Resource = "b",
            StartsOn = DateTimeOffset.Now,
            ExpiresOn = DateTimeOffset.Now.AddMinutes(30),
            BlobName = GetBlobNameFromUrl(blobUrl),
        };

        //Specifying the permission
        sasBuilder.SetPermissions(BlobAccountSasPermissions.Read);

        //Get SasToken
        var sasToken = sasBuilder
            .ToSasQueryParameters(new StorageSharedKeyCredential
            (_blobStorageSettings.AccountName,
            _blobStorageSettings.AccountKey)).ToString();

        //Now retreiving the sasurl
        return String.Concat(blobUrl,'?',sasToken);
    }

    private static string GetBlobNameFromUrl(string blobUrl)
    {
        var url= new Uri(blobUrl);
        return url.Segments.Last().ToString();
    }
}
