namespace Example.Domain.ValueObjects
{
    public class OrderStatus
    {
        public static readonly OrderStatus Open = new(
            id: 1, name: nameof(Open),
            canBeCancelled: true,
            canBeClosed: true);

        public static readonly OrderStatus Cancelled = new(
            id: 2, name: nameof(Cancelled),
            canBeCancelled: false,
            canBeClosed: false);

        public static readonly OrderStatus Closed = new(
            id: 3, name: nameof(Closed),
            canBeCancelled: false,
            canBeClosed: false);

        public int Id { get; private set; }
        public string Name { get; private set; }
        public bool CanBeCancelled { get; private set; }
        public bool CanBeClosed { get; private set; }

        private OrderStatus(int id, string name, bool canBeCancelled, bool canBeClosed)
        {
            Id = id;
            Name = name;
            CanBeCancelled = canBeCancelled;
            CanBeClosed = canBeClosed;
        }

        public static OrderStatus FromName(string name)
        {
            switch (name)
            {
                case nameof(Open):
                    return Open;
                case nameof(Cancelled):
                    return Cancelled;
                case nameof(Closed):
                    return Closed;
            }

            throw new NotSupportedException($"not support OrderStatus name:=[{name}]");

        }
    }
}
