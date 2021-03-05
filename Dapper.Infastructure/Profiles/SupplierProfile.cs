using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Dapper.Core.Entities;
using Dapper.Core.Models;
using Dapper.Infastructure.Models;

namespace Dapper.Infastructure.Profiles
{
    public class SupplierProfile:Profile
    {
        public SupplierProfile()
        {
            CreateMap<Products, ProductsDto>();
            CreateMap<ProductsDto, Products>();
            CreateMap<SupplierRequest, Suppliers>();
            CreateMap<Suppliers, SupplierRequest>();
        }
    }
}
