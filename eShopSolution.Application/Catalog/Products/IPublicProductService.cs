
using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Catalog.Products.Public;
using eShopSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.Catalog.Products
{
    public interface IPublicProductService // Chữ I đầu tên class là Interface
    {
       Task< PagedResult<ProductViewModel>> GetAllByCategoryId(GetProductPagingRequest request);
        
        // Chi danh cho admin
        //int Create(ProductCreateRequest createRequest);
        //int Update(ProductUpdateRequest editRequest);
        //int Delete(int ProductId);
    }
}
