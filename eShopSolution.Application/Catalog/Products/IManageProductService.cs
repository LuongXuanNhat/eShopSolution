
using eShopSolution.ViewModels.Catalog.ProductImages;
using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.Catalog.Products
{
    public interface IManageProductService // Chữ I đầu tên class là Interface
    {
        Task<int> Create(ProductCreateRequest createRequest);

        Task<int> Update(ProductUpdateRequest editRequest);

        Task<int> Delete(int ProductId);

        Task<ProductViewModel> GetById(int productId,string languageId);

        Task<bool> UpdatePrice(int ProductId, decimal newPrice);
        Task<bool> UpdateStock(int ProductId, int addedQuantity);
        Task AddViewCount(int productId);
        //Task<List<ProductViewModel>> GetAll();
        Task<PagedResult<ProductViewModel>> GetAllPaging(GetManageProductPagingRequest request);

        Task<int> AddImage(int productId, ProductImageCreateRequest request);
        Task<int> RemoveImage( int imageId);
        Task<int> UpdateImage(int imageId, ProductImageUpdateRequest request);
        Task<ProductImageViewModel> GetImageById(int imageId);
        Task<List<ProductImageViewModel>> GetListImages(int productId);
    }
}
