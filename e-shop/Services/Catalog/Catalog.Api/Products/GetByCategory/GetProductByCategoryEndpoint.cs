using Carter;
using Catalog.Api.Models;
using Mapster;
using MediatR;

namespace Catalog.Api.Products.GetByCategory
{
    public class GetProductByCategoryEndpoint : ICarterModule
    {
        public record GetProductByCategoryResult(IEnumerable<Product> Products);
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/category/{category}", async (string category, ISender mediator) =>
            {
                try
                {
                    var query = new GetProductByCategoryQuery(category);
                    var result = await mediator.Send(query);
                    var response = result.Adapt<GetProductByCategoryResult>();

                    return Results.Ok(response);
                }
                catch (KeyNotFoundException ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            })

        .WithName("GetProductByCategory")
        .Produces<List<Product>>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Returns products by Category")
        .WithDescription("Get Product by Category");
        }
    }
}
