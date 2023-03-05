using AutoMapper;

using InventoryApp.DTO.Paging;
using InventoryApp.Core.Entities;
using InventoryApp.DTO.Products;

namespace InventoryApp.BLL.Mapping
{
    public class DtoToEntityMappingProfile : Profile
    {
        public DtoToEntityMappingProfile( )
        {
            CreateMap<ProductInputDto,Product >();

        }
    }
}