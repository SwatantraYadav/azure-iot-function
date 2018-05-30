using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fnShopping
{
    public class PrintEntity : TableEntity
    {
        public PrintEntity(string shortdate, string email)
        {
            PartitionKey = shortdate;
            RowKey = email;
        }

        public string Email { get; set; }

        public string Photo { get; set; }

        public float Cost { get; set; }
        public int Quantity { get; set; }

        public override string ToString()
        {
            return $"Device: {Email} File: {Photo} ${Cost} Q: {Quantity}";
        }
    }
}
