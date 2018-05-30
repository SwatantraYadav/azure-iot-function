using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Data
{
    public class LogicAppData
    {
        public PrintBaseServer[] Property1 { get; set; }
    }

    public class PrintBaseServer
    {
        public string Photo { get; set; }
        public Printtype PrintType { get; set; }
        public int Quantity { get; set; }
    }

    public class Printtype
    {
        public string Description { get; set; }
        public float Cost { get; set; }
    }

}
