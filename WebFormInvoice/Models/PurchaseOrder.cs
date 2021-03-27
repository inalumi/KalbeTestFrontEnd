using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebFormInvoice.Models
{
    public class PurchaseOrder
    {
        public int ID { get; set; }
        public string PONo { get; set; }
        public int Amount { get; set; }
    }
}
