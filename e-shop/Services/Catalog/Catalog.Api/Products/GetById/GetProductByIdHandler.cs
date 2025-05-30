using Catalog.Api.Models;
using Marten;
using MediatR;

namespace Catalog.Api.Products.GetById
{
    public record GetProductByIdQuery(Guid Id) : IRequest<GetProductByIdResult>;
    public record GetProductByIdResult(Product Product);

    internal class GetProductByIdHandler(IDocumentSession session) : IRequestHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await session.LoadAsync<Product>(request.Id);

            if (result is null)
                throw new KeyNotFoundException($"Product with ID {request.Id} not found.");

            return new GetProductByIdResult(result);
        }
    }
}
