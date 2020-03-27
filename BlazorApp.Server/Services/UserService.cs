using BlazorApp.Server.Interfaces;
using BlazorApp.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Server.Services
{
    public class UserService : IUserService
    {

        public UserService()
        {

        }

        public Task<ResponseViewModel> LoginUser(LoginViewModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseViewModel> RegisterUser(RegisterViewModel model)
        {
            if (model == null)
                throw new NullReferenceException("register model is null");
            var response =  new ResponseViewModel() { IsValid = true, Message = "register succces" };

            return response;
        }
    }
}
