using Dapper.Application.Interfaces;
using Dapper.Core.Entities;
using Dapper.Infastructure.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dapper.Infastructure.Profiles;
using Dapper.Core.Models;
using System.Data;

namespace Dapper.Infastructure.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly IConfiguration configuration;
        private readonly IMapper _mapper;

        public SupplierRepository(IConfiguration configuration, IMapper mapper)
        {
            this.configuration = configuration;
            this._mapper = mapper;
        }

        public async Task<int> AddAsync(SupplierRequest entity)
        {
           var sproc = "sp_InsertSupplier";

            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                var qparametes = new DynamicParameters();
                qparametes.Add("@CompanyName", entity.CompanyName);
                qparametes.Add("@ContactName", entity.ContactName);
                qparametes.Add("@ContactTitle", entity.ContactTitle);
                qparametes.Add("@Address", entity.Address);
                qparametes.Add("@City", entity.City);
                qparametes.Add("@Region", entity.Region);
                qparametes.Add("@PostalCode", entity.PostalCode);
                qparametes.Add("@Country", entity.Country);
                qparametes.Add("@Phone", entity.Phone);
                qparametes.Add("@Fax", entity.Fax);
                qparametes.Add("@HomePage", entity.HomePage);

                connection.Open();
                var result = await connection.ExecuteAsync(sproc, qparametes, commandType: CommandType.StoredProcedure);
                return result;
              }
        }

        public Task<int> AddAsync(Suppliers entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> DeleteAsync(int id)
        {
            var sql = "DELETE FROM Suppliers where SupplierId = @Id";


            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { Id = id });
                return result;
            }
        }

        public async Task<IReadOnlyList<Suppliers>> GetAllAsync()
        {
            var sql = "SELECT * FROM Suppliers";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Suppliers>(sql);
                return result.ToList();
            }
        }

        public Task<Suppliers> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<Suppliers> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM Suppliers WHERE SupplierId = @Id";

            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryFirstOrDefaultAsync<Suppliers>(sql, new { Id = id });
                return result;
            }
        }

        public  async Task<IEnumerable<Products>> GetProducts(int id)
        {
            var sql = "SELECT [ProductID],[ProductName],[SupplierID],[CategoryID],[QuantityPerUnit],[UnitPrice],[UnitsInStock],[UnitsOnOrder],[ReorderLevel], cast([Discontinued] as int) as [Discontinued],cast([RowVersion] as datetime) as [RowVersion] " +
                              "FROM [dbo].[Products] where SupplierID = @Id";

            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Products>(sql, new { Id = id });
                return result.ToList();
            }
            
        }

        public async Task<int> UpdateAsync(Suppliers entity)
        {
            var sql = "UPDATE Suppliers SET CompanyName = @CompanyName,ContactName = @ContactName,ContactTitle = @ContactTitle, Address = @Address, City = @City, Region = @Region,PostalCode = @PostalCode,Phone = @Phone,Fax = @Fax,HomePage = @HomePage WHERE SupplierId = @Id";

            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, entity);
                return result;
            }
        }
    }
}

