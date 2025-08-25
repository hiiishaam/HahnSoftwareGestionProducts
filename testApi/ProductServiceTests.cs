using Apibackend.Data;
using Apibackend.Hubs;
using Apibackend.Models;
using Apibackend.Services;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using static Apibackend.Hubs.Hubs;

namespace testApi
{
    /// <summary>
    /// classe de test unitaire pour ProductService
    /// </summary>
    [TestClass]
    public class ProductServiceTests
    {
        private AppDbContext _context;
        private ProductService _service;
        private Mock<IHubContext<ProductHub>> _hubContextMock;

        [TestInitialize]
        public void Setup()
        {
            // Cr�er une base InMemory unique
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
                .Options;

            _context = new AppDbContext(options);

            // Mock HubContext
            _hubContextMock = new Mock<IHubContext<ProductHub>>();

            // Pour �viter les erreurs, mocker Clients.All.SendAsync
            var clientsMock = new Mock<IHubClients>();
            var clientProxyMock = new Mock<IClientProxy>();
            clientsMock.Setup(c => c.All).Returns(clientProxyMock.Object);
            _hubContextMock.Setup(h => h.Clients).Returns(clientsMock.Object);

            _service = new ProductService(_context, _hubContextMock.Object);
        }
        /// <summary>
        /// test de la m�thode CreateProduct du service test si le produit est bien ajout�
        /// </summary>
        [TestMethod]
        public void CreateProduct_ShouldAddProduct()
        {
            var product = new Product { Name = "Produit1", Description = "Desc1", Price = 10 };

            var result = _service.CreateProduct(product);

            Assert.IsNotNull(result);
            Assert.AreEqual("Produit1", result.Name);
            Assert.AreEqual(1, _context.Products.Count());
        }
        /// <summary>
        /// test de la m�thode GetProductById du service test si le produit retourn� est correct
        /// </summary>
        [TestMethod]
        public void GetProductById_ShouldReturnProduct()
        {
            var product = new Product { Name = "Produit2", Description = "Desc2", Price = 20 };
            _service.CreateProduct(product);

            var result = _service.GetProductById(product.Id);

            Assert.IsNotNull(result);
            Assert.AreEqual("Produit2", result.Name);
        }
        /// <summary>
        /// test de la m�thode UpdateProduct du service test si le produit est bien modifi�
        /// </summary>
        [TestMethod]
        public void UpdateProduct_ShouldModifyProduct()
        {
            var product = new Product { Name = "Produit3", Description = "Desc3", Price = 30 };
            _service.CreateProduct(product);

            var updated = new Product { Name = "Produit3Modifi�", Description = "Desc3Modifi�", Price = 35 };
            var result = _service.UpdateProduct(product.Id, updated);

            Assert.IsNotNull(result);
            Assert.AreEqual("Produit3Modifi�", result.Name);
            Assert.AreEqual(35, result.Price);
        }
        /// <summary>
        /// test de la m�thode DeleteProduct du service test si le produit est bien supprim�
        /// </summary>
        [TestMethod]
        public void DeleteProduct_ShouldRemoveProduct()
        {
            var product = new Product { Name = "Produit4", Description = "Desc4", Price = 40 };
            _service.CreateProduct(product);

            var result = _service.DeleteProduct(product.Id);

            Assert.IsTrue(result);
            Assert.AreEqual(0, _context.Products.Count());
        }
    }
}
