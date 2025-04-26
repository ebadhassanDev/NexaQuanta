namespace NexaQuanta.Application.FunctionalTests.Products.Commands;

using NexaQuanta.Application.Products.Commands.CreateProducts;
using NexaQuanta.Application.Products.Commands.DeleteProducts;
using NexaQuanta.Domain.Entities;
using static Testing;
public class DeleteProductTest : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidProductId()
    {
        var invalidId = 99999;

        await FluentActions.Invoking(() =>
            SendAsync(new DeleteProductCommand(invalidId)))
            .Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldDeleteProduct()
    {
        var userId = await RunAsDefaultUserAsync();

        var createCommand = new CreateProductCommand
        {
            Name = "Product To Delete",
            Price = 49.99m,
            Quantity = 5,
            Category = "Books",
            Description = "Product created for delete test.",
            ImageUrl = "https://via.placeholder.com/150",
        };

        var productId = await SendAsync(createCommand);

        var product = await FindAsync<Product>(productId);
        product.Should().NotBeNull();

        await SendAsync(new DeleteProductCommand(productId));

        var deletedProduct = await FindAsync<Product>(productId);
        deletedProduct.Should().BeNull();
    }
}
