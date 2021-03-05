using System;
using System.Collections.Generic;
using System.Text;

namespace Dapper.Core.Entities
{
    public class OrderDetailShort
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
    }
}
