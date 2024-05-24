using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Prism.Application.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prism.Application.Services
{
    public class RoleService(RoleManager<IdentityRole> roleManager) : IRoleService
    {
        public async Task<IEnumerable<SelectListItem>> GetRoleForDropdownlistAsync()
        {
            var roles = await roleManager.Roles.ToListAsync();

            var roleSelectList = roles.Select(x => new SelectListItem
            {
                Value = x.Name,
                Text = x.Name
            }).ToList();

            // Add a placeholder item at the beginning
            roleSelectList.Insert(0, new SelectListItem
            {
                Value = "", // Ensure the value is empty for the placeholder
                Text = "-- Chọn --",
                Disabled = true,
                Selected = true // Mark the placeholder as selected
            });

            return roleSelectList;
        }
    }
}