using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Spacifications.Productspec
{
    public class ProductWithCount : BaseSpacification<Product>
    {
        public ProductWithCount(ProductSpecParam productSpecParam) : base
            (p =>
            (!productSpecParam.BrandId.HasValue || p.BrandId == productSpecParam.BrandId.Value)
            &&
            (!productSpecParam.CategoryId.HasValue || p.CategoryId == productSpecParam.CategoryId.Value)
            ){ }
    }
}
