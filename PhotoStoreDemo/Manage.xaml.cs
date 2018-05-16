using ShoppingCommon;
using ShoppingCommon.Model;
using ShoppingCommon.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PhotoStoreDemo
{
    /// <summary>
    /// Interaction logic for Manage.xaml
    /// </summary>
    public partial class Manage : Window
    {
        public Manage()
        {
            InitializeComponent();
        }

        private async void Register_Click(object sender, RoutedEventArgs e)
        {
            var email = this.txtEmail.Text;
            string pattern = @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
                             + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				                        [0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
                             + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				                        [0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
                             + @"([a-zA-Z0-9]+[\w-]+\.)+[a-zA-Z]{1}[a-zA-Z0-9-]{1,23})$";
            Regex reg = new Regex(pattern);
            if(!reg.Match(email).Success)
            {
                MessageBox.Show("Invalid email");
                return;
            }

            ManagedDevice device = new ManagedDevice() {
                Email = email
            };

            var token = await HttpHelperService.Instance.RegisterDeviceAsync(device);

            if(token != null)
            {
                device.IoTToken = token;
                device.IsRegistered = true;
                Application.Current.Properties["State"] = device;
                FileHelper.SaveIoTClientState(device);
                Application.Current.MainWindow = new MainWindow();
                Application.Current.MainWindow.Show();
                this.Close();
            }
        }

    }
}
