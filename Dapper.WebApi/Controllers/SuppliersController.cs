using Dapper.Application.Interfaces;
using Dapper.Core.Entities;
using Dapper.Infastructure.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Swashbuckle.AspNetCore.Annotations;
using Dapper.Core.Models;
using System.Linq;

namespace Dapper.WebApi.Controllers
{
    [SwaggerTag("Create, read, update and delete Suppliers")]
    [Route("api/suppliers")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public SuppliersController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await unitOfWork.Suppliers.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}/products")]
        public async Task<IActionResult> GetProducts(int id)
        {

            var data = await unitOfWork.Suppliers.GetProducts(id);

            if (data == null || data.Count<Products>() == 0)   return NotFound();
                return Ok(mapper.Map<IEnumerable<ProductsDto>>(data));         
        }

        [HttpGet("{id}", Name = "GetSupplierInfo")]
        public async Task<IActionResult> GetById(int id)
        {

          var data = await unitOfWork.Suppliers.GetByIdAsync(id);
            if (data == null) return NotFound();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Add(SupplierRequest suppliers)
        {

           var  supplierEntity = mapper.Map<SupplierRequest>(suppliers);

            var data = await unitOfWork.Suppliers.AddAsync(suppliers);
            var resultEnity = mapper.Map<Suppliers>(suppliers);

            return CreatedAtRoute("GetSupplierInfo",new { id = resultEnity.SupplierId } , suppliers);

        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await unitOfWork.Suppliers.DeleteAsync(id);
            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Suppliers suppliers)
        {
            var data = await unitOfWork.Suppliers.UpdateAsync(suppliers);
            return Ok(data);
        }
    }
}
