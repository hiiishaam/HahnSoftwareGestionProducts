namespace Apibackend.Events
{
    /// <summary>
    /// class representing a product update event
    /// </summary>
    public class ProductUpdatedEvent
    {
        /// <summary>
        /// properties of the product update event
        /// </summary>
        public int ProductId { get; }
        /// <summary>
        /// name of the product after update
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// price of the product after update
        /// </summary>
        public decimal Price { get; }

        /// <summary>
        /// constructor for the product update event
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="name"></param>
        /// <param name="price"></param>
        public ProductUpdatedEvent(int productId, string name, decimal price)
        {
            ProductId = productId;
            Name = name;
            Price = price;
        }
    }
}
