﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Data
{
    public class Constants
    {
        public const string RegisterFunctionUri = "https://functionappserverless.azurewebsites.net/api/RegisterDevice?code=HX7nmkwMcHU0dMWFiF6JmaGLYf0rP7Pa997DILh8lYSfXnHk8xKXPQ==";
        public const string IoTHubConnectionString = "HostName=serverlessiot.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=F6QwJz3anYGeFFmKGIKZmVFVZG9vtYLJZuKUWMCya+0=";
        public const string IoThubHostName = "serverlessiot.azure-devices.net";
        public const int IoTDeviceTimeToLiveInDays = 100;

        public const string DatabaseConnectionString = "DefaultEndpointsProtocol=https;AccountName=dbserverlesscosmos;AccountKey=66sGoaHZlOVR4YNqvipbOurp55fINYHlm0r4RtqmGPeDISJXJflmOaYSbPwxNPrM2ELwPE9kowNoFVR7sFUWDA==;TableEndpoint=https://dbserverlesscosmos.table.cosmosdb.azure.com:443/;";
    }
}
