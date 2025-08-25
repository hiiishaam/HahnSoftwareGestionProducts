using Apibackend.Models;

namespace Apibackend.Services
{
    /// <summary>
    /// product service interface for dependency injection 
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// method to get all products 
        /// </summary>
        /// <returns></returns>
        List<Product> GetAllProduct();
        /// <summary>
        /// method to get product by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Product GetProductById(int id);
        /// <summary>
        /// method to create a product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        Product CreateProduct(Product product);
        /// <summary>
        /// method to update a product
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedProduct"></param>
        /// <returns></returns>
        Product UpdateProduct(int id, Product updatedProduct);
        /// <summary>
        /// method to delete a product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool DeleteProduct(int id);
    }
}
