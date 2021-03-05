using Dapper.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dapper.Infastructure.Models
{
    public class ProductsDto
    {
        public string ProductName { get; set; }
        public int SupplierId { get; set; }
        public int CategoryId { get; set; }
        public string QuantityPerUnit { get; set; }
        public double UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
        public int UnitsOnOrder { get; set; }
        public int ReorderLevel { get; set; }
        public int Discontinued { get; set; }
        public DateTime RowVersion { get; set; }

    }
}
