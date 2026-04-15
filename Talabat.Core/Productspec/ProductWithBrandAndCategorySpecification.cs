using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Spacifications;

namespace Talabat.Core.Product_spec
{
    public class ProductWithBrandAndCategorySpecification : BaseSpacification<Product>
    {
        public ProductWithBrandAndCategorySpecification(int id) : base(p => p.Id == id)
        {
            includes.Add(p => p.ProductBrand);
            includes.Add(p => p.ProductCategory);
        }
        public ProductWithBrandAndCategorySpecification(string? sort)
        {
            includes.Add(p => p.ProductBrand);
            includes.Add(p => p.ProductCategory);
            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
                {
                    case "PriceAsc":
                        OrderBy = p => p.Price;
                        break;

                    case "PriceDesc":
                        OrderByDesc = p => p.Price;
                        break;

                    default:
                        OrderBy = p => p.Name;
                        break;
                }
            }
            else {
                OrderBy=p => p.Name;
            }
        }
    }
}