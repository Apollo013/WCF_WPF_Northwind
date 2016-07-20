using LINQNorthwind.Models.DataTransferObjects;
using LINQNorthwind.Models.ModelFactories;
using LinqNorthwindDAL;
using System;

namespace LINQNorthwindLogic
{
    public class ProductLogic
    {
        ProductDAO productDAO = new ProductDAO();
        ModelFactory modelFactory = new ModelFactory();

        /// <summary>
        /// Gets a Product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ProductDTO GetProduct(int id)
        {
            return modelFactory.Create(productDAO.GetProduct(id));
        }

        /// <summary>
        /// Updates Product Table
        /// </summary>
        /// <param name="product"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool UpdateProduct(ref ProductDTO product, ref string message)
        {
            var productInDB = productDAO.GetProduct(product.ProductID);
            if (productInDB == null)
            {
                message = "Product not Found";
                throw new Exception("No product found with id: " + product.ProductID);
            }

            // a product cannot be discontinued if there are non-fulfilled orders
            if (product.Discontinued == true && productInDB.UnitsOnOrder > 0)
            {
                message = "This Product Cannot Be Discontinued";
                return false;
            }
            else
            {
                return productDAO.UpdateProduct(ref product, ref message);
            }
        }
    }
}
