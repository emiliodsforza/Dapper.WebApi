using System;
using System.Collections.Generic;
using System.Text;

namespace Dapper.Core.Models
{
    public class EmployeeOrdersResponse
    {
        public int OrderId { get; set; }
        public string CustomerId { get; set; }
        public string CompanyName { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName {get;set;}
        public DateTime OrderDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public DateTime ShippedDate { get; set; }
        public int ShipVia { get; set; }
        public string Shipper { get; set; }
        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
        public string ShipCity { get; set; }
        public string ShipRegion { get; set; }
        public string ShipPostalCode { get; set; }
        public string ShipCountry { get; set; }
    }
}
