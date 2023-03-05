using AutoMapper;
using InventoryApp.Core.Entities;
using InventoryApp.Repositroy.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryApp.DTO.Products;
using InventoryApp.BLL.BaseReponse;
using InventoryApp.BLL;
using InventoryApp.Core.Entities;
using InventoryApp.Core.Infrastructure;
using Microsoft.AspNetCore.Components.Forms;
using InventoryApp.BLL.Validation.products;
using InventoryApp.Repositroy.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.JsonPatch;
namespace InventoryApp.BLL.Products
{
    public class ProductServiceBLL :  BaseBLL, IProductServiceBLL
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public ProductServiceBLL(IUnitOfWork unitOfWork, IRepository<Product> productRepository, IMapper mapper):base(mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        #region v1

        public async Task<IResponse<List<ProductDto>>> GetAllProductsAsync()
        {
            // Create a new response object to encapsulate the result
            var output = new Response<List<ProductDto>>();

            // Call the GetAllListAsync method on the _productRepository to retrieve a list of products
            var products =  _productRepository.GetAllLocalInMemory();

            // If the list of products is null or empty, return a failed response
            if (products == null || !products.Any())
                return output.CreateResponse(MessageCodes.FailedToFetchData);

            // Map the list of products to a list of product data transfer objects using the AutoMapper library
            var productDtos = _mapper.Map<List<ProductDto>>(products);

            // Return a successful response containing the list of product data transfer objects
            return output.CreateResponse(productDtos);
        }


        public async Task<IResponse<ProductDto>> AddProductAsync(ProductInputDto productDto)
        {
            var output = new Response<ProductDto>();
            try
            {
                // Map DTO to domain model and add to repository
                var product = _mapper.Map<Product>(productDto);
                var addedProduct = await _productRepository.AddAsync(product);
                await _unitOfWork.CommitAsync();

                // Map added domain model back to DTO for response
                var addedProductDto = _mapper.Map<ProductDto>(addedProduct);
                return output.CreateResponse(addedProductDto);

            }
            catch (Exception ex)
            {
                return output.CreateResponse(MessageCodes.Failed);
            }
        }

        public async Task<IResponse<ProductDto>> UpdateProductAsync(int id, ProductInputDto productDto)
        {
            var output = new Response<ProductDto>();
            try
            {
                var product =  _productRepository.GetAllLocalInMemory().FirstOrDefault(p => p.Id == id);
                if (product == null)
                {
                    return output.CreateResponse(MessageCodes.NotFound);
                }
                _mapper.Map(productDto, product);
                var updatedProduct =  _productRepository.Update(product);
                await _unitOfWork.CommitAsync();
                var updatedProductDto = _mapper.Map<ProductDto>(updatedProduct);
                return output.CreateResponse(updatedProductDto);
            }
            catch (Exception ex)
            {
                return output.CreateResponse(MessageCodes.Failed);
            }
        }


        public async Task<IResponse<ProductDto>> GetProductByProductId(int productId)
        {
            var output = new Response<ProductDto>();

            try
            {
                var entity = _productRepository.GetAllLocalInMemory().FirstOrDefault(x => x.Id == productId);
                if (entity == null)
                    return output.CreateResponse(MessageCodes.FailedToFetchData);
                var result = _mapper.Map<ProductDto>(entity);
                return output.CreateResponse(result);
            }
            catch (Exception ex)
            {
                return output.CreateResponse(ex);
            }
        }

        public async Task<IResponse<bool>> DeleteProductByProductId(int productId)
        {
            var output = new Response<bool>();

            try
            {
                var entity = _productRepository.GetAllLocalInMemory().FirstOrDefault(x => x.Id == productId);
                if (entity == null)
                    return output.CreateResponse(MessageCodes.FailedToFetchData);
                _productRepository.Delete(entity);
                await _unitOfWork.CommitAsync();
                return output.CreateResponse(true);
            }
            catch (Exception ex)
            {
                return output.CreateResponse(ex);
            }
        }

        #endregion v1

        #region v2
        public async Task<IResponse<ProductDto>> UpdateProductAsyncV2(int id, ProductInputDto productDto)
        {
            var output = new Response<ProductDto>();
            try
            {
                //input validations
                var verifuEmailValidation = await new ProductInputDtoValidator().ValidateAsync(productDto);
                if (!verifuEmailValidation.IsValid)
                {
                    return output.CreateResponse(verifuEmailValidation.Errors);
                }
                var product = _productRepository.GetAllLocalInMemory().FirstOrDefault(p => p.Id == id);
                if (product == null)
                {
                    return output.CreateResponse(MessageCodes.NotFound);
                }
                _mapper.Map(productDto, product);
                var updatedProduct = _productRepository.Update(product);
                await _unitOfWork.CommitAsync();
                var updatedProductDto = _mapper.Map<ProductDto>(updatedProduct);
                return output.CreateResponse(updatedProductDto);
            }
            catch (Exception ex)
            {
                return output.CreateResponse(MessageCodes.Failed);
            }
        }

        public async Task<IResponse<ProductDto>> AddProductAsyncV2(ProductInputDto productDto)
        {
            var output = new Response<ProductDto>();
            try
            {
                //input validations
                var verifuEmailValidation = await new ProductInputDtoValidator().ValidateAsync(productDto);
                if (!verifuEmailValidation.IsValid)
                {
                    return output.CreateResponse(verifuEmailValidation.Errors);
                }
                // Map DTO to domain model and add to repository
                var product = _mapper.Map<Product>(productDto);
                var addedProduct = await _productRepository.AddAsync(product);
                await _unitOfWork.CommitAsync();

                // Map added domain model back to DTO for response
                var addedProductDto = _mapper.Map<ProductDto>(addedProduct);
                return output.CreateResponse(addedProductDto);

            }
            catch (Exception ex)
            {
                return output.CreateResponse(MessageCodes.Failed);
            }
        }


        public async Task<IResponse<ProductDto>> UpdateProductPatchAsync(int id, ProductInputDto productDto)
        {
            var output = new Response<ProductDto>();
            try
            {
              
                //input validations
                var verifuEmailValidation = await new ProductInputDtoValidator().ValidateAsync(productDto);
                if (!verifuEmailValidation.IsValid)
                {
                    return output.CreateResponse(verifuEmailValidation.Errors);
                }
                var product = _productRepository.GetAllLocalInMemory().FirstOrDefault(p => p.Id == id);
                if (product == null)
                {
                    return output.CreateResponse(MessageCodes.NotFound);
                }
                // Map DTO to domain model and add to repository
                product= _mapper.Map(productDto, product);
                var updatedProduct = _productRepository.Update(product);
                await _unitOfWork.CommitAsync();
                var updatedProductDto = _mapper.Map<ProductDto>(updatedProduct);
                return output.CreateResponse(updatedProductDto);
            }
            catch (Exception ex)
            {
                return output.CreateResponse(MessageCodes.Failed);
            }
        }

        #endregion v2

    }

}
