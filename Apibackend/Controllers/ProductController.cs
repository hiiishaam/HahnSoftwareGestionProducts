using Apibackend.DTOS;
using Apibackend.Models;
using Apibackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Apibackend.Controllers
{
    /// <summary>
    /// Provides endpoints for managing products in the system.
    /// </summary>
    /// <remarks>This controller handles CRUD operations for products, including retrieving all products, 
    /// retrieving a product by its ID, creating new products, updating existing products, and deleting products. All
    /// endpoints require authorization and are accessible via the "api/products" route.</remarks>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        /// <summary>
        /// Represents the service used to manage product-related operations.
        /// </summary>
        /// <remarks>This field is a dependency injected instance of <see cref="IProductService"/>. It is
        /// used to perform operations related to products, such as retrieving, creating, or updating product
        /// data.</remarks>
        private readonly IProductService _productService;
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductsController"/> class.
        /// </summary>
        /// <param name="productService">The service used to manage product-related operations. This parameter cannot be null.</param>
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Retrieves all products from the system.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var products = _productService.GetAllProduct();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur interne : {ex.Message}");
            }
        }

        /// <summary>
        /// gets a product by its ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            try
            {
                var product = _productService.GetProductById(id);
                if (product == null)
                    return NotFound($"Produit avec ID {id} introuvable.");
                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur interne : {ex.Message}");
            }
        }

        /// <summary>
        /// creates a new product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(Product product)
        {
            try
            {
                var createdProduct = _productService.CreateProduct(product);
                return CreatedAtAction(nameof(GetById), new { id = createdProduct.Id }, createdProduct);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur interne lors de la création : {ex.Message}");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ActionResult Update(int id, Product product)
        {
            try
            {
                var updatedProduct = _productService.UpdateProduct(id, product);
                if (updatedProduct == null)
                    return NotFound($"Produit avec ID {id} introuvable.");
                return Ok(updatedProduct);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur interne lors de la mise à jour : {ex.Message}");
            }
        }

        /// <summary>
        /// deletes a product by its ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _productService.DeleteProduct(id);

            if (!result)
                return NotFound(new { message = $"Produit avec ID {id} introuvable." });

            return Ok(new ResponseMessage { Message = "Produit supprimé avec succès" });

        }





    }
}
