using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Data.Model
{
    public class ManagedDevice
    {
        public string Email { get; set; }
        public string IoTToken { get; set; }
        public bool IsRegistered { get; set; }
    }
}
