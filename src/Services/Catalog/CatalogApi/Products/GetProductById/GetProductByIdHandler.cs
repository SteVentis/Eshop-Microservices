using CatalogApi.Exceptions;
using Marten.Linq.QueryHandlers;

namespace CatalogApi.Products.GetProductById;

public record GetProductByIdQuery(Guid Id) : IQuery<GetProductbyIdResult>;

public record GetProductbyIdResult(Product Product);

internal class GetProductByIdQueyHandler(IDocumentSession session, ILogger<GetProductByIdQueyHandler> logger) : IQueryHandler<GetProductByIdQuery, GetProductbyIdResult>
{
	public async Task<GetProductbyIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
	{
		logger.LogInformation("GetProductByIdQueryHandler.Handle called with {@Query}", query);

		var product = await session.LoadAsync<Product>(query.Id, cancellationToken);

		if(product is null)
		{
			throw new ProductNotFoundException();
		}

		return new GetProductbyIdResult(product);
	}
}
