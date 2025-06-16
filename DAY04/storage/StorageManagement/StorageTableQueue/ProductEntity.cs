
using Azure;
using Azure.Data.Tables;

namespace StorageTableQueue
{
    public class ProductEntity : ITableEntity
    {
        // Proprietà di default

        public string PartitionKey { get; set; } = string.Empty;
        public string RowKey { get; set; } = string.Empty;
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }

        // Proprietà Custom

        public string Name { get; set; } = string.Empty;
        public double Price { get; set; } = 0.0;
        public int Stock { get; set; } = 0;
        public Boolean Enabled { get; set; } = true;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;


    }
}
