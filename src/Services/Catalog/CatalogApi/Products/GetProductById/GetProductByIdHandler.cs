using CatalogApi.Exceptions;
using Marten.Linq.QueryHandlers;

namespace CatalogApi.Products.GetProductById;

public record GetProductByIdQuery(Guid Id) : IQuery<GetProductbyIdResult>;

public record GetProductbyIdResult(Product Product);

internal class GetProductByIdQueyHandler(IDocumentSession session : IQueryHandler<GetProductByIdQuery, GetProductbyIdResult>
{
	public async Task<GetProductbyIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
	{
		var product = await session.LoadAsync<Product>(query.Id, cancellationToken);

		if(product is null)
		{
			throw new ProductNotFoundException(query.Id);
		}

		return new GetProductbyIdResult(product);
	}
}
