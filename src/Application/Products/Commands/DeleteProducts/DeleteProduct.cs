
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Dapper;
namespace NexaQuanta.Application.Products.Commands.DeleteProducts;
public record DeleteProductCommand(int id) : IRequest<bool>;
public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
{
    private readonly IConfiguration _configuration;
    public DeleteProductCommandHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        using var connection = new SqlConnection(_configuration.GetConnectionString("NexaQuanta"));

        int success = await connection.ExecuteAsync(
            "sp_DeleteProduct",
            new { request },
            commandType: System.Data.CommandType.StoredProcedure
        );

        return success is 1;
    }
}
