using BlazorApp.Server.Data;
using BlazorApp.Server.Interfaces;
using BlazorApp.Shared.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.Server.Services
{
    public class UserService : IUserService
    {
        private readonly BlazorBlogContext _context;
        private readonly IConfiguration _config;

        public UserService(BlazorBlogContext context,IConfiguration config )
        {
            _context = context;
            _config = config;
        }

        public Task<ResponseViewModel> GetUserById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseViewModel> LoginUser(LoginViewModel model)
        {
            //Set user claims if login success

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, model.Email),
                new Claim(ClaimTypes.NameIdentifier, 1.ToString()),
                new Claim(ClaimTypes.Role, 4.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["AuthSettings:Key"]));
            var token = new JwtSecurityToken(
                issuer: _config["AuthSettings:Issuer"],
                audience: _config["AuthSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.Sha256)
                );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return new ResponseViewModel
            {
                IsValid = true,
                Message = tokenString,
                ExpiresDate = token.ValidTo
            };
        }

        public async Task<ResponseViewModel> RegisterUser(RegisterViewModel model)
        {
            if (model == null)
                throw new NullReferenceException("Register model is null");

            //Logic to register user
            var response =  new ResponseViewModel() { IsValid = true, Message = "register succces" };

            return response;
        }
    }
}
