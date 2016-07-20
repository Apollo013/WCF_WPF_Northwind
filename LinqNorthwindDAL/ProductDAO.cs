using LINQNorthwind.Models.DataTransferObjects;
using LINQNorthwind.Models.DomainEntities;
using System;
using System.Linq;

namespace LinqNorthwindDAL
{
    /// <summary>
    /// Product Data Access Object
    /// </summary>
    public class ProductDAO
    {
        NorthWindContext ctx = new NorthWindContext();

        /// <summary>
        /// Gets a Product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Product GetProduct(int id)
        {
            return ctx.Products.Where(p => p.ProductID == id).FirstOrDefault();
        }

        /// <summary>
        /// Updates Product Table
        /// </summary>
        /// <param name="productBDO"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool UpdateProduct(ref ProductDTO product, ref string message)
        {

            // We have to save productID in a new variable and then use it in the LINQ query. 
            // Otherwise, we will get an error saying Cannot use ref or out parameter 'productBDO' inside an anonymous method, 
            // lambda expression, or query expression

            // If Remove and Attach are not called, RowVersion from the database (not from the client) 
            // will be used when submitting to database, even though we have updated its value before 
            // submitting to the database. An update will always succeed, but without concurrency control.

            // Also, If Remove is not called and we call the Attach method, we will get an error saying 
            // the object cannot be attached because it is already in the object context.

            // If the object state is not set to be Modified, Entity Framework will not honor our changes 
            // to the entity object and we will not be able to save any change to the database.

            // Make sure the product ID is a valid value in the database
            Product productInDB = GetProduct(product.ProductID);
            if (productInDB == null)
            {
                message = "Product not Found";
                throw new Exception("No product found with id: " + product.ProductID);
            }

            ctx.Products.Remove(productInDB);

            // update product
            productInDB.ProductName = product.ProductName;
            productInDB.QuantityPerUnit = product.QuantityPerUnit;
            productInDB.UnitPrice = product.UnitPrice;
            productInDB.Discontinued = product.Discontinued;
            productInDB.RowVersion = product.RowVersion;

            ctx.Products.Attach(productInDB);
            ctx.Entry(productInDB).State = System.Data.Entity.EntityState.Modified;
            int num = ctx.SaveChanges();

            return num == 1;
        }
    }
}
