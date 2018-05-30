using Newtonsoft.Json;
using Common.Data.Model;
using System;
using System.IO;

namespace Common.Client
{
    public class FileHelper
    {
        private static string ShoppingStateFile => string.Format(@"{0}\{1}", Path.GetTempPath(), "ShoppingState");
        private static object objLock = new object();

        public static void SaveIoTClientState(ManagedDevice iotClientState)
        {
            lock (objLock)
            {
                try
                {
                    using (var file = File.CreateText(ShoppingStateFile))
                    {
                        var serializer = new JsonSerializer();
                        serializer.Serialize(file, iotClientState);
                    }
                }
                catch
                {

                }
            }
        }

        public static ManagedDevice ReadIoTClientState()
        {
            lock (objLock)
            {
                ManagedDevice iotClientState = null;

                try
                {
                    if (File.Exists(ShoppingStateFile))
                    {
                        using (var file = File.OpenText(ShoppingStateFile))
                        {
                            var serializer = new JsonSerializer();
                            iotClientState = (ManagedDevice)serializer.Deserialize(file, typeof(ManagedDevice));
                        }
                    }
                }
                catch
                {
                }

                return iotClientState;
            }
        }
    }
}
