using System;
using System.Collections.Generic;
using System.Text;

namespace Dapper.Application.Interfaces
{
    public interface IUnitOfWork 
    {
        ICustomerRepository Customers { get; }
        ISupplierRepository Suppliers { get; }
        IEmployeeRepository Employees { get; }
    }
}
