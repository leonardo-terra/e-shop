using Catalog.Api.Models;
using Marten;
using MediatR;

namespace Catalog.Api.Products.GetByCategory
{
    public record GetProductByCategoryQuery(string Categories) : IRequest<GetProductByCategoryResult>;
    public record GetProductByCategoryResult(IEnumerable<Product> Products);


    internal class GetProductByCategoryHandler(IDocumentSession session) : IRequestHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
    {
        public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery request, CancellationToken cancellationToken)
            {

            var result = await session.Query<Product>()
                .Where(p => p.Categories.Any(c => c.Equals(request.Categories, StringComparison.OrdinalIgnoreCase)))
                .ToListAsync(cancellationToken);

            if (result is null || !result.Any())
                throw new KeyNotFoundException($"No products found in category {request.Categories}.");

            return new GetProductByCategoryResult(result);
        }
    }
}
