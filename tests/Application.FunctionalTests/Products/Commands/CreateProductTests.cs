using FluentValidation;
using NexaQuanta.Application.Products.Commands.CreateProducts;
using NexaQuanta.Application.Products.Queries.GetProductsWithPagination;
using NexaQuanta.Domain.Entities;

namespace NexaQuanta.Application.FunctionalTests.Products.Commands;
using static Testing;
public class CreateProductTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new CreateProductCommand();

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldCreateProduct()
    {
        var userId = await RunAsDefaultUserAsync();

        var command = new CreateProductCommand
        {
            Name = "Test Product",
            Price = 99.99m,
            Quantity = 10,
            Category = "Electronics",
            Description = "A test product for unit testing.",
            ImageUrl = "https://via.placeholder.com/150",
        };

        var productId = await SendAsync(command);

        var product = await FindAsync<Product>(productId);

        product.Should().NotBeNull();
        product!.Name.Should().Be(command.Name);
        product.Price.Should().Be(command.Price);
        product.Quantity.Should().Be(command.Quantity);
        product.Category.Should().Be(command.Category);
        product.Description.Should().Be(command.Description);
        product.ImageUrl.Should().Be(command.ImageUrl);
        product.CreatedBy.Should().Be(userId);
        product.Created.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
        product.LastModifiedBy.Should().Be(userId);
        product.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
