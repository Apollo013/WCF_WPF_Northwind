using LINQNorthwindBDO;
using System;
using System.Linq;

namespace LinqNorthwindDAL
{
    public class ProductDAO
    {

        public ProductBDO GetProduct(int id)
        {
            ProductBDO productBDO = null;

            using (var db = new NorthwindEntities())
            {
                //db.Database.Connection.Open();

                Product product = (from p in db.Products where p.ProductID == id select p).FirstOrDefault();

                if (product != null)
                {
                    productBDO = new ProductBDO()
                    {
                        ProductID = product.ProductID,
                        ProductName = product.ProductName,
                        QuantityPerUnit = product.QuantityPerUnit,
                        UnitPrice = (decimal)product.UnitPrice,
                        UnitsInStock = (int)product.UnitsInStock,
                        ReorderLevel = (int)product.ReorderLevel,
                        UnitsOnOrder = (int)product.UnitsOnOrder,
                        Discontinued = product.Discontinued,
                        RowVersion = product.RowVersion
                    };
                }
            }

            return productBDO;
        }

        public bool UpdateProduct(ref ProductBDO productBDO, ref string message)
        {
            message = "Product Updated Successsfully";
            bool rv = true;

            using (NorthwindEntities db = new NorthwindEntities())
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

                var productID = productBDO.ProductID;

                // Make sure the product ID is a valid value in the database
                Product productInDB = (from p in db.Products where p.ProductID == productID select p).FirstOrDefault();
                if (productInDB == null)
                {
                    rv = false;
                    message = "Product not Found";
                    throw new Exception("No product found with id: " + productID);
                }

                db.Products.Remove(productInDB);

                // update product
                productInDB.ProductName = productBDO.ProductName;
                productInDB.QuantityPerUnit = productBDO.QuantityPerUnit;
                productInDB.UnitPrice = productBDO.UnitPrice;
                productInDB.Discontinued = productBDO.Discontinued;
                productInDB.RowVersion = productBDO.RowVersion;

                db.Products.Attach(productInDB);
                db.Entry(productInDB).State = System.Data.Entity.EntityState.Modified;
                int num = db.SaveChanges();

                // Check for change to a single object.
                if (num != 1)
                {
                    rv = false;
                    message = "Product not Updated";
                }
            }

            return rv;
        }
    }


}
