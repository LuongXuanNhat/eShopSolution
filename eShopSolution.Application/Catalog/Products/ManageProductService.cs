using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Catalog.Products.Manage;
using eShopSolution.ViewModels.Common;
using eShopSolution.Data.EF;
using eShopSolution.Data.Entities;
using eShopSolution.Utilities.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.IO;
using eShopSolution.Application.Common;

namespace eShopSolution.Application.Catalog.Products
{
    public class ManageProductService : IManageProductService
    {
        private readonly EShopDbContext _context;
        private readonly IStorageService _storageService;
        public ManageProductService(EShopDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }

        public Task<int> AddImages(int productId, List<IFormFile> files)
        {
            throw new NotImplementedException();
        }

        public async Task AddViewCount(int productId)
        {
            // Async phai dung await
            var product = await _context.Products.FindAsync(productId);
            product.ViewCount += 1;
            // Luu va tra ve 1 so
            await _context.SaveChangesAsync();
        }

        public async Task<int> Create(ProductCreateRequest createRequest)
        {
            var product = new Product()
            {
                Price = createRequest.Price,
                OriginalPrice = createRequest.OriginalPrice,
                Stock = createRequest.Stock,
                ViewCount = 0,
                DateCreated = DateTime.Now,
                ProductTranslations = new List<ProductTranslation>()
                {
                    new ProductTranslation()
                    {
                        Name = createRequest.Name,
                        Description = createRequest.Description,
                        Details = createRequest.Details,
                        SeoAlias = createRequest.SeoAlias,
                        SeoDescription = createRequest.SeoDescription,
                        SeoTitle = createRequest.SeoTitle,

                    }
                }
            };
            // Save image
            // Xem lai
            product.ProductImages = new List<ProductImage>()
                {
                    new ProductImage()
                    {
                        Caption = "Thumbnail image",
                        DateCreated = DateTime.Now,
                        FileSize = createRequest.ThumbnailImage.Length,
                        ImagePath =await this.SaveFile(createRequest.ThumbnailImage),
                        IsDefault = true,
                        SortOrder = 1
                    }
            };
            _context.Products.Add(product);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(int ProductId)
        {
            // Async phai dung await
            var product = await _context.Products.FindAsync(ProductId);
            if (product == null)
            {
                throw new eShopException($"Khong tim thay san pham: {ProductId}");
            }
            var images = _context.ProductImages.Where(i => i.ProductId == ProductId);
            foreach (var image in images)
            {
                await _storageService.DeleteFileAsync(image.ImagePath);
            }

            _context.Products.Remove(product);
            // Luu va tra ve 1 so
            return await _context.SaveChangesAsync();
        }

        //public Task<List<ProductViewModel>> GetAll()
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<PagedResult<ProductViewModel>> GetAllPaging(GetProductPagingRequest request)
        {
            // 1 Select Join
            var query = from p in _context.Products
                        join pt in _context.ProductTranslations on p.Id equals pt.ProductId
                        join pic in _context.ProductInCategories on p.Id equals pic.ProductId
                        join c in _context.Categories on pic.CategoryId equals c.Id
                        where pt.Name.Contains(request.keyWord)
                        select new { p, pt, pic, c };
            // 2 Filter 
            if (!string.IsNullOrEmpty(request.keyWord))
            {
                query = query.Where(x => x.pt.Name.Contains(request.keyWord));
            }

            if (request.CategoryIds.Count > 0)
            {
                query = query.Where(p => request.CategoryIds.Contains(p.pic.CategoryId));
            }
            // 3 Paging ( Phan trang tat ca)
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) *
                request.PageSize).Take(request.PageSize)
                .Select(x => new ProductViewModel()
                {
                    Id = x.p.Id,
                    Name = x.pt.Name,
                    Description = x.pt.Description,
                    DateCreated = x.p.DateCreated,
                    Details = x.pt.Details,
                    LanguageId = x.pt.LanguageId,
                    OriginalPrice = x.p.OriginalPrice,
                    Price = x.p.Price,
                    SeoAlias = x.pt.SeoAlias,
                    SeoDescription = x.pt.SeoDescription,
                    SeoTitle = x.pt.SeoTitle,
                    Stock = x.p.Stock,
                    ViewCount = x.p.ViewCount
                }).ToListAsync();

            // 4 Select and projection
            var pagedResult = new PagedResult<ProductViewModel>()
            {
                TotalRecord = totalRow,
                Items = data
            };
            return pagedResult;

        }

        public Task<List<ViewModels.Catalog.Products.ProductImageViewModel>> GetListImage(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<int> RemoveImages(int imageId)
        {
            throw new NotImplementedException();
        }

        public async Task<int> Update(ProductUpdateRequest editRequest)
        {
            var product = await _context.Products.FindAsync(editRequest.Id);
            var productTranslations = await _context.ProductTranslations.FirstOrDefaultAsync(x => x.ProductId == editRequest.Id 
                && x.LanguageId == editRequest.LanguageId);

            if (product == null || productTranslations == null)
            {
                throw new eShopException($"Khong tim thay id: {editRequest.Id}");
            }

            productTranslations.Name = editRequest.Name;
            productTranslations.SeoAlias = editRequest.SeoAlias;
            productTranslations.SeoDescription = editRequest.SeoDescription;
            productTranslations.SeoTitle = editRequest.SeoTitle;
            productTranslations.Description = editRequest.Description;
            productTranslations.Details = editRequest.Details;

            // Save image
            var thumbnailImage = await _context.ProductImages.FirstOrDefaultAsync(i => i.IsDefault == true && i.ProductId == editRequest.Id);
            if (thumbnailImage != null)
            {
                thumbnailImage.FileSize = editRequest.ThumbnailImage.Length;
                thumbnailImage.ImagePath = await this.SaveFile(editRequest.ThumbnailImage);
                _context.ProductImages.Update(thumbnailImage);

            }

            return await _context.SaveChangesAsync();
        }

        public Task<int> UpdateImages(int imageId, string caption, bool isDefault)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdatePrice(int ProductId, decimal newPrice)
        {
            var product = await _context.Products.FindAsync(ProductId);
            if (product == null)
            {
                throw new eShopException($"Khong tim thay id: {ProductId}");
            }

            product.Price = newPrice;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateStock(int ProductId, int addedQuantity)
        {
            var product = await _context.Products.FindAsync(ProductId);
            if (product == null)
            {
                throw new eShopException($"Khong tim thay id: {ProductId}");
            }

            product.Price = addedQuantity;
            return await _context.SaveChangesAsync() > 0;
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName;
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }
    }
}
