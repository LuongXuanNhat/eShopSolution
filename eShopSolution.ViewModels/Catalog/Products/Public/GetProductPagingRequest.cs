using eShopSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.ViewModels.Catalog.Products.Public
{
    public class GetProductPagingRequest : PagedRequestBase
    {
        public int? CategoryId { get; set; }
    }
}
