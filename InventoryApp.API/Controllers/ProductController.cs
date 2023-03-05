using AutoMapper;
using InventoryApp.BLL.Products;
using InventoryApp.DTO.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.JsonPatch;
using System;

namespace InventoryApp.API.Controllers
{
    [ApiController]
    [Route("api/products")]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    public class ProductController : ControllerBase
    {

        private readonly IProductServiceBLL _productService;
        private readonly IMapper _mapper;

        public ProductController(IProductServiceBLL productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet("GetAllProductsAsync")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult> GetAllProductsAsync()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpPost("AddProduct")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> AddProduct(ProductInputDto productDto)
        {
            var result = await _productService.AddProductAsync(productDto);
            return Ok(result);
        }



        [HttpPut("Update/{id}")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<ProductDto>> UpdateProductAsync(int id, ProductInputDto productDto)
        {
            var result = await _productService.UpdateProductAsync(id, productDto);
            return Ok(result);
        }


        [HttpDelete("Delete/{id}")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult> DeleteProductByProductId(int id)
        {
            var products = await _productService.DeleteProductByProductId(id);
            return Ok(products);
        }

        [HttpGet("GetProductByProductId")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult> GetProductByProductId(int productId)
        {
            var products = await _productService.GetProductByProductId(productId);
            return Ok(products);
        }


        [HttpPost("AddProductV2")]
        [MapToApiVersion("2.0")]
        public async Task<IActionResult> AddProductAsyncV2(ProductInputDto productDto)
        {
            var result = await _productService.AddProductAsyncV2(productDto);
            return Ok(result);
        }



        [HttpPut("UpdateV2/{id}")]
        [MapToApiVersion("2.0")]
        public async Task<ActionResult<ProductDto>> UpdateProductAsyncV2(int id, ProductInputDto productDto)
        {
            var result = await _productService.UpdateProductAsyncV2(id, productDto);
            return Ok(result);
        }

        [HttpPatch("UpdateByPatchV2/{id}")]
        [MapToApiVersion("2.0")]
        public async Task<ActionResult> UpdateProductPatchAsync(int id, [FromBody] JsonPatchDocument<ProductInputDto> patchDoc)
        {
            // Retrieve the person from the database using the provided ID
            var productResult =await _productService.GetProductByProductId(id);

            // Apply the patch to the DTO
            var productInputDto = _mapper.Map<ProductInputDto>(productResult.Data);
            // Apply patch to the product input DTO
            patchDoc.ApplyTo(productInputDto, (error) =>
            {
                ModelState.AddModelError("JsonPatch", error.ErrorMessage);
            });

            // Check if there are any errors
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productService.UpdateProductPatchAsync(id, productInputDto);
            return Ok(result);
        }


    }
}
