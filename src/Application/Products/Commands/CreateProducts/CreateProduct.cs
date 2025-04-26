using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using NexaQuanta.Application.Common.Interfaces;
using NexaQuanta.Domain.Entities;
using Dapper;


namespace NexaQuanta.Application.Products.Commands.CreateProducts;
public class CreateProductCommand : IRequest<int>
{
    public string Name { get; init; } = string.Empty;
    public decimal Price { get; init; }
    public int Quantity { get; init; }
    public string Category { get; init; } = string.Empty;
    public string? Description { get; init; }
    public string? ImageUrl { get; init; }
}
public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
{
    private readonly IConfiguration _configuration;

    public CreateProductCommandHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        using var connection = new SqlConnection(_configuration.GetConnectionString("NexaQuanta"));

        var entity = new Product
        {
            Name = request.Name,
            Price = request.Price,
            Quantity = request.Quantity,
            Category = request.Category,
            Description = request.Description,
            ImageUrl = request.ImageUrl,
            DateAdded = DateTime.UtcNow
        };
        var product = await connection.ExecuteScalarAsync<int>(
            "sp_InsertProduct",
            entity,
            commandType: System.Data.CommandType.StoredProcedure
        );

        return entity.Id;
    }
}
