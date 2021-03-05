using Dapper.Core.Models;
using Dapper.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.Application.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employees>
    {
        Task<IEnumerable<EmployeeOrdersResponse>> GetOrdersByEmployee(int id);
    }
}
