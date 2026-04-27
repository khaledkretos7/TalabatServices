using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Spacifications.Productspec
{
    public class ProductSpecParam
    {
        private string? search;
        public string? Search 
        { 
           get {return search; } 
           set { search = value.ToLower();}
        }
        public int PageIndex { get; set; } = 1;
        private int pageSize = MaxPageSize;
        private const int MaxPageSize = 5;
        public int PageSize
        { 
            get { return pageSize; }
            set { pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }
   

        public string? Sort { get; set; }
        public int? BrandId { get; set; }
        public int? CategoryId { get; set; }
    }
}
