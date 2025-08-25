namespace Apibackend.Events
{
    /// <summary>
    /// class representing an event when a product is deleted
    /// </summary>
    public class ProductDeletedEvent
    {
        /// <summary>
        /// property representing the ID of the deleted product
        /// </summary>
        public int ProductId { get; }

        /// <summary>
        /// constructor for the ProductDeletedEvent class
        /// </summary>
        /// <param name="productId"></param>
        public ProductDeletedEvent(int productId)
        {
            ProductId = productId;
        }
    }
}
