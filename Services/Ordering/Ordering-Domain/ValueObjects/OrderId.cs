namespace Ordering_Domain.ValueObjects
{
    public record OrderId
    {
        public Guid Value { get; }
        private OrderId(Guid value) => Value = value;
        public static OrderId Of(Guid value)
        {
            if(value == Guid.Empty)
            {
                throw new DomainException("Orderid can not be empty");
            }
            return new OrderId(value);
        }
    }
}
