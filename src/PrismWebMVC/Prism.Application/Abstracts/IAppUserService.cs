using Prism.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prism.Application.Abstracts
{
    public interface IAppUserService
    {
        Task<ResponseDatatable<UserModel>> GetUserByPaginationAsync(RequestDatatable request);

        Task<AccountDTO> GetUserByIdAsync(string id);
        Task<ResponseModel> SaveAsync(AccountDTO accountDTO);
    }
}
