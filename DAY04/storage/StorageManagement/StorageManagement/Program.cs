using System;
using Azure;
using Azure.Identity;
using Azure.Storage;
using Azure.Storage.Blobs;


namespace StorageManagement
{

    class Program {
        const string AZURE_CLIENT_TENANT_ID = "";
        const string AZURE_CLIENT_ID = "";
        const string AZURE_CLIENT_SECRET = "";

        const string ACCOUNT_KEY_SAS = "";


        static void Main(string[] args) {
            string storageAccountName = "sacorsodttl0078sample001";

            // Scenario 1
            /*
            var client = GetBlobServiceClient(storageAccountName);
            CreateRootContainer(client, "test");
            */

            // Scenario 2
            var client = GetBlobServiceClientSecret(storageAccountName);
            CreateRootContainer(client, "testsecret");

        }

        // Creazione Client per Storage Account
        private static BlobServiceClient GetBlobServiceClient(string storageAccountName) {

            BlobServiceClient client = new(
                    new Uri($"https://{storageAccountName}.blob.core.windows.net"),
                    new DefaultAzureCredential()
                );

            return client;
        }


        // Creazione Client per Storage Account
        private static BlobServiceClient GetBlobServiceClientSecret(string storageAccountName)
        {
            var credential= new ClientSecretCredential(
                AZURE_CLIENT_TENANT_ID,
                AZURE_CLIENT_ID,
                AZURE_CLIENT_SECRET
                );


            BlobServiceClient client = new(
                    new Uri($"https://{storageAccountName}.blob.core.windows.net"),
                    credential
                );

            return client;
        }

        private static BlobServiceClient GetBlobServiceClientSAS(string storageAccountName)
        {
             

            var credential = new StorageSharedKeyCredential(
                    storageAccountName,
                    ACCOUNT_KEY_SAS
                );

            BlobServiceClient client = new(
                    new Uri($"https://{storageAccountName}.blob.core.windows.net"),
                    credential
                );

            return client;
        }



        private static void CreateRootContainer(BlobServiceClient client, string containerName)
        {
            try
            {
                BlobContainerClient container = client.CreateBlobContainer(containerName);

                Console.WriteLine(
                    container.Exists() ?
                    "Container creato con successo"
                    :
                    "Container non creato"
                    );
            }
            catch (RequestFailedException ex)
            {
                Console.WriteLine(
                    "HTTP error code {0}: {1}",
                    ex.Status,
                    ex.ErrorCode
                    );
            }

        }




        


    }

}