using Carter;
using Catalog.Api.Models;
using Catalog.Api.Products.Get;
using Mapster;
using MediatR;

public record GetProductsRequest();

public record GetProductsResponse(List<Product> ProductsList);


public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products",
            async (ISender sender) =>
            {
                var query = new GetProductsQuery();
                var result = await sender.Send(query);
                result.Adapt<GetProductsResponse>();
                return Results.Ok(result);
            })
        .WithName("GetProducts")
        .Produces<List<Product>>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Returns all products listed")
        .WithDescription("Get Products");
    }
}