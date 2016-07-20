using LINQNorthwind.Models.BusinessDomainObjects;
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

            ProductBDO productBDO = null;
            try
            {
                productBDO = productLogic.GetProduct(id);
            }
            catch (Exception e)
            {
                string msg = e.Message;
                string reason = e.Message; //"Get Product Exception";
                throw new FaultException<ProductFault>(new ProductFault(msg), reason);
            }

            if (productBDO == null)
            {
                string msg = string.Format("No product found for id {0}", id);
                string reason = "Product Could Not Be Found.";
                throw new FaultException<ProductFault>(new ProductFault(msg), reason);
            }

            ProductDTO productDTO = new ProductDTO();
            TranslateProductBDOToProductDTO(productBDO, productDTO);
            return productDTO;
        }

        public bool UpdateProduct(ref ProductDTO product, ref string message)
        {
            bool rv = true;
            /*
            // first check to see if it is a valid price
            if (product.UnitPrice <= 0)
            {
                message = "Price cannot be <= 0";
                rv = false;
            }
            // ProductName can't be empty
            else 
            */
            if (string.IsNullOrEmpty(product.ProductName))
            {
                message = "Product name cannot be empty";
                rv = false;
            }
            // QuantityPerUnit can't be empty
            else if
            (string.IsNullOrEmpty(product.QuantityPerUnit))
            {
                message = "Quantity cannot be empty";
                rv = false;
            }
            else
            {
                try
                {
                    var productBDO = new ProductBDO();
                    TranslateProductDTOToProductBDO(product, productBDO);
                    rv = productLogic.UpdateProduct(ref productBDO, ref message);
                    product.RowVersion = productBDO.RowVersion;
                }
                catch (Exception e)
                {
                    string msg = e.Message;
                    throw new FaultException<ProductFault>(new ProductFault(msg), msg);
                }
            }

            return rv;
        }

        private void TranslateProductBDOToProductDTO(ProductBDO bdo, ProductDTO dto)
        {
            dto.ProductID = bdo.ProductID;
            dto.ProductName = bdo.ProductName;
            dto.QuantityPerUnit = bdo.QuantityPerUnit;
            dto.UnitPrice = bdo.UnitPrice;
            dto.Discontinued = bdo.Discontinued;
            dto.RowVersion = bdo.RowVersion;
        }

        private void TranslateProductDTOToProductBDO(ProductDTO dto, ProductBDO bdo)
        {
            bdo.ProductID = dto.ProductID;
            bdo.ProductName = dto.ProductName;
            bdo.QuantityPerUnit = dto.QuantityPerUnit;
            bdo.UnitPrice = dto.UnitPrice;
            bdo.Discontinued = dto.Discontinued;
            bdo.RowVersion = dto.RowVersion;
        }

    }


}
