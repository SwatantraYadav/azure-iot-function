using IoTHubTrigger = Microsoft.Azure.WebJobs.ServiceBus.EventHubTriggerAttribute;

using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.ServiceBus.Messaging;
using System.Text;
using System.Net.Http;

namespace fnShopping
{
    public static class ProcessIoTStep1
    {
        private static HttpClient client = new HttpClient();

        [FunctionName("ProcessIoTStep1")]
        public static void Run([IoTHubTrigger("messages/events", Connection = "HostName=serverlessiot.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=F6QwJz3anYGeFFmKGIKZmVFVZG9vtYLJZuKUWMCya+0=")]EventData message, TraceWriter log)
        {
            log.Info($"C# IoT Hub trigger function processed a message: {Encoding.UTF8.GetString(message.GetBytes())}");
        }
    }
}