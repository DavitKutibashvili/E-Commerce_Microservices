namespace Catalog_API.Products.DeleteProduct
{
    public record DeleteProductCommand(Guid id) : ICommand<DeleteProductResult>;
    public record DeleteProductResult(bool isSuccess);
    internal class DeleteProductCommandHandler(IDocumentSession session, ILogger<DeleteProductCommandHandler> logger) : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("Started handling of deleting command with: {Command}", command);
            session.Delete<Product>(command.id);
            await session.SaveChangesAsync();
            return new DeleteProductResult(true);
        }
    }
}
