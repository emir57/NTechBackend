﻿using Microsoft.AspNetCore.Mvc;
using NTech.Business.Abstract;
using NTech.Dto.Concrete;
using NTech.Entity.Concrete;

namespace NTech.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : BaseController<Product, ProductWriteDto, ProductReadDto>
    {
        public ProductsController(IProductService baseService) : base(baseService)
        {
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return await base.GetListAsync();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductWriteDto productWriteDto)
        {
            return await base.AddAsync(productWriteDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            return await base.SoftDeleteAsync(id);
        }
    }
}
