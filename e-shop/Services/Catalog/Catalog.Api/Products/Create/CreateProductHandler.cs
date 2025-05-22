using Catalog.Api.Models;
using MediatR;

namespace Catalog.Api.Products.Create
{
    public record CreateProductCommand(
        string Name,
        List<Category> Categories,
        string Description,
        string ImageFile,
        decimal Price
    ) : IRequest<CreateProductResult>;
    public record CreateProductResult(Guid id) { }

    internal class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreateProductResult>
    {
        public Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
