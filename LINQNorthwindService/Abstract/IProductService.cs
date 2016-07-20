using LINQNorthwind.Models.DataTransferObjects;
using LINQNorthwindService.FaultExceptions;
using System.ServiceModel;

namespace LINQNorthwindService.Abstract
{
    [ServiceContract]
    public interface IProductService
    {
        [OperationContract]
        [FaultContract(typeof(ProductFault))]
        ProductDTO GetProduct(int id);

        [OperationContract]
        [FaultContract(typeof(ProductFault))]
        bool UpdateProduct(ref ProductDTO product, ref string message);
    }
}
