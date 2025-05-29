using Catalog.Api.Models;
using Marten;
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
    public record CreateProductResult(Guid id);

    internal class CreateProductCommandHandler(IDocumentSession dbSession) : IRequestHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Categories = request.Categories,
                Description = request.Description,
                ImageFile = request.ImageFile,
                Price = request.Price
            };

            dbSession.Store(product);

            await dbSession.SaveChangesAsync(cancellationToken);

            return new CreateProductResult(product.Id);
        }
    }
}
