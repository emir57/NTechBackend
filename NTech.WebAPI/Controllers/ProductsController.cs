﻿using Core.Entity.Concrete;
using Microsoft.AspNetCore.Mvc;
using NTech.Business.Abstract;
using NTech.Dto.Concrete;
using NTech.Entity.Concrete;

namespace NTech.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : BaseController<Product, ProductWriteDto, ProductReadDto>
    {
        private readonly IProductService _productService;
        private readonly IImageService _imageService;
        public ProductsController(IProductService baseService, IImageService imageService) : base(baseService)
        {
            _productService = baseService;
            _imageService = imageService;
        }

        //[HttpGet]
        //public async Task<IActionResult> Get()
        //{
        //    return await base.GetListAsync();
        //}

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PaginationFilter paginationFilter)
        {
            var requestUrl = $"{Request.Scheme}://{Request.Host.Value}/";
            var result = await _productService.GetPaginationAsync(paginationFilter, requestUrl);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            return await base.GetByIdAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductWriteDto productWriteDto)
        {
            var image = await _imageService.UploadAsync(Request.Form.Files[0]);
            productWriteDto.ImageId = image.Data.Id;

            return await base.AddAsync(productWriteDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] ProductWriteDto productWriteDto)
        {
            return await base.UpdateAsync(id, productWriteDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            return await base.SoftDeleteAsync(id);
        }
    }
}
