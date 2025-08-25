namespace Apibackend.Events
{
    /// <summary>
    /// class representing an event when a product is created
    /// </summary>
    public class ProductCreatedEvent
    {
        /// <summary>
        /// variables to hold product id and name
        /// </summary>
        public int ProductId { get; }
        public string Name { get; }

        /// <summary>
        /// constructor to initialize the event with product id and name
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="name"></param>
        public ProductCreatedEvent(int productId, string name)
        {
            ProductId = productId;
            Name = name;
        }
    }
}
