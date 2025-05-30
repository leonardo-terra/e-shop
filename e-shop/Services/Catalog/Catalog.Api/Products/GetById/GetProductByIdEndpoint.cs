using Carter;
using Catalog.Api.Models;
using Mapster;
using MediatR;

namespace Catalog.Api.Products.GetById
{
    public record GetProductByIdResponse(Product Product);
    public class GetProductByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/{id:guid}",
                async (Guid id, ISender sender) =>
                {
                    try
                    {
                        var query = new GetProductByIdQuery(id);
                        var result = await sender.Send(query);
                        return Results.Ok(result.Adapt<GetProductByIdResponse>());
                    }
                    catch (KeyNotFoundException ex)
                    {
                        return Results.BadRequest(ex.Message);    
                    }
                })
        .WithName("GetProductById")
        .Produces<List<Product>>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Returns product by Id")
        .WithDescription("Get Product");
        }
    }
}
