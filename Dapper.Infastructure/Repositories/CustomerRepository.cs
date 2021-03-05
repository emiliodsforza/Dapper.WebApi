using Dapper.Application.Interfaces;
using Dapper.Core.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Infastructure.Models;
using System.Data;
using Dapper.Core.Models;
using AutoMapper;

namespace Dapper.Infastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IConfiguration configuration;
        private readonly IMapper mapper;

        public CustomerRepository(IConfiguration configuration, IMapper mapper)
        {
            this.configuration = configuration;
            this.mapper = mapper;
        }

        public async Task<int> AddAsync(Customers entity)
        {
            var sql = "INSERT INTO dbo.Customers (CustomerId,CompanyName,ContactName,ContactTitle,Address,City,Region,PostalCode,Country,Phone,Fax)  VALUES (@CustomerId,@CompanyName,@ContactName,@ContactTitle,@Address,@City,@Region,@PostalCode,@Country,@Phone,@fax)";

            using(var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, entity);
                return result;
            }
        }

        public async Task<int> DeleteAsync(string id)
        {
            var sql = "DELETE FROM Customers where CustomerId = @Id";


            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { Id = id });
                return result;
            }
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<Customers>> GetAllAsync()
        {
            var sql = "SELECT * FROM Customers";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Customers>(sql);
                return result.ToList();
            }
        }

        public  async Task<Customers> GetByIdAsync(string id)
        {
            var sql = "SELECT * FROM Customers WHERE CustomerId = @Id";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryFirstOrDefaultAsync<Customers>(sql, new { Id = id });
                return result;
            }
        }

        public Task<Customers> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<OrderDetailShort>> GetCustomerOrders(string customerid)
        {
            var spname = "[dbo].[getOrdersByCustomerID]";


            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                var qparametes = new DynamicParameters();
                qparametes.Add("@CustomerID", customerid);
                connection.Open();

                var result = await connection.QueryAsync<OrderDetailShort>(spname, qparametes, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        public async Task<EmployeeOrdersResponse> GetCustomerOrder(string customerid, int orderId)
        {
            var spname = "[dbo].[sp_GetCustomerOrdersDetails_ByCustAndOrderD]";


            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                var qparametes = new DynamicParameters();
                qparametes.Add("@CustomerID", customerid);
                qparametes.Add("@OrderId", orderId);
                connection.Open();

                var result = await connection.QueryFirstOrDefaultAsync<EmployeeOrdersResponse>(spname, qparametes, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<int> UpdateAsync(Customers entity)
        {

            var sql = "UPDATE Customers SET CompanyName = @CompanyName,ContactName = @ContactName,ContactTitle = @ContactTitle, Address = @Address, City = @City, Region = @Region,PostalCode = @PostalCode,Phone = @Phone,Fax = @Fax WHERE CustomerID = @Id";

            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, entity);
                return result;
            }
        }
    }
}
