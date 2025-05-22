using Carter;
using Catalog.Api.Models;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Products.Create
{
    public record class CreateProductRequest(string Name, string Description, string ImageFile, decimal Price, List<Category> Categories);
    public record class CreateProductResponse(Guid Id);
    internal class CreateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/products",
                 async (CreateProductRequest request, ISender sender, CancellationToken cancellationToken) =>
              {
                  var command = request.Adapt<CreateProductCommand>();
                  var result = await sender.Send(command, cancellationToken);

                  var response = result.Adapt<CreateProductResponse>();
                  return Results.Created($"/products/{response.Id}", response);

              })
                .WithName("CreateProduct")
                .Produces<CreateProductResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Create Product")
                .WithDescription("Create a new product");
        }
    }
    [ApiController]
    [Route("products")]
    public class CreateProductController : ControllerBase
    {
        private readonly ISender _sender;

        public CreateProductController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateProductResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [ApiExplorerSettings(GroupName = "Create Product")]
        [ActionName("CreateProduct")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request, CancellationToken cancellationToken)
        {
            var command = request.Adapt<CreateProductCommand>();
            var result = await _sender.Send(command, cancellationToken);

            var response = result.Adapt<CreateProductResponse>();
            return Created($"/products/{response.Id}", response);
        }
    }
}
