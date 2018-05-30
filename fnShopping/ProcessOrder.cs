using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.ServiceBus.Messaging;

namespace fnShopping
{
    public static class ProcessOrder
    {
        [FunctionName("ProcessOrder")]
        public static void Run([ServiceBusTrigger("serverlesssbqueue2", AccessRights.Manage, Connection = "Endpoint=sb://serverlesssbnamespace.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=/My6rPHz4azTjEiF1kSgguTS6LPF8KPp8rwn7CJbMiA=")]string myQueueItem, TraceWriter log)
        {
            log.Info($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
        }
    }
}
