using AutoMapper;
using InventoryApp.DTO.Products;
using Microsoft.AspNetCore.JsonPatch;
namespace InventoryApp.BLL.Mapping
{
    public class DtoMappingProfile : Profile
    {
        public DtoMappingProfile( )
        {
            CreateMap<JsonPatchDocument<ProductInputDto>, ProductInputDto>();
            CreateMap<ProductDto,ProductInputDto >();
        }
    }
}