using HomeWork.Server.Models;
using HomeWork.Server.Repositories;
using HomeWork.Server.Services;
using Moq;

namespace HomeWork.Tests.Services
{   
    
    [TestFixture]
        public class ProductServiceTests
        {
            private Mock<IProductRepository> _mockProductRepository;
            private ProductService _productService;

            [SetUp]
            public void Setup()
            {
                _mockProductRepository = new Mock<IProductRepository>();
                _productService = new ProductService(_mockProductRepository.Object);
            }

            [Test]
            public async Task GetProducts_ValidResponse_ReturnsProductList()
            {
                // Arrange
                var mockProducts = new List<Product>
            {
                new Product { title = "Essence Mascara Lash Princess", description = "Productdesclipstick", price = 10 },
                new Product { title = "Essence Mascara Lash PrincessEyeshadow Palette with Mirror", description = "Productdesceyeshadow", price = 9 }
            };
                _mockProductRepository.Setup(repo => repo.GetProducts()).ReturnsAsync(mockProducts);

                // Act
                var result = await _productService.GetProducts();

                // Assert
                Assert.IsNotNull(result);
                Assert.That(result.Count, Is.EqualTo(2));
                Assert.That(result[0].title, Is.EqualTo("Essence Mascara Lash Princess"));
            }

            [Test]
            public async Task GetProducts_EmptyResponse_ReturnsEmptyList()
            {
                // Arrange
                _mockProductRepository.Setup(repo => repo.GetProducts()).ReturnsAsync(new List<Product>());

                // Act
                var result = await _productService.GetProducts();

                // Assert
                Assert.IsNotNull(result);
                Assert.IsEmpty(result);
            }

            [Test]
            public void GetProducts_RepositoryThrowsException_ThrowsException()
            {
                // Arrange
                _mockProductRepository.Setup(repo => repo.GetProducts()).ThrowsAsync(new Exception("Database error"));

                // Act & Assert
                var exception = Assert.ThrowsAsync<Exception>(async () => await _productService.GetProducts());
                Assert.That(exception.Message, Is.EqualTo("Database error"));
            }
        }
}
