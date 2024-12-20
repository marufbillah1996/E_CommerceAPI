using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class ProductSpecification : BaseSpecification <Product>
    {
        public ProductSpecification(ProductSpecParams specParams): base(x=> 
                (specParams.Brands.Count == 0 || specParams.Brands.Contains(x.Brand)) && 
                (specParams.Types.Count == 0 || specParams.Types.Contains(x.Type))
        )
        {
            switch(specParams.Sort)
            {
                case "priceAsc": 
                    AddOrderBy(x=>x.Price);
                    break;
                case "priceDesc":
                    AddOrderByDescinding(x=>x.Price);
                    break;
                default:
                    AddOrderBy(x => x.Name);
                    break;
            }
            //pagination
            ApplyPaging(specParams.PageSize *(specParams.PageIndex - 1),specParams.PageSize);
        }
    }
}
