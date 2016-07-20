using System.Runtime.Serialization;

namespace LINQNorthwind.Models.DataTransferObjects
{
    /// <summary>
    /// Product Data Transfer Object
    /// </summary>
    [DataContract]
    public class ProductDTO
    {
        [DataMember]
        public int ProductID { get; set; }
        [DataMember]
        public string ProductName { get; set; }
        [DataMember]
        public string QuantityPerUnit { get; set; }
        [DataMember]
        public decimal UnitPrice { get; set; }
        [DataMember]
        public bool Discontinued { get; set; }
        [DataMember]
        public byte[] RowVersion { get; set; }
    }
}
