using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryApp.BLL.BaseReponse;
using InventoryApp.DTO.Products;
using Microsoft.AspNetCore.JsonPatch;
namespace InventoryApp.BLL.Products
{
    public interface IProductServiceBLL
    {
        #region v1
        /// <summary>
        /// Retrieves a list of all products.
        /// </summary>
        /// <returns>An asynchronous operation that returns an enumerable of <see cref="ProductDto"/> objects.</returns>
        public Task<IResponse<List<ProductDto>>> GetAllProductsAsync();


        /// <summary>
        /// Adds a new product to the database
        /// </summary>
        /// <param name="productDto">The product information to be added</param>
        /// <returns>The response containing the added product information</returns>
        /// <remarks>Returns error response if validation fails or the product addition fails</remarks>
        public Task<IResponse<ProductDto>> AddProductAsync(ProductInputDto productDto);

        /// <summary>
        /// Updates an existing product with the given ID in the database.
        /// </summary>
        /// <param name="id">The ID of the product to be updated.</param>
        /// <param name="productDto">The updated product information.</param>
        /// <returns>The response containing the updated product information.</returns>
        /// <remarks>Returns error response if validation fails or the product update fails</remarks>
        public Task<IResponse<ProductDto>> UpdateProductAsync(int id, ProductInputDto productDto);

        /// <summary>
        /// Retrieves product information for a given product ID.
        /// </summary>
        /// <param name="productId">The ID of the product to retrieve.</param>
        /// <returns>The response containing the retrieved product information.</returns>
        /// <remarks>Returns error response if product not found</remarks>
        public Task<IResponse<ProductDto>> GetProductByProductId(int productId);

        /// <summary>
        /// Deletes a product from the database based on the provided product ID
        /// </summary>
        /// <param name="productId">The ID of the product to be deleted</param>
        /// <returns>The response containing the status of the product deletion</returns>
        /// <remarks>Returns error response if deletion fails</remarks>
        public Task<IResponse<bool>> DeleteProductByProductId(int productId);
        #endregion v1

        #region v2

        /// <summary>
        /// validate and Adds a new product to the database
        /// </summary>
        /// <param name="productDto">The product information to be added</param>
        /// <returns>The response containing the added product information</returns>
        /// <remarks>Returns error response if validation fails or the product addition fails</remarks>
        public Task<IResponse<ProductDto>> AddProductAsyncV2(ProductInputDto productDto);

        /// <summary>
        /// validate and then Updates an existing product with the given ID in the database.
        /// </summary>
        /// <param name="id">The ID of the product to be updated.</param>
        /// <param name="productDto">The updated product information.</param>
        /// <returns>The response containing the updated product information.</returns>
        /// <remarks>Returns error response if validation fails or the product update fails</remarks>
        public Task<IResponse<ProductDto>> UpdateProductAsyncV2(int id, ProductInputDto productDto);

        // <summary>
        /// validate and then Updates an existing product with the given ID in the database.
        /// </summary>
        /// <param name="id">The ID of the product to be updated.</param>
        /// <param name="productDto">The updated product information.</param>
        /// <returns>The response containing the updated product information.</returns>
        /// <remarks>Returns error response if validation fails or the product update fails</remarks>
        public Task<IResponse<ProductDto>> UpdateProductPatchAsync(int id, ProductInputDto productDto);

        #endregion v2
    }
}
