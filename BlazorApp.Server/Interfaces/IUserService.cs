using BlazorApp.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Server.Interfaces
{
    public interface IUserService
    {
        Task<ResponseViewModel> RegisterUser(RegisterViewModel model);
        Task<ResponseViewModel> LoginUser(LoginViewModel model);
    }
}
