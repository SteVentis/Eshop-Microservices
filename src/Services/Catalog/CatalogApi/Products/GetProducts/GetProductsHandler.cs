﻿
namespace CatalogApi.Products.GetProducts;

public record GetProductsQuery() : IQuery<GetProductsResult>;

public record GetProductsResult(IEnumerable<Product> Products);

internal class GetProductsQueryHandler(IDocumentSession session, ILogger<GetProductsQuery> logger) : IQueryHandler<GetProductsQuery, GetProductsResult>
{
	public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
	{
		logger.LogInformation("GetProductsHandler.Handle called with {@Query}", query);

		var products = await session.Query<Product>().ToListAsync(cancellationToken);

		return new GetProductsResult(products);
	}
}
