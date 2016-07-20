using LINQNorthwind.Models.BusinessDomainObjects;
using LinqNorthwindDAL;

namespace LINQNorthwindLogic
{
    public class ProductLogic
    {
        ProductDAO productDAO = new ProductDAO();

        public ProductBDO GetProduct(int id)
        {
            return productDAO.GetProduct(id);
        }

        public bool UpdateProduct(ref ProductBDO productBDO, ref string message)
        {
            var productInDb = GetProduct(productBDO.ProductID);
            if (productInDb == null)
            {
                message = "Product not Found";
                return false;
            }

            // a product cannot be discontinued if there are non-fulfilled orders
            if (productBDO.Discontinued == true && productInDb.UnitsOnOrder > 0)
            {
                message = "This Product Cannot Be Discontinued";
                return false;
            }
            else
            {
                return productDAO.UpdateProduct(ref productBDO, ref message);
            }
        }
    }
}
