using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Prism.Application.Abstracts;
using Prism.Application.DTOs;
using Prism.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prism.Application.Services
{
    public class AppUserService(UserManager<AppUser> userManager, IMapper mapper) : IAppUserService
    {
        public async Task<AccountDTO> GetUserByIdAsync(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            var role = (await userManager.GetRolesAsync(user)).First();

            var userDto = mapper.Map<AccountDTO>(user);
            userDto.RoleName = role;

            return userDto;
        }

        public async Task<ResponseDatatable<UserModel>> GetUserByPaginationAsync(RequestDatatable request)
        {
            var users = await userManager.Users.Where(x => x.IsActive && (string.IsNullOrEmpty(request.Keyword)
                                                       || (x.UserName.Contains(request.Keyword)
                                                            || x.Email.Contains(request.Keyword)
                                                            || x.PhoneNumber.Contains(request.Keyword))))
                                                .Select(x => new UserModel
                                                {
                                                    Email = x.Email,
                                                    Phone = x.PhoneNumber,
                                                    Username = x.UserName,
                                                    IsActive = x.IsActive ? "Yes" : "No",
                                                    Id = x.Id,
                                                }).ToListAsync();
            int totalRecords = users.Count;

            var result = users.Skip(request.SkipItems).Take(request.PageSize).ToList();

            return new ResponseDatatable<UserModel>
            {
                Draw = request.Draw,
                RecordsTotal = totalRecords,
                RecordsFiltered = totalRecords,
                Data = result
            };
        }

        public async Task<ResponseModel> SaveAsync(AccountDTO accountDTO)
        {
            string errors = string.Empty;
            IdentityResult identityResult;
            var user = new AppUser
            {
                UserName = accountDTO.UserName,
                Email = accountDTO.Email,
                PasswordHash = accountDTO.Password,
                PhoneNumber = accountDTO.Phone,
                IsActive = accountDTO.IsActive,
            };

            if (string.IsNullOrEmpty(accountDTO.Id))
            {
                // CREATE
                identityResult = await userManager.CreateAsync(user, accountDTO.Password);

                if (identityResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, accountDTO.RoleName);

                    return new ResponseModel
                    {
                        Action = Domain.Enums.ActionType.Insert,
                        Message = "Ok",
                        Status = true,
                    };
                }

                errors = string.Join("<br />", identityResult.Errors.Select(x => x.Description));
            }
            else
            {
                // UPDATE
                var userUpdate = await userManager.FindByIdAsync(accountDTO.Id);

                userUpdate.Email = accountDTO.Email;
                userUpdate.PhoneNumber = accountDTO.Phone;
                userUpdate.IsActive = accountDTO.IsActive;

                identityResult = await userManager.UpdateAsync(userUpdate);

                if (identityResult.Succeeded)
                {
                    var hasRole = await userManager.IsInRoleAsync(userUpdate, accountDTO.RoleName);
                    if (!hasRole)
                    {
                        var oldRoleName = (await userManager.GetRolesAsync(userUpdate)).FirstOrDefault();
                        var removeResult = await userManager.RemoveFromRoleAsync(userUpdate, oldRoleName);
                        if (removeResult.Succeeded)
                        {
                            await userManager.AddToRoleAsync(user, accountDTO.RoleName);
                        }
                    }
                    return new ResponseModel
                    {
                        Status = true,
                        Message = "Update Successful",
                        Action = Domain.Enums.ActionType.Update,
                    };
                }
            }
            errors = string.Join("<br />", identityResult.Errors.Select(x => x.Description));

            return new ResponseModel
            {
                Action = Domain.Enums.ActionType.Insert,
                Message = $"{(string.IsNullOrEmpty(accountDTO.Id) ? "Insert" : "Update")} failed. {errors}",
                Status = false,
            };
        }
    }
}