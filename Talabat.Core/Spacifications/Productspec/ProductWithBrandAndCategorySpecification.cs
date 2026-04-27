using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Spacifications;
using Talabat.Core.Spacifications.Productspec;

namespace Talabat.Core.Product_spec
{
    public class ProductWithBrandAndCategorySpecification : BaseSpacification<Product>
    {
        public ProductWithBrandAndCategorySpecification(int id) : base(p => p.Id == id)
        {
            includes.Add(p => p.ProductBrand);
            includes.Add(p => p.ProductCategory);
        }
        public ProductWithBrandAndCategorySpecification(ProductSpecParam productSpecParam)
            : base(p =>
                 (string.IsNullOrEmpty(productSpecParam.Search)||p.Name.Contains(productSpecParam.Search))
                 &&
                 (!productSpecParam.BrandId.HasValue || p.BrandId == productSpecParam.BrandId.Value)
                 &&
                 (!productSpecParam.CategoryId.HasValue || p.CategoryId == productSpecParam.CategoryId.Value)
                 )
        {
            includes.Add(p => p.ProductBrand);
            includes.Add(p => p.ProductCategory);
            if (!string.IsNullOrEmpty(productSpecParam.Sort))
            {
                switch (productSpecParam.Sort)
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
            else
            {
                OrderBy = p => p.Name;
            }
            ApplyPagination((productSpecParam.PageIndex - 1) * productSpecParam.PageSize, productSpecParam.PageSize);
        }
    }
}