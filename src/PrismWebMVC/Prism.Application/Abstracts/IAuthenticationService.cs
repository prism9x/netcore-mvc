using Prism.Application.DTOs;

namespace Prism.Application.Abstracts
{
    public interface IAuthenticationService
    {
        Task<ResponseModel> CheckLogin(string username, string password, bool hasRemember);
    }
}