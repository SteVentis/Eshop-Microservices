using BuildingBlocks.Exceptions;

namespace CatalogApi.Exceptions;

[Serializable]
internal class ProductNotFoundException : NotFoundException
{
	public ProductNotFoundException(Guid Id) : base("Product", Id)
	{
	}
}