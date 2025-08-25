using Apibackend.Data;
using Apibackend.Events;
using Apibackend.Hubs;
using Apibackend.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using static Apibackend.Hubs.Hubs;

namespace Apibackend.Services
{
    /// <summary>
    /// implementation of IProductService to manage products and emit events via SignalR
    /// </summary>
    public class ProductService : IProductService
    {
        /// <summary>
        /// instance of AppDbContext for database operations
        /// </summary>
        private readonly AppDbContext _context;
        /// <summary>
        /// instance of IHubContext to communicate with SignalR hubs
        /// </summary>
        private readonly IHubContext<ProductHub> _hubContext;

        /// <summary>
        /// constructor accepting AppDbContext and IHubContext for dependency injection
        /// </summary>
        /// <param name="context"></param>
        /// <param name="hubContext"></param>
        public ProductService(AppDbContext context, IHubContext<ProductHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }
        /// <summary>
        /// method to get all products from the database
        /// </summary>
        /// <returns></returns>
        public List<Product> GetAllProduct()
        {
            return _context.Products.ToList();
        }
        /// <summary>
        /// method to get a product by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Product GetProductById(int id)
        {
            return _context.Products.Find(id);
        }
        /// <summary>
        /// method to create a new product and emit a ProductCreated event via SignalR
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public Product CreateProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();

            // Émettre un event SignalR
            var evt = new ProductCreatedEvent(product.Id, product.Name);
            DispatchEvent("ProductCreated", evt);

            return product;
        }
        /// <summary>
        /// method to update an existing product and emit a ProductUpdated event via SignalR
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedProduct"></param>
        /// <returns></returns>
        public Product UpdateProduct(int id, Product updatedProduct)
        {
            var product = _context.Products.Find(id);
            if (product == null) return null;

            product.Name = updatedProduct.Name;
            product.Description = updatedProduct.Description;
            product.Price = updatedProduct.Price;
            product.UpdatedAt = DateTime.UtcNow;

            _context.Products.Update(product);
            _context.SaveChanges();

            // Émettre un event SignalR
            var evt = new ProductUpdatedEvent(product.Id, product.Name, product.Price);
            DispatchEvent("ProductUpdated", evt);

            return product;
        }
        /// <summary>
        /// method to delete a product by its id and emit a ProductDeleted event via SignalR
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null) return false;

            _context.Products.Remove(product);
            _context.SaveChanges();

            // Émettre un event SignalR
            var evt = new ProductDeletedEvent(id);
            DispatchEvent("ProductDeleted", evt);

            return true;
        }
        /// <summary>
        /// method to dispatch an event to all connected SignalR clients
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="domainEvent"></param>
        private void DispatchEvent(string eventName, object domainEvent)
        {
            _hubContext.Clients.All.SendAsync(eventName, domainEvent);
            Console.WriteLine($"📢 Event envoyé : {eventName} -> {domainEvent}");
        }
    }
}
