using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCommon
{
    public class Constants
    {
        public const string RegisterFunctionUri = "https://functionapp20180513100146.azurewebsites.net/api/RegisterDevice?code=XRKboHanR0/VhRCltpdk0EW37slz3fEeRchv6C1vmTzpKVoJH/gHsw==";
        public const string IoTHubConnectionString = "HostName=serverlessiot.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=F6QwJz3anYGeFFmKGIKZmVFVZG9vtYLJZuKUWMCya+0=";
        public const string IoThubHostName = "serverlessiot.azure-devices.net";
        public const int IoTDeviceTimeToLiveInDays = 100;
    }
}
