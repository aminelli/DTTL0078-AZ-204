using Azure.Data.Tables;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;

namespace StorageTableQueue
{
    public class AzureStorageExample
    {

        public string ConnectionString { get; private set; }
        const string QUEUE_NAME = "ordersqueue";
        const string TABLE_NAME = "products";


        public AzureStorageExample(string connectionString)
        {
            this.ConnectionString = connectionString;
        }


        // AZURE Storace Account: QUEUES
        public async Task QueueTestSend()
        {

                // Inizializza il client
                var queueClient = new QueueClient(
                    ConnectionString,
                    QUEUE_NAME
                    );

                await queueClient.CreateIfNotExistsAsync();

                // Test invio messaggio 1
                await queueClient.SendMessageAsync("Ordine #001 - Spaghetti Carbonara");

                // Messaggio con struttura JSON
                var orderJson = """
                {
                    "orderId": "002",
                    "product": "Cacio e pepe",
                    "Quantity": 2,
                    "UnitPrice": 15.00
                }
                """;

                // Test invio messaggio 2
                await queueClient.SendMessageAsync(orderJson);

                for (int count = 2; count < 100; count++)
                {
                    orderJson = """
                {
                    "orderId": {orderId},
                    "product": "Cacio e pepe",
                    "Quantity": 1,
                    "UnitPrice": 15.00
                }
                """.Replace("{orderId}", count.ToString().PadLeft(3, '0'));

                    // Messaggio consegnato ma con visibilità ritardata di 10 secondi sullo storage per la lettura
                    await queueClient.SendMessageAsync(
                        orderJson,
                        visibilityTimeout: TimeSpan.FromSeconds(10)

                     );

                }
        }


        // AZURE Storace Account: QUEUES
        public async Task QueueTestReceived()
        {
            

            // Inizializza il client
            var queueClient = new QueueClient(
                ConnectionString,
                QUEUE_NAME
                );

            try
            {
                await queueClient.CreateAsync();

                QueueMessage[] messages = await queueClient.ReceiveMessagesAsync(maxMessages: 30);
                //QueueMessage[] messages = await queueClient.ReceiveMessagesAsync(maxMessages: 10);

                if (messages != null && messages.Length > 0)
                {
                    foreach (var msg in messages)
                    {
                        Console.WriteLine($"ID: {msg.MessageId}");

                        Console.WriteLine($"Content: {msg.Body.ToString()}");
                        Console.WriteLine($"Content: {msg.InsertedOn}");
                        Console.WriteLine($"Content: {msg.ExpiresOn}");

                        // ELIMINAZIONE MESSAGGIO
                        await queueClient.DeleteMessageAsync(msg.MessageId, msg.PopReceipt);

                    }


                }
            }
            catch (Exception)
            {

                throw;
            }


        }


        public async Task TableTestInsert() {

            var tableclient = new TableClient(ConnectionString, TABLE_NAME);

            await tableclient.CreateIfNotExistsAsync();

            var p1 = new ProductEntity { 
                PartitionKey = "Elettronica",
                RowKey = "ART00001",
                Name = "Mouse PK",
                Price = 12.00,
                Stock = 10,
                Enabled = true,
                CreatedDate = DateTime.UtcNow
            };

            var p2 = new ProductEntity
            {
                PartitionKey = "Casa",
                RowKey = "ART00002",
                Name = "Scottex",
                Price = 2,
                Stock = 15,
                Enabled = true,
                CreatedDate = DateTime.UtcNow
            };

            var p3 = new ProductEntity
            {
                PartitionKey = "Auto",
                RowKey = "ART00003",
                Name = "Profumatore pino silvestre",
                Price = 1.00,
                Stock = 3,
                Enabled = true,
                CreatedDate = DateTime.UtcNow
            };


            await tableclient.AddEntityAsync(p1);
            await tableclient.AddEntityAsync(p2);
            await tableclient.AddEntityAsync(p3);




        }


        public async Task TableTestSelect()
        {

            var tableclient = new TableClient(ConnectionString, TABLE_NAME);

            // Lettura singolo prodotto conoscendo le chiavi

            var response = await tableclient.GetEntityAsync<ProductEntity>("Elettronica", "ART00001");
            PrintProduct(response.Value);

            //var responseQuery = tableclient.Query<ProductEntity>(x => x.PartitionKey.Equals("Auto"));
            //PrintProduct(response.Value);

            await foreach (var product in tableclient.QueryAsync<ProductEntity>(x => x.PartitionKey.Equals("Auto"))){
                PrintProduct(product);
            } ;


            await foreach (var product in tableclient.QueryAsync<ProductEntity>())
            {
                PrintProduct(product);
            }
            ;



        }


        public void PrintProduct(ProductEntity product) {
            Console.WriteLine($"==========================");
            Console.WriteLine($"{product.PartitionKey}");
            Console.WriteLine($"{product.RowKey}");
            Console.WriteLine($"{product.Name}");

        }



    }
}
