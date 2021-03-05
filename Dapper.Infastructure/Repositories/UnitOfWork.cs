using Dapper.Application.Interfaces;
using Dapper.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dapper.Infastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(ICustomerRepository customerRepository,ISupplierRepository supplierRepository,IEmployeeRepository employeeRepository)
        {
            Customers = customerRepository;
            Suppliers = supplierRepository;
            Employees = employeeRepository;
        }

        public ICustomerRepository Customers { get; }

        public ISupplierRepository Suppliers { get; }

        public IEmployeeRepository Employees { get; }
    }
}
