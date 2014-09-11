using System.ServiceModel;

namespace MyServices
{
    [ServiceContract]
    public interface IMyService
    {
        [OperationContract]
        int GetAnswer();
    }
}
