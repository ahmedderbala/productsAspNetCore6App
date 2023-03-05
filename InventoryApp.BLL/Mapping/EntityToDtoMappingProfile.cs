using AutoMapper;

using InventoryApp.BLL.Constants;
using InventoryApp.Core.Entities;
using InventoryApp.DTO;
using InventoryApp.DTO.Translation;
using System.Runtime.Serialization;
using Azure.Core;
using InventoryApp.Core.Infrastructure;
using InventoryApp.Core.Entities;
using InventoryApp.DTO.Products;

namespace InventoryApp.BLL.Mapping
{
    public class EntityToDtoMappingProfile : Profile
    {
        public EntityToDtoMappingProfile( )
        {
            CreateMap<Product, ProductDto>();
            CreateMap<Product, ProductInputDto>();

        }
    }
}