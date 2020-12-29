using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmService_Data
{
    public class BlobHelper
    {

        static CloudBlobContainer container;
       // CloudBlockBlob blob = null;
        public BlobHelper()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("DataConnectionString"));

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            container = blobClient.GetContainerReference("blob");
            container.CreateIfNotExists();

            BlobContainerPermissions permissions = container.GetPermissions();
            permissions.PublicAccess = BlobContainerPublicAccessType.Container;
            container.SetPermissions(permissions);
        }


        public CloudBlockBlob UploadToBlob(String imeBloba, String poruka)
        {
            CloudBlockBlob blob = null;

            using (var stream = new MemoryStream(Encoding.Default.GetBytes(poruka)))
            {
                try
                {
                    blob = container.GetBlockBlobReference(imeBloba);
                    blob.UploadFromStream(stream);
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            return blob;
        }


        public String DownloadFromBlob(String blobIme)
        {
            String poruka = "";

            using (var stream = new MemoryStream())
            {
                try
                {
                    CloudBlockBlob blob = container.GetBlockBlobReference(blobIme);
                    blob.DownloadToStream(stream);
                    poruka = Encoding.Default.GetString(stream.ToArray());
                }
                catch (Exception e)
                {
                    //throw e;
                }
            }
            return poruka;
        }






    }
}
