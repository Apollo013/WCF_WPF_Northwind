using LINQNorthwind.Models.DomainEntities;
using System.Data.Entity;

namespace LinqNorthwindDAL
{
    public class NorthWindContext : DbContext
    {
        public NorthWindContext() : base("DefaultConnection")
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        public virtual DbSet<Product> Products { get; set; }
    }
}
