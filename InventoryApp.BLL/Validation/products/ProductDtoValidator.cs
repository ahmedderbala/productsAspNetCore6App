using InventoryApp.BLL.Validation;
using InventoryApp.DTO.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
namespace InventoryApp.BLL.Validation.products
{
    public class ProductInputDtoValidator : DtoValidationAbstractBase<ProductInputDto>
    {
        public ProductInputDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty();
            RuleFor(x => x.Price)
               .NotNull()
               .NotEmpty().GreaterThan(0);
           
        }

    }
}
