using Catalog.Api.Models;
using Marten;
using MediatR;

namespace Catalog.Api.Products.Delete
{
    public record DeleteProductCommand(Guid Id) : IRequest<DeleteProductResponse>;
    public record DeleteProductResponse(Guid Id);

    public class DeleteProductHandler(IDocumentSession session) : IRequestHandler<DeleteProductCommand, DeleteProductResponse>
    {
        public async Task<DeleteProductResponse> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            session.Delete<Product>(request.Id);
            await session.SaveChangesAsync(cancellationToken);

            return new DeleteProductResponse(request.Id);
        }
    }
}
