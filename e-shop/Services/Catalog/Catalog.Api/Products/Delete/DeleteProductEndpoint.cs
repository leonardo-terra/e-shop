using Carter;
using Mapster;
using MediatR;

namespace Catalog.Api.Products.Delete
{
    public class DeleteProductEndpoint : ICarterModule
    {
        private record DeleteProductResponse(Guid Id);

        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/products/{id:guid}",
                async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new DeleteProductCommand(id));

                var response = result.Adapt<DeleteProductResponse>();

                return Results.Ok(response);
            });
        }
    }
}
