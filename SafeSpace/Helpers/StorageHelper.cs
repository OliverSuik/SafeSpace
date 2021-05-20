using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using SafeSpace.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SafeSpace.Helpers
{
    public class StorageHelper
    {
        //public static async Task<bool> UploadFileToStorage(Stream fileStream, string fileName,
        //                                                    AzureStorageConfig _storageConfig)
        //{
        //    // Create a URI to the blob
        //    Uri blobUri = new Uri("https://" +
        //                          _storageConfig.AccountName +
        //                          ".blob.core.windows.net/" +
        //                          _storageConfig.ImageContainer +
        //                          "/" + fileName);

        //    // Create StorageSharedKeyCredentials object by reading
        //    // the values from the configuration (appsettings.json)
        //    StorageSharedKeyCredential storageCredentials =
        //        new StorageSharedKeyCredential(_storageConfig.AccountName, _storageConfig.AccountKey);

        //    // Create the blob client.
        //    BlobClient blobClient = new BlobClient(blobUri, storageCredentials);

        //    // Upload the file
        //    await blobClient.UploadAsync(fileStream);

        //    return await Task.FromResult(true);
        //}
    }
}
