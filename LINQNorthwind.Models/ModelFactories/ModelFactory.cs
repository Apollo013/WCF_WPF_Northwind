using LINQNorthwind.Models.BusinessDomainObjects;
using LINQNorthwind.Models.DataTransferObjects;
using LINQNorthwind.Models.DomainEntities;

namespace LINQNorthwind.Models.ModelFactories
{
    public class ModelFactory
    {
        public ProductDTO Create(Product model)
        {
            return new ProductDTO
            {
                ProductID = model.ProductID,
                ProductName = model.ProductName,
                QuantityPerUnit = model.QuantityPerUnit,
                UnitPrice = model.UnitPrice,
                Discontinued = model.Discontinued,
                RowVersion = model.RowVersion
            };
        }

        public ProductDTO Create(ProductBDO model)
        {
            return new ProductDTO
            {
                ProductID = model.ProductID,
                ProductName = model.ProductName,
                QuantityPerUnit = model.QuantityPerUnit,
                UnitPrice = model.UnitPrice,
                Discontinued = model.Discontinued,
                RowVersion = model.RowVersion
            };
        }
    }
}
