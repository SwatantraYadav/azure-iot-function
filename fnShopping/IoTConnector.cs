using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Common.Exceptions;
using Microsoft.Azure.Devices.Common.Security;
using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCommon
{
    public class IoTConnector : IDisposable
    {
        #region Fields
        private ServiceClient serviceClient;

        private RegistryManager registryManager;
        private static object objLock = new object();
        #endregion


        private static IoTConnector instance;
        public static IoTConnector Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (objLock)
                    {
                        if (instance == null)
                        {
                            instance = new IoTConnector();
                        }
                    }
                }

                return instance;
            }
        }
        private IoTConnector()
        {
            this.registryManager = RegistryManager.CreateFromConnectionString(Constants.IoTHubConnectionString);
            this.serviceClient = ServiceClient.CreateFromConnectionString(Constants.IoTHubConnectionString);
        }

        #region register
        public async Task<Tuple<string, string>> RegisterDeviceAsync(string deviceId)
        {
            if(string.IsNullOrEmpty(deviceId))
            {
                return null;
            }

            string deviceKey = null;
            string err = null;
            try
            {
                deviceId = deviceId.ToUpperInvariant();
                var device = await registryManager.AddDeviceAsync(new Device(deviceId));
                deviceKey = device.Authentication.SymmetricKey.PrimaryKey;
            }
            catch (DeviceAlreadyExistsException e)
            {
                var device = await registryManager.GetDeviceAsync(deviceId);
                deviceKey = device.Authentication.SymmetricKey.PrimaryKey;
                err = "Already exists" + e.ToString();
            }
            catch (Exception ex)
            {
                var device = await registryManager.GetDeviceAsync(deviceId);
                deviceKey = device?.Authentication?.SymmetricKey?.PrimaryKey;
                err = "Failed to register " + ex.ToString();
            }

            return new Tuple<string, string>(deviceKey, err);
        }

        public async Task<string> GetKeyIfRegisteredAsync(string deviceId)
        {
            string deviceKey = null;
            try
            {
                var device = await registryManager.GetDeviceAsync(deviceId);
                deviceKey = device?.Authentication.SymmetricKey.PrimaryKey;
            }
            catch (Exception)
            {
                // not an error
            }
            return deviceKey;
        }

        public string GenerateSecurityToken(string deviceId, string deviceKey, out StringBuilder error, int timeToLiveInDays)
        {
            error = new StringBuilder();

            string sasToken = null;
            try
            {
                var sasBuilder = new SharedAccessSignatureBuilder()
                {
                    Key = deviceKey,
                    Target = String.Format("{0}/devices/{1}", Constants.IoThubHostName, WebUtility.UrlEncode(deviceId)),
                    TimeToLive = TimeSpan.FromDays(Constants.IoTDeviceTimeToLiveInDays)
                };

                sasToken = sasBuilder.ToSignature();
            }
            catch (Exception ex)
            {
                error.AppendLine($"Failed to generated sas token for device: {deviceId} due to exception: {ex.Message}");
                return null;
            }

            return String.Format("HostName={0};DeviceId={1};SharedAccessSignature={2}", Constants.IoThubHostName, deviceId, sasToken);
        }

        #endregion

        public void Dispose()
        {
        }
    }
}
