namespace ConcurrencyTestWebApi.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public int OrderQuantity { get; set; }

        public Order()
        {

        }

        public Order(Guid productId, int quantity)
        {
            ProductId = productId;
            OrderQuantity = quantity;
        }
    }
}
