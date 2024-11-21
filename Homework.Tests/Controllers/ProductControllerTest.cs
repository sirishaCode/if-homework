using System;
using System.Collections.Generic;
using System.Linq;
using HomeWork.Server.Controllers;
using HomeWork.Server.Models;
using HomeWork.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

    namespace HomeWork.Tests.Controllers
    {
        [TestFixture]
        public class ProductControllerTests
        {
            private Mock<IProductService> _mockProductService;
          
            private ProductController _controller;

            [SetUp]
            public void Setup()
            {
                _mockProductService = new Mock<IProductService>();
                _controller = new ProductController(_mockProductService.Object);
            }

            [Test]
            public async Task GetProducts_ReturnsOkResult_WithProducts()
            {
                // Arrange
                var mockProducts = new List<Product>
            {
                new Product { title = "Essence Mascara Lash Princess", description = "Productdesclipstick", price = 10 },
                new Product { title = "Essence Mascara Lash PrincessEyeshadow Palette with Mirror", description = "Productdesceyeshadow", price = 9 }
            };
                _mockProductService.Setup(service => service.GetProducts()).ReturnsAsync(mockProducts);

                // Act
                var result = await _controller.GetProducts();

                // Assert
                Assert.IsInstanceOf<OkObjectResult>(result); // Validate the result is OkObjectResult
                var okResult = result as OkObjectResult; // Cast to OkObjectResult
                Assert.NotNull(okResult); // Ensure it is not null

                var returnedProducts = okResult.Value as List<Product>; // Cast to List<Product>
                Assert.NotNull(returnedProducts); // Ensure the list is not null
                Assert.That(returnedProducts.Count, Is.EqualTo(2)); // Verify the product count
            }

            [Test]
            public async Task GetProducts_ReturnsOkResult_WithEmptyList_WhenNoProducts()
            {
                // Arrange
                _mockProductService.Setup(service => service.GetProducts()).ReturnsAsync(new List<Product>());

                // Act
                var result = await _controller.GetProducts();

                // Assert
                Assert.IsInstanceOf<OkObjectResult>(result); // Verify the result is of type OkObjectResult
                var okResult = result as OkObjectResult; // Cast to OkObjectResult
                Assert.NotNull(okResult); // Ensure casting is successful

                var returnedProducts = okResult.Value as List<Product>; // Cast Value to List<Product>
                Assert.NotNull(returnedProducts); // Ensure the list is not null
                Assert.That(returnedProducts.Count, Is.EqualTo(0)); // Verify the count is 0

            }

          
        }
    }

