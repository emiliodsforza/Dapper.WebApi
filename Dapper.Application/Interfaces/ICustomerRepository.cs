using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper.Core.Entities;
using Dapper.Core.Models;

namespace Dapper.Application.Interfaces
{
    public interface ICustomerRepository : IGenericRepository<Customers>
    {
        Task<IEnumerable<OrderDetailShort>> GetCustomerOrders(string customerid);
        Task<EmployeeOrdersResponse> GetCustomerOrder(string customerid,int orderid);
    }


}
