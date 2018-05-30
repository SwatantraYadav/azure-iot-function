using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Common.Data.Model;

namespace fnShopping
{
    public static class RegisterDevice
    {
        [FunctionName("RegisterDevice")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            try
            {
                // Get request body
                var deviceInfo = await req.Content.ReadAsAsync<ManagedDevice>();
                var email = deviceInfo?.Email?.ToLower();
                log.Info($"C# EMAIL: {email}");
                if (email == null)
                    return req.CreateResponse(HttpStatusCode.BadRequest, "noemail");

                var result = await IoTConnector.Instance.RegisterDeviceAsync(email);
                string deviceKey = result.Item1;
                
                log.Info($"C# Registered with deviceKey: {deviceKey} {result.Item2}");
                string token = IoTConnector.Instance.GenerateSecurityToken(email, deviceKey, out StringBuilder sb, 100);
                log.Info($"C# token: {token} {sb}");
                return email == null
                    ? req.CreateResponse(HttpStatusCode.BadRequest, "failedRegister")
                    : req.CreateResponse(HttpStatusCode.OK, token);
            }
            catch (Exception ex)
            {
                log.Error("Failed " + ex);
            }

            return req.CreateResponse(HttpStatusCode.BadRequest, "failedRegister");
        }
    }
}
