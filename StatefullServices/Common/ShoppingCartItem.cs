using System.Runtime.Serialization;

namespace Common
{
    [DataContract]
    public struct ShoppingCartItem
    {
        [DataMember]
        public string ProductName { get; set; }

        [DataMember]
        public double UnitPrice { get; set; }

        [DataMember]
        public int Amount { get; set; }

        public double LineTotal
        {
            get { return Amount * UnitPrice; }
        }
    }
}
