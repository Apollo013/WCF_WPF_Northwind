namespace LINQNorthwind.Models.BusinessDomainObjects
{
    /// <summary>
    /// Product Business Domain Object
    /// </summary>
    public class ProductBDO
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
        public int ReorderLevel { get; set; }
        public int UnitsOnOrder { get; set; }
        public bool Discontinued { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
