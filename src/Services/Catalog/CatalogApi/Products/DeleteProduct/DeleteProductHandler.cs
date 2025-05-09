
using CatalogApi.Products.GetProducts;

namespace CatalogApi.Products.DeleteProduct
{
	public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;

	public record DeleteProductResult(bool IsSuccess);

	internal class DeleteProductHandler(IDocumentSession session, ILogger<GetProductsQuery> logger) : ICommandHandler<DeleteProductCommand, DeleteProductResult>
	{
		public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
		{
			logger.LogInformation("DeleteProductHandler.Handle called with {@Command}", command);

			session.Delete<Product>(command.Id);

			await session.SaveChangesAsync(cancellationToken);

			return new DeleteProductResult(true);
		}
	}
}
