namespace Ordering_Domain.ValueObjects
{
    public record ProductId
    {
        public Guid Value { get; }
        private ProductId(Guid value) => Value = value;
        public static ProductId Of(Guid value)
        {
            if(value == Guid.Empty)
            {
                throw new DomainException("ProductId can not be null");
            }
            return new ProductId(value);
        }
    }
}
