using Catalog.Api.Models;
using Marten;
using MediatR;

namespace Catalog.Api.Products.Get
{
    public record GetProductsQuery() : IRequest<GetProductsResult>;

    public record GetProductsResult(List<Product> productList);
    internal class GetProductsHandler(IDocumentSession dbSession) : IRequestHandler<GetProductsQuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
