using Dapper.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Dapper.Core.Entities;
using System.Data.SqlClient;
using System.Linq;
using System.Data;
using Dapper.Core.Models;

namespace Dapper.Infastructure.Repositories
{
    class EmployeeRepository : IEmployeeRepository
    {
        private readonly IConfiguration configuration;

        public EmployeeRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<int> AddAsync(Employees entity)
        {
            var spname = "dbo.sp_InsertEmployee";


            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                var qparametes = new DynamicParameters();
                qparametes.Add("@LastName", entity.LastName);
                qparametes.Add("@FirstName", entity.FirstName);
                qparametes.Add("@Title", entity.Title);
                qparametes.Add("@TitleOfCourtesy", entity.TitleOfCourtesy);
                qparametes.Add("@BirthDate", entity.BirthDate);
                qparametes.Add("@HireDate", entity.HireDate);
                qparametes.Add("@Address", entity.Address);
                qparametes.Add("@City", entity.City);
                qparametes.Add("@Region", entity.Region);
                qparametes.Add("@PostalCode", entity.PostalCode);
                qparametes.Add("@Country", entity.Country);
                qparametes.Add("@HomePhone", entity.HomePhone);
                qparametes.Add("@Extension", entity.Extension);
                qparametes.Add("@Photo", entity.Photo);
                qparametes.Add("@Notes", entity.Notes);
                qparametes.Add("@ReportsTo", entity.ReportsTo);
                qparametes.Add("@PhotoPath", entity.PhotoPath);
                connection.Open();

                var result = await connection.ExecuteAsync(spname, qparametes, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public Task<int> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> DeleteAsync(int id)
        {
            var sql = "DELETE FROM Employees where EmployeeId = @Id";


            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { Id = id });
                return result;
            }
        }

        public async Task<IReadOnlyList<Employees>> GetAllAsync()
        {
            var sproc = "[dbo].[sp_GetAllEmployees]";

            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Employees>(sproc,commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        public Task<Employees> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<Employees> GetByIdAsync(int id)
        {
            var sql = "sp_GetEmployee_ByEmployeeId";

            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                var qparametes = new DynamicParameters();
                qparametes.Add("@EmployeeID", id);
                connection.Open();
                var result = await connection.QueryFirstOrDefaultAsync<Employees>(sql, qparametes,commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<IEnumerable<EmployeeOrdersResponse>> GetOrdersByEmployee(int id)
        {

            var sproc = "sp_GetAllOrdersDetails_ByEmployee";

            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var qparametes = new DynamicParameters();
                qparametes.Add("@EmployeeID", id);
                var result = await connection.QueryAsync<EmployeeOrdersResponse>(sproc, qparametes,commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }


        public Task<int> UpdateAsync(Employees entity)
        {
            throw new NotImplementedException();
        }
    }
}

