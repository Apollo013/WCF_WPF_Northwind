using LINQNorthwind.Models.DataTransferObjects;
using LINQNorthwindLogic;
using LINQNorthwindService.Abstract;
using LINQNorthwindService.FaultExceptions;
using System;
using System.ServiceModel;

namespace LINQNorthwindService.Concrete
{

    public class ProductService : IProductService
    {
        ProductLogic productLogic = new ProductLogic();

        public ProductDTO GetProduct(int id)
        {
            try
            {
                var product = productLogic.GetProduct(id);
                if (product == null)
                {
                    string msg = string.Format("No product found for id {0}", id);
                    string reason = "Product Could Not Be Found.";
                    throw new FaultException<ProductFault>(new ProductFault(msg), reason);
                }
                return product;
            }
            catch (Exception e)
            {
                string msg = e.Message;
                throw new FaultException<ProductFault>(new ProductFault(msg), msg);
            }

        }

        public bool UpdateProduct(ref ProductDTO product, ref string message)
        {
            if (string.IsNullOrEmpty(product.ProductName))
            {
                message = "Product name cannot be empty";
                return false;
            }
            // QuantityPerUnit can't be empty
            else if
            (string.IsNullOrEmpty(product.QuantityPerUnit))
            {
                message = "Quantity cannot be empty";
                return false;
            }
            else
            {
                try
                {
                    return productLogic.UpdateProduct(ref product, ref message);
                }
                catch (Exception e)
                {
                    string msg = e.Message;
                    throw new FaultException<ProductFault>(new ProductFault(msg), msg);
                }
            }
        }
    }
}
