namespace NexaQuanta.Application.FunctionalTests.Products.Commands;
using NexaQuanta.Application.Products.Commands.CreateProducts;

using NexaQuanta.Application.Products.Commands.UpdateProducts;
using NexaQuanta.Domain.Entities;
using static Testing;
public class UpdateProductTest : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidProductId()
    {
        var invalidId = 99999;

        var updateCommand = new UpdateProductCommand
        {
            Id = invalidId,
            Name = "Updated Name",
            Price = 99.99m,
            Quantity = 10,
            Category = "Electronics",
            Description = "Updated Description",
            ImageUrl = "https://via.placeholder.com/150"
        };

        await FluentActions.Invoking(() =>
            SendAsync(updateCommand))
            .Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldUpdateProduct()
    {
        var userId = await RunAsDefaultUserAsync();

        var createCommand = new CreateProductCommand
        {
            Name = "Original Product",
            Price = 50.00m,
            Quantity = 5,
            Category = "Books",
            Description = "Original Description",
            ImageUrl = "https://via.placeholder.com/150"
        };

        var productId = await SendAsync(createCommand);

        var updateCommand = new UpdateProductCommand
        {
            Id = productId,
            Name = "Updated Product",
            Price = 75.00m,
            Quantity = 8,
            Category = "Updated Books",
            Description = "Updated Description",
            ImageUrl = "https://via.placeholder.com/200"
        };

        await SendAsync(updateCommand);

        var product = await FindAsync<Product>(productId);

        product.Should().NotBeNull();
        product!.Name.Should().Be(updateCommand.Name);
        product.Price.Should().Be(updateCommand.Price);
        product.Quantity.Should().Be(updateCommand.Quantity);
        product.Category.Should().Be(updateCommand.Category);
        product.Description.Should().Be(updateCommand.Description);
        product.ImageUrl.Should().Be(updateCommand.ImageUrl);
        product.LastModifiedBy.Should().Be(userId);
        product.LastModified.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromMilliseconds(10000));
    }
}
