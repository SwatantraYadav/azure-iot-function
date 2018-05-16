// // Copyright (c) Microsoft. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.

using ShoppingCommon.Model;
using ShoppingCommon.Service;
using System.Windows;

namespace PhotoStoreDemo
{
    
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        ManagedDevice DeviceState = null;

        protected override void OnStartup(StartupEventArgs e)
        {
            this.DeviceState = FileHelper.ReadIoTClientState();
            if(DeviceState == null || !DeviceState.IsRegistered)
            {
                StartupUri = new System.Uri("/PhotoStoreDemo;component/Manage.xaml", System.UriKind.Relative);
            }
            else
            {
                StartupUri = new System.Uri("/PhotoStoreDemo;component/MainWindow.xaml", System.UriKind.Relative);
                Application.Current.Properties["State"] = DeviceState;
            }

            

            base.OnStartup(e);
        }
    }
}