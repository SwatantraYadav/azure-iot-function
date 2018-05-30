using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Common.Data;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using Microsoft.Azure.Storage;
using Microsoft.Azure.CosmosDB.Table;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace fnShopping
{
    public static class LogicAppTrigger
    {
        [FunctionName("LogicAppTrigger")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");
            var message = req.Content?.ReadAsStringAsync().Result;
            // parse query parameter

            
            if (message == null)
            {
                // Get request body
                return req.CreateResponse(HttpStatusCode.BadRequest, "Please pass a name on the query string or in the request body");
            }

            var messageObj = JsonConvert.DeserializeObject<LogicAppMessage>(message);
            var device = messageObj?.Properties.iothubconnectiondeviceid;
            var actualMessageBase64 = messageObj.ContentData;
            byte[] buffer = Convert.FromBase64String(actualMessageBase64);
            string converted = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
            log.Info("Message content " + converted);

            var data = JsonConvert.DeserializeObject<LogicAppData>(converted);


            var connectionString = "DefaultEndpointsProtocol=https;AccountName=dbserverlesscosmos;AccountKey=66sGoaHZlOVR4YNqvipbOurp55fINYHlm0r4RtqmGPeDISJXJflmOaYSbPwxNPrM2ELwPE9kowNoFVR7sFUWDA==;TableEndpoint=https://dbserverlesscosmos.table.cosmosdb.azure.com:443/;";
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            tableClient.GetTableReference("")
            return message == null
                ? req.CreateResponse(HttpStatusCode.BadRequest, "Please pass a name on the query string or in the request body")
                : req.CreateResponse(HttpStatusCode.OK, "Hello " + converted);
        }
    }
}
