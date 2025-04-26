
using Microsoft.AspNetCore.Http.HttpResults;
using NexaQuanta.Application.Common.Models;
using NexaQuanta.Application.Products.Commands.CreateProducts;
using NexaQuanta.Application.Products.Commands.DeleteProducts;
using NexaQuanta.Application.Products.Commands.UpdateProducts;
using NexaQuanta.Application.Products.Queries.GetProductsWithPagination;
using NexaQuanta.Application.Products.Queries.GetProductWithPaginationById;
using NexaQuanta.Domain.Entities;

namespace NexaQuanta.Web.Endpoints;

public class Product : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetProductsWithPagination)
            .MapGet(GetProductById, "product/{id}")
            .MapPost(CreateProduct)
            .MapPut(UpdateProduct, "{id}")
            .MapDelete(DeleteProduct, "{id}");
    }
    public async Task<Ok<PaginatedList<ProductBriefDto>>> GetProductsWithPagination(ISender sender, [AsParameters] GetProductWithPaginationQuery query)
    {
        var result = await sender.Send(query);
        return TypedResults.Ok(result);
    }

    public async Task<Results<Ok<ProductBriefDto>, NotFound>> GetProductById(ISender sender, int id)
    {
        var product = await sender.Send(new GetProductWithPaginationQueryById(id));
        return product is not null ? TypedResults.Ok(product) : TypedResults.NotFound();
    }

    public async Task<Created<int>> CreateProduct(ISender sender, CreateProductCommand command)
    {
        var id = await sender.Send(command);
        return TypedResults.Created($"/{nameof(Product)}/{id}", id);
    }

    public async Task<Results<NoContent, BadRequest>> UpdateProduct(ISender sender, int id, UpdateProductCommand command)
    {
        if (id != command.Id) return TypedResults.BadRequest();
        await sender.Send(command);
        return TypedResults.NoContent();
    }

    public async Task<NoContent> DeleteProduct(ISender sender, int id)
    {
        await sender.Send(new DeleteProductCommand(id));
        return TypedResults.NoContent();
    }
}
