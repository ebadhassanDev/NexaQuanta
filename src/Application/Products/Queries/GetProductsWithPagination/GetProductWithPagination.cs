using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using NexaQuanta.Application.Common.Models;
using Dapper;

namespace NexaQuanta.Application.Products.Queries.GetProductsWithPagination;
public record GetProductWithPaginationQuery : IRequest<PaginatedList<ProductBriefDto>>
{
    public int ListId { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}
public class GetProductsWithPAginationQueryHandler : IRequestHandler<GetProductWithPaginationQuery, PaginatedList<ProductBriefDto>>
{
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;
    public GetProductsWithPAginationQueryHandler(IMapper mapper, IConfiguration configuration)
    {
        _mapper = mapper;
        _configuration = configuration;
    }

    public async Task<PaginatedList<ProductBriefDto>> Handle(GetProductWithPaginationQuery request, CancellationToken cancellationToken)
    {
        using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

        var allProducts = await connection.QueryAsync<ProductBriefDto>(
            "sp_GetAllProducts",
            commandType: System.Data.CommandType.StoredProcedure
        );

        var mappedProducts = _mapper.Map<PaginatedList<ProductBriefDto>>(allProducts);


        var pagedProducts = mappedProducts.Items
        .Skip((request.PageNumber - 1) * request.PageSize)
        .Take(request.PageSize)
        .ToList();

        return new PaginatedList<ProductBriefDto>(
            pagedProducts,
            mappedProducts.Items.Count,
            request.PageNumber,
            request.PageSize
        );
    }
}
