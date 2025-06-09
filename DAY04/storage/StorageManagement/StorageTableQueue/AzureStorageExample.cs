using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;

namespace StorageTableQueue
{
    public class AzureStorageExample
    {

        public string ConnectionString { get; private set; }

        public AzureStorageExample(string connectionString)
        {
            this.ConnectionString = connectionString;
        }


        // AZURE Storace Account: QUEUES
        public async Task QueueTestSend()
        {

            const string queueName = "orders_queue";

            // Inizializza il client
            var queueClient = new QueueClient(
                ConnectionString,
                queueName
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
            const string queueName = "orders_queue";


            // Inizializza il client
            var queueClient = new QueueClient(
                ConnectionString,
                queueName
                );

            try
            {
                await queueClient.CreateAsync();

                QueueMessage[] messages = await queueClient.ReceiveMessagesAsync(maxMessages: 10);

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


    }





}
