using eShopSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.ViewModels.Catalog.Products.Manage
{
    public class GetProductPagingRequest : PagedRequestBase
    {
        public string keyWord { get; set; }
        public List<int> CategoryIds { get; set; }
        public object CategoryId { get; set; }
    }
}
