using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Common.Data;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;

namespace fnShopping
{
    public static class LogicAppTrigger
    {
        [FunctionName("LogicAppTrigger")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");
            var message = await req.Content?.ReadAsStringAsync();
            // parse query parameter
            log.Info("Message content: " + message);

            if (message == null)
            {
                // Get request body
                return req.CreateResponse(HttpStatusCode.BadRequest, "Please pass a name on the query string or in the request body");
            }

            var messageObj = JsonConvert.DeserializeObject<LogicAppMessage>(message);
            
            var device = messageObj.Properties.iothubconnectiondeviceid;
            log.Info("Device 1: " + device);
            if (string.IsNullOrEmpty(device))
            {
                int start = message.IndexOf("iothub-connection-device-id") + "iothub-connection-device-id".Length + 3;
                int end = message.IndexOf("iothub-connection-auth-method") - 3;
                int length = end - start + 1;
                device = message.Substring(start, length);
            }

            log.Info("Device 2: " + device);

            var actualMessageBase64 = messageObj.ContentData;
            byte[] buffer = Convert.FromBase64String(actualMessageBase64);
            string converted = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
            log.Info("Message content data: " + converted);

            try
            {
                var parent = (List<PrintBaseServer>)JsonConvert.DeserializeObject(converted, typeof(List<PrintBaseServer>));

                var table = await TableStorage.CreateTableAsync("PrintTable", log);
                foreach (var data in parent)
                {
                    log.Info($"Inserting {device} {data.Photo} {data.Quantity} ");
                    var saved = await TableStorage.InsertPrintAsync(table, data, device, log);
                    log.Info("saved content " + saved);
                }
            }
            catch (Exception ex)
            {
                log.Error("Failed save " + ex.ToString());
            }
            return message == null
                ? req.CreateResponse(HttpStatusCode.BadRequest, "Please pass a name on the query string or in the request body")
                : req.CreateResponse(HttpStatusCode.OK, "Hello " + converted);
        }

    }

}
