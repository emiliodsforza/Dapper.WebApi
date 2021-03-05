using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper.Core.Entities;
using Dapper.Core.Models;

namespace Dapper.Application.Interfaces
{
    public interface ISupplierRepository : IGenericRepository<Suppliers>
    {
        Task<IEnumerable<Products>> GetProducts(int id);
        Task<int> AddAsync(SupplierRequest suppliers);
    }
}
