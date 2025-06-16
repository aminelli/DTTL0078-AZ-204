using System;
using Azure;
using Azure.Identity;
using Azure.Storage;
using Azure.Storage.Blobs;


// dotnet add package Azure.Identity
// dotnet add package Azure.Data.Tables
// dotnet add package Azure.Storage.Queues
// dotnet add package Azure.Storage.Blobs


namespace StorageTableQueue
{

    class Program
    {
        const string AZURE_CLIENT_TENANT_ID = "";
        const string AZURE_CLIENT_ID = "";
        const string AZURE_CLIENT_SECRET = "";

        const string ACCOUNT_KEY_SAS = "";
        const string ACCOUNT_SAS_URL = "";

        const string ACCOUNT_NAME = "";
        const string ACCOUNT_KEY = "";

        const string CONN_STRING_MODEL = "DefaultEndpointsProtocol=https;AccountName={yourstorageaccount};AccountKey={yourkey};EndpointSuffix=core.windows.net";


        public static async Task Main(string[] args)
        {
            var connString = CONN_STRING_MODEL
                .Replace("{yourstorageaccount}", ACCOUNT_NAME)
                .Replace("{yourkey}", ACCOUNT_KEY);

            var accountExample = new AzureStorageExample(connString);

            try
            {
                //await accountExample.QueueTestSend();
                //await accountExample.QueueTestReceived();

                await accountExample.TableTestInsert();
                await accountExample.TableTestSelect();
            }
            catch (RequestFailedException ex)
            {
                Console.WriteLine(
                    "HTTP error code {0}: {1}",
                    ex.Status,
                    ex.ErrorCode
                    );
            }

            //Thread.Sleep(5000);

        }
    
    }
        
}