
using Moq;
using Resources.Interfaces;
using Resources.Models;
using Resources.Services;

namespace Resources.Tests.UnitTests;

public class ProductService_Tests
{
    private readonly Mock<IProductService<Product, Product>> _mockProductService = new();

    [Fact]

    public void CreateProduct_ShouldReturnSuccessFeedback_WhenProductIsCreated()
    {
        //Arrange
        var product = new Product { ProductName = "iPhone 16", ProductSpecification = "4K Camera", ProductPrice = 17490 };
        var expectedFeedback = new FeedbackStatus<Product> { Succeeded = true, Feedback = product, Message = "Product successfully created!" };

        _mockProductService.Setup(productService => productService.CreateProduct(product)).Returns(expectedFeedback);
        var productService = _mockProductService.Object;

        //Act
        var feedback = productService.CreateProduct(product);

        //Assert
        Assert.True(feedback.Succeeded);
        Assert.Equal(product, feedback.Feedback);
    }

    [Fact]

    public void GetAllProducts_ShouldReturnListOfProducts()
    {
        //Arrange
        var product = new Product { ProductName = "iPhone 16", ProductSpecification = "4K Camera", ProductPrice = 17490 };
        var products = new List<Product> { product };
        var expectedFeedback = new FeedbackStatus<IEnumerable<Product>> { Succeeded = true, Feedback = products };

        _mockProductService.Setup(productService => productService.GetAllProducts()).Returns(expectedFeedback);
        var productsService = _mockProductService.Object;

        //Act
        var feedback = productsService.GetAllProducts();

        //Assert
        Assert.True(feedback.Succeeded);
        Assert.Equal(products, feedback.Feedback);
    }

    [Fact]
    public void GetSingleProduct_ShouldReturnProduct_WhenProductExistsInList()
    {
        //Arrange
        var id = Guid.NewGuid().ToString();
        var product = new Product { Id = id, ProductName = "iPhone 16", ProductSpecification = "4K Camera", ProductPrice = 17490 };
        var expectedFeedback = new FeedbackStatus<Product> { Succeeded = true, Feedback = product};

        _mockProductService.Setup(productService => productService.GetSingleProduct(id)).Returns(expectedFeedback);
        var productService = _mockProductService.Object;

        //Act
        var feedback = productService.GetSingleProduct(id);

        //Assert
        Assert.True(feedback.Succeeded);
        Assert.Equal(product, feedback.Feedback);
    }

    [Fact]
    public void UpdateProduct_ShouldReturnProduct_WhenProductIsUpdated()
    {
        //Arrange
        var id = Guid.NewGuid().ToString();
        var product = new Product { Id = id, ProductName = "iPhone 16", ProductSpecification = "4K Camera", ProductPrice = 17490 };
        var updatedProduct = new Product { Id = id, ProductName = "iPhone 16", ProductSpecification = "4K Camera", ProductPrice = 17000 };
        var expectedFeedback = new FeedbackStatus<Product> { Succeeded = true, Feedback =  updatedProduct };

        _mockProductService.Setup(productService => productService.UpdateProduct(id, updatedProduct)).Returns(expectedFeedback);
        var productService = _mockProductService.Object;

        //Act
        var feedback = productService.UpdateProduct(id, updatedProduct);

        //Assert
        Assert.True(feedback.Succeeded);
        Assert.NotEqual(product, feedback.Feedback);
    }

    [Fact]
    public void DeleteProduct_ShouldReturnSuccessFeedback_WhenProductIsDeleted()
    {
        //Arrange
        var id = Guid.NewGuid().ToString();
        var expectedFeedback = new FeedbackStatus<Product> { Succeeded = true };

        _mockProductService.Setup(productService => productService.DeleteProduct(id)).Returns(expectedFeedback);
        var productService = _mockProductService.Object;

        //Act
        var feedback = productService.DeleteProduct(id);

        //Assert
        Assert.True(feedback.Succeeded);
    }

}
