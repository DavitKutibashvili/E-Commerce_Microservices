
using Discount_gRPC.Protos;

namespace Basket_API.Basket.StoreBasket
{
    public record StoreBasketCommand(ShoppingCart shoppingCart) : ICommand<StoreBasketResult>;
    public record StoreBasketResult(string userName);
    public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketCommandValidator()
        {
            RuleFor(x => x.shoppingCart).NotNull().WithMessage("Shopping cart cannot be null.");
            RuleFor(x => x.shoppingCart.UserName).NotEmpty().WithMessage("UserName cannot be empty.");
        }
    }
    internal class StoreBasketCommandHandler(IBasketRepository basketRepository, DiscountService.DiscountServiceClient discountProto) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            await DeductDiscount(command.shoppingCart, cancellationToken);
            await basketRepository.StoreBasket(command.shoppingCart, cancellationToken);
            return new StoreBasketResult(command.shoppingCart.UserName);
        }
        private async Task DeductDiscount(ShoppingCart shoppingCart, CancellationToken cancellationToken)
        {
            foreach (var item in shoppingCart.Items)
            {
                var discount = await discountProto.GetDiscountAsync(new GetDiscountRequest { ProductName = item.ProductName });
                item.Price -= discount.Amount;
            }
        }
    }
}
