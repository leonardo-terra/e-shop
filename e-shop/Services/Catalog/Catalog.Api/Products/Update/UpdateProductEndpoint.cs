using Carter;
using Catalog.Api.Models;
using Mapster;
using MediatR;

namespace Catalog.Api.Products.Update
{
    public class UpdateProductEndpoint : ICarterModule
    {
        public record UpdateProductResponse(Product Product);
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/products/update",
                async (UpdateProductCommand command, ISender sender) =>
                {
                    var result = await sender.Send(command);
                    var response = result.Adapt<UpdateProductResponse>();

                    return Results.Ok(response);
                });
        }
    }
}
