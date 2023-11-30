using System.ComponentModel.DataAnnotations;

namespace ConcurrencyTestWebApi.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int StockQuantity { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public Product()
        {

        }

        public Product(string name, int stockQuantity)
        {
            Id = Guid.NewGuid();
            Name = name;
            StockQuantity = stockQuantity;
        }
    }
}
