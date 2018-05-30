using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Client
{
    public class IoTClient
    {
        public IoTClient(string iotToken)
        {
            TryConnect(iotToken);
        }
        private string IoTToken = null;

        DeviceClient iotDeviceClient = null;
        public bool TryConnect(string deviceToken)
        {
            try
            {
                IoTToken = deviceToken.Replace("\"", "");

                iotDeviceClient = DeviceClient.CreateFromConnectionString(IoTToken, Microsoft.Azure.Devices.Client.TransportType.Amqp_WebSocket_Only);
                iotDeviceClient.OperationTimeoutInMilliseconds = 10 * 1000;

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception {ex}");
                return false;
            }
        }

        public async Task SendTelemetryDataAsync<T>(T data, int retry)
        {
            try
            {
                var messageString = JsonConvert.SerializeObject(data);
                var message = new Message(Encoding.ASCII.GetBytes(messageString));

                await iotDeviceClient.SendEventAsync(message);
            }
            catch(Exception ex)
            {
                Console.Write($"SendDataToCloudAsync {ex}");
                if (retry > 0)
                {
                    if (TryConnect(IoTToken))
                    {
                        await SendTelemetryDataAsync(data, --retry);
                    }
                }
            }
        }

        public async Task SendStorageDataAsync<T>(T data, int retry)
        {
            try
            {
 
                var messageString = JsonConvert.SerializeObject(data);
                var message = new Message(Encoding.ASCII.GetBytes(messageString));
                
                message.Properties.Add("level", "storage");
                await iotDeviceClient.SendEventAsync(message);
            }
            catch (Exception ex)
            {
                Console.Write($"SendStorageDataAsync {ex}");
                if (retry > 0)
                {
                    if (TryConnect(IoTToken))
                    {
                        await SendStorageDataAsync(data, --retry);
                    }
                }
            }
        }

        public async Task SendDataToServiceBusAsync<T>(T data, int retry)
        {
            try
            {
                var messageString = JsonConvert.SerializeObject(data);
                var message = new Message(Encoding.ASCII.GetBytes(messageString));

                message.Properties.Add("level", "input");
                await iotDeviceClient.SendEventAsync(message);
            }
            catch (Exception ex)
            {
                Console.Write($"SendDataToServiceBusAsync {ex}");
                if (retry > 0)
                {
                    if (TryConnect(IoTToken))
                    {
                        await SendDataToServiceBusAsync(data, --retry);
                    }
                }
            }
        }
    }
}
