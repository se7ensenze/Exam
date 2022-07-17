namespace Example.Domain.ValueObjects
{
    public class OrderSide
    {
        public static readonly OrderSide Bid = new(name: nameof(Bid));
        public static readonly OrderSide Ask = new(name: nameof(Ask));

        public string Name { get; } = string.Empty;
        private OrderSide(string name)
        {
            Name = name;
        }

        public static OrderSide FromName(string name)
        {
            switch (name)
            {
                case nameof(Bid):
                    return Bid;
                case nameof(Ask):
                    return Ask;
            }

            throw new NotSupportedException($"not support OrderSide name:=[{name}]");
        }
    }
}
