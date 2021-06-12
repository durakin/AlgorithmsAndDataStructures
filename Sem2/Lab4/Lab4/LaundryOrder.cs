using System;

namespace Lab4
{
    public class LaundryOrder : IComparable
    {
        private int OrderId { get; }
        private string Name { get; }
        private string ClotheType { get; }

        
        
        public LaundryOrder(int id, string name, string clotheType)
        {
            OrderId = id;
            Name = name;
            ClotheType = clotheType;
        }
        public int CompareTo(object obj)
        {
            switch (obj)
            {
                case LaundryOrder laundryOrder:
                    return string.Compare(Name, laundryOrder.Name, StringComparison.Ordinal);
                case string name:
                    return string.Compare(Name, name, StringComparison.Ordinal);
                default:
                    throw new ArgumentException("Object is not a LaundryOrder");
            }
        }

        public override string ToString()
        {
            return $"Order {OrderId} with clothe {ClotheType} by {Name}";
        }
    }
}