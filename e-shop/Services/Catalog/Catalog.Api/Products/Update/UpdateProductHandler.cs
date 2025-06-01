using Catalog.Api.Models;
using Marten;
using MediatR;

namespace Catalog.Api.Products.Update
{
    public record UpdateProductCommand(Product Product) : IRequest<UpdateProductResponse>;
    public record UpdateProductResponse(Product Product);
    internal class UpdateProductHandler(IDocumentSession session) : IRequestHandler<UpdateProductCommand, UpdateProductResponse>
    {
        public async Task<UpdateProductResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            session.Update(request.Product);
            await session.SaveChangesAsync(cancellationToken);

            return new UpdateProductResponse(request.Product);
        }
    }
}
