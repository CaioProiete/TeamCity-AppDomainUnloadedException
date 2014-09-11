using System.ServiceModel;

namespace MyServices
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class MyService : IMyService
    {
        public int GetAnswer()
        {
            return 42;
        }
    }
}
