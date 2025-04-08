﻿
using CatalogApi.Products.CreateProduct;

namespace CatalogApi.Products.GetProducts;

public record GetProductsRepsonse(IEnumerable<Product> Products);

public class GetProductsEndpoint : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapGet("/products", async (ISender sender) =>
		{
			var result = await sender.Send(new GetProductsQuery());

			var response = result.Adapt<GetProductsRepsonse>();

			return Results.Ok(response);
		})
		.WithName("GetProducts")
		.Produces<CreateProductResponse>(StatusCodes.Status200OK)
		.ProducesProblem(StatusCodes.Status400BadRequest)
		.WithSummary("Get Products")
		.WithDescription("Get Products");
	}
}
