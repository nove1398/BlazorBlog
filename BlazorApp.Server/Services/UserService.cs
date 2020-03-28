using BlazorApp.Server.Data;
using BlazorApp.Server.Interfaces;
using BlazorApp.Server.Utility;
using BlazorApp.Shared.Extensions;
using BlazorApp.Shared.Models;
using BlazorApp.Shared.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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

        public async  Task<ActionResult<ResponseViewModel>> AllUsers()
        {
            var data = await _context.Users.AsNoTracking().ToListAsync();
            if (data != null && data.Count > 0)
                return new ResponseViewModel { IsValid = true, ResultData = JsonConvert.SerializeObject(data) };
            else
                return new ResponseViewModel { IsValid = false, Message = "no items found" };
        }

        public async Task<ActionResult<ResponseViewModel>> DeleteUserById(int id)
        {

            var toBeremoved = await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);
            if(toBeremoved == null){
                return new ResponseViewModel { IsValid = false, Message = "could not be found" };

            }
            _context.Remove(toBeremoved);
            await _context.SaveChangesAsync();
            return new ResponseViewModel { IsValid = true, Message = "data removed", ResultData = JsonConvert.SerializeObject(toBeremoved) };
        }

        public async Task<ActionResult<ResponseViewModel>> GetUserById(int? id)
        {
            if(id == null)
            {
                return new ResponseViewModel { IsValid = false, Message = "invalid resource" };

            }

            var model = await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);

            if(model != null)
            {
                return new ResponseViewModel { ResultData = JsonConvert.SerializeObject(model), Message = "found", IsValid = true };
            }else
            {
                return new ResponseViewModel { IsValid = false, Message = "no user found by that id" };
            }
        }

        /// <summary>
        /// Login User Service
        /// </summary>
        /// <param name="model">Login model DTO for web api</param>
        /// <returns>Response with token and expiredate and valid status</returns>
        public async Task<ActionResult<ResponseViewModel>> LoginUser(LoginViewModel model)
        {

            var loginUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
            if(loginUser == null)
            {
                return new ResponseViewModel { IsValid = false, Message = "user account not found", ResultData = JsonConvert.SerializeObject(model) };
            }

            if(!PasswordHasher.VerifyPass(model.Password, loginUser.Password, loginUser.Salt))
            {

                return new ResponseViewModel { IsValid = false, Message = "invalid credentials", ResultData = JsonConvert.SerializeObject(model) };
            }

            //Set user claims if login success
            var assignedRoles = await _context.UserRoles.Where(x => x.UserId == loginUser.UserId).Select(y => y.RoleId).ToListAsync();
            
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, model.Email),
                new Claim(ClaimTypes.NameIdentifier, 1.ToString()),
                
            };
            foreach (var role in assignedRoles)
                new Claim(ClaimTypes.Role, role.ToString());


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["AuthSettings:Key"]));
            var token = new JwtSecurityToken(
                issuer: _config["AuthSettings:Issuer"],
                audience: _config["AuthSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(2),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return new ResponseViewModel
            {
                IsValid = true,
                Message = tokenString,
                ExpiresDate = token.ValidTo
            };
        }

        public async Task<ActionResult<ResponseViewModel>> RegisterUser(RegisterViewModel model)
        {
            if (model == null)
                throw new NullReferenceException("Register model is null");

            //Logic to register user
            string salty = PasswordHasher.GenerateSalt();
            var newUser = new User();
            newUser.Email = model.Email.ToLower().Trim();
            newUser.FirstName = model.FirstName.ToLower().Trim();
            newUser.LastName = model.LastName.ToLower().Trim();
            newUser.Birthday = model.Birthday;
            newUser.CreatedAt = DateTime.Now;
            newUser.LevelId = 1;
            newUser.Status = User.AccountStatus.Pending;
            newUser.Password = PasswordHasher.HashPass(model.Password, salty);
            newUser.Salt = salty;

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return  new ResponseViewModel() { IsValid = true, Message = "register succces" , ResultData = JsonConvert.SerializeObject(model)};
        }

        public async Task<ActionResult<ResponseViewModel>> UpdateUser(int id, User model)
        {
            if(id != model.UserId)
            {
                return new ResponseViewModel { Message = "Invalid update request", IsValid = false, ResultData = JsonConvert.SerializeObject(model) };
            }
            var currentUser = await _context.Users.FirstOrDefaultAsync(x => x.UserId == id);
            currentUser.FirstName = model.FirstName.ToLower().Trim();
            currentUser.LastName = model.LastName.ToLower().Trim();
            currentUser.Birthday = model.Birthday;
            currentUser.Email = model.Email.ToLower().Trim();
            currentUser.Username = model.Username.Trim();
            _context.Users.Update(currentUser);
            await _context.SaveChangesAsync();
            return new ResponseViewModel { IsValid = true, Message = "data updated", ResultData = JsonConvert.SerializeObject(model) };
        }
    }
}
