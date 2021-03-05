using Dapper.Application.Interfaces;
using Dapper.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dapper.WebApi.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public CustomersController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult>GetAll()
        {
            var data = await unitOfWork.Customers.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async  Task<IActionResult> GetById(string id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var data = await unitOfWork.Customers.GetByIdAsync(id);
            if (data == null) return NotFound();
            return Ok(data);
        }

        [HttpGet("{customerid}/OrderDetails")]
        public async Task<IActionResult>GetCustomerOrders(string customerid)
        {
            if(customerid == null)
            {
                return NotFound();
            }

            var data = await unitOfWork.Customers.GetCustomerOrders(customerid);
            return Ok(data);
        }

        [HttpGet("{customerid}/OrderDetails/{orderid}")]
        public async Task<IActionResult> GetCustomerOrder(string customerid, int orderid)
        {
            if (customerid == null)
            {
                return NotFound();
            }

            var data = await unitOfWork.Customers.GetCustomerOrder(customerid,orderid);
            if (data == null) return NotFound();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Customers customer)
        {
            var data = await unitOfWork.Customers.AddAsync(customer);
            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var data = await unitOfWork.Customers.DeleteAsync(id);
            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Customers customers)
        {
            var data = await unitOfWork.Customers.UpdateAsync(customers);
            return Ok(data);
        }

    }
}
