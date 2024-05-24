using Microsoft.AspNetCore.Mvc.Rendering;

namespace Prism.Application.Abstracts
{
    public interface IRoleService
    {
        Task<IEnumerable<SelectListItem>> GetRoleForDropdownlistAsync();
    }
}