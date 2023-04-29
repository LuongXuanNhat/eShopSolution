using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.System.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.System.Users
{
    public interface IUserService
    {
        Task<ApiResult<string>> Authenticate(LoginRequest request);
        Task<ApiResult<bool>> Register(RegisterRequest request);
        Task<ApiResult<bool>> Update(Guid id,UserUpdateRequest request);

        // Lấy danh sách user- trả về model phân trang
        Task<ApiResult<PagedResult<UserVM>>> GetUsersPaging(GetUsersPagingRequest request);
        Task<ApiResult<UserVM>> GetById(Guid id);

    }
}
