using BlazorApp.Shared.Models;
using BlazorApp.Shared.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Server.Interfaces
{
    public interface IUserService
    {
        Task<ActionResult<ResponseViewModel>> AllUsers();
        Task<ActionResult<ResponseViewModel>> RegisterUser(RegisterViewModel model);
        Task<ActionResult<ResponseViewModel>> LoginUser(LoginViewModel model);
        Task<ActionResult<ResponseViewModel>> GetUserById(int? id);
        Task<ActionResult<ResponseViewModel>> DeleteUserById(int id);
        Task<ActionResult<ResponseViewModel>> UpdateUser(int id,User model);
    }
}
