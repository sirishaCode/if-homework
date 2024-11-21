using HomeWork.Server.Configurations;
using HomeWork.Server.Repositories;
using Microsoft.Extensions.Options;
using Moq;
using System.Net;
using System.Text.Json;
using HomeWork.Server.Models;
using Moq.Protected;

namespace HomeWork.Tests.Repositories
{
   
    public class ProductRepositoryTests
    {
        private Mock<IOptions<ApiSettings>> _mockApiSettings;
        private Mock<HttpMessageHandler> _mockHttpMessageHandler;
        private ProductRepository _productRepository;
        private const string ProductApiUrl = "http://api.com/products";

        [SetUp]
        public void Setup()
        {
            // Mock the IOptions<ApiSettings>
            _mockApiSettings = new Mock<IOptions<ApiSettings>>();
            _mockApiSettings.Setup(x => x.Value)
                .Returns(new ApiSettings { ProductApiUrl = ProductApiUrl });

           
            _mockHttpMessageHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);

            // Create an instance of ProductRepository with the mocked dependencies
            var httpClient = new HttpClient(_mockHttpMessageHandler.Object)
            {
                BaseAddress = new Uri(ProductApiUrl)
            };
            _productRepository = new ProductRepository(httpClient, _mockApiSettings.Object);
        }

            [Test]
            public async Task GetProducts_ValidResponse_ReturnsProductList()
            {
                // Arrange
                var mockProducts = new ProductsResponse
                {
                    products = new List<Product>
        {
             new Product { title = "Essence Mascara Lash Princess", description = "Productdesclipstick", price = 10 },
                new Product { title = "Essence Mascara Lash PrincessEyeshadow Palette with Mirror", description = "Productdesceyeshadow", price = 9 }
        }
                };

                var mockResponseContent = JsonSerializer.Serialize(mockProducts);

                _mockHttpMessageHandler
                    .Protected()
                    .Setup<Task<HttpResponseMessage>>("SendAsync",
                        ItExpr.IsAny<HttpRequestMessage>(),
                        ItExpr.IsAny<CancellationToken>())
                    .ReturnsAsync(new HttpResponseMessage
                    {
                        StatusCode = HttpStatusCode.OK,
                        Content = new StringContent(mockResponseContent)
                    });

                // Act
                var result = await _productRepository.GetProducts();

                // Assert
                Assert.IsNotNull(result);
                Assert.That(result.Count, Is.EqualTo(2));
                Assert.That(result[0].title, Is.EqualTo("Essence Mascara Lash Princess"));
              
            }

        [Test]
        public async Task GetProducts_EmptyResponse_ReturnsEmptyList()
        {
            // Arrange
            _mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(string.Empty) // empty response
                });

            // Act
            var result = await _productRepository.GetProducts();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsEmpty(result);
        }

        [Test]
        public async Task GetProducts_NullResponse_ReturnsEmptyList()
        {
            // Arrange
            _mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = null 
                });

            // Act
            var result = await _productRepository.GetProducts();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsEmpty(result);
        }

        [Test]
        public void GetProducts_ApiError_ThrowsException()
        {
            // Arrange
            _mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.InternalServerError
                });

            // Act & Assert
            var ex = Assert.ThrowsAsync<Exception>(async () => await _productRepository.GetProducts());
            Assert.That(ex.Message, Does.Contain("Error while fetching products"));
        }

    }



}
