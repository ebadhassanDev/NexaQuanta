using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using NexaQuanta.Application.Products.Queries.GetProductsWithPagination;
using NexaQuanta.Domain.Entities;

namespace NexaQuanta.Application.Products.Queries.GetProductWithPaginationById;
public record GetProductWithPaginationQueryById(int id) : IRequest<ProductBriefDto>;
public class GetProductWithPaginationQueryHandler : IRequestHandler<GetProductWithPaginationQueryById, ProductBriefDto>
{
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    public GetProductWithPaginationQueryHandler(IConfiguration configuration, IMapper mapper)
    {
        _configuration = configuration;
        _mapper = mapper;
    }

    public async Task<ProductBriefDto> Handle(GetProductWithPaginationQueryById request, CancellationToken cancellationToken)
    {
        using var connection = new SqlConnection(_configuration.GetConnectionString("NexaQuanta"));

        var parameters = new DynamicParameters();
        parameters.Add("@Id", request);

        var product = await connection.QueryFirstOrDefaultAsync<Product>(
            "sp_GetProductById",
            parameters,
            commandType: CommandType.StoredProcedure
        );

        var productMapper = _mapper.Map<ProductBriefDto>(product);

        return productMapper;
    }
}

