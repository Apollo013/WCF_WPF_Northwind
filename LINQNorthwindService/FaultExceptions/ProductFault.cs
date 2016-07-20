using System.Runtime.Serialization;

namespace LINQNorthwindService.FaultExceptions
{
    [DataContract]
    public class ProductFault
    {
        [DataMember]
        public string FaultMessage;

        public ProductFault(string message)
        {
            FaultMessage = message;
        }
    }
}
