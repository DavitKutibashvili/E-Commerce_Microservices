using BuildingBlocks.Exceptions;

namespace Catalog_API.Exceptions
{
    public class ProductNotFoundException : NotFoundException
    {
        public ProductNotFoundException(Guid id) : base ("Product", id)
        {
            
        }
    }
}
