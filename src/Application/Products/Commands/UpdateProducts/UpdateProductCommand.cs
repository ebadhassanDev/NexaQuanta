using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Dapper;
using NexaQuanta.Domain.Entities;
namespace NexaQuanta.Application.Products.Commands.UpdateProducts;
public class UpdateProductCommand : IRequest
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string Category { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
}
public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
{
    private readonly IConfiguration _configuration;

    public UpdateProductCommandHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        using var connection = new SqlConnection(_configuration.GetConnectionString("NexaQuanta"));

        var parameters = new Product
        {
           Id = request.Id,
            Name = request.Name,
            Price = request.Price,
            Quantity = request.Quantity,
            Category = request.Category,
            Description = request.Description,
            ImageUrl = request.ImageUrl,
            DateAdded = DateTime.UtcNow
        };

        await connection.ExecuteAsync(
            "sp_UpdateProduct",
            parameters,
            commandType: System.Data.CommandType.StoredProcedure
        );

        return Unit.Value;
    }

    Task IRequestHandler<UpdateProductCommand>.Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        return Handle(request, cancellationToken);
    }
}
