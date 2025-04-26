using NexaQuanta.Domain.Entities;

namespace NexaQuanta.Application.Products.Queries.GetProductsWithPagination;
public class ProductBriefDto
{
    public int Id { get; init; }
    public string? Name { get; init; }
    public string? Description { get; init; }
    public string? ImageUrl { get; init; }   
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Product, ProductBriefDto>();
        }
    }
}
