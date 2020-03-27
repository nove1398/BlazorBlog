using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlazorApp.Server.Data;
using BlazorApp.Shared.Models;
using BlazorApp.Server.Interfaces;
using BlazorApp.Shared.ViewModels;

namespace BlazorApp.Server.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService service)
        {
            _userService = service;
        }

        // GET: api/v1/User
        [HttpGet]
        public async Task<ActionResult<ResponseViewModel>> GetUsers()
        {
            return await _userService.AllUsers();
        }

        // GET: api/v1/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseViewModel>> GetUser(int id)
        {
            var user = await _userService.GetUserById(id);

            if (user == null)
            {
                return new ResponseViewModel{ Message = "not found", IsValid = false };
            }

            return user;
        }

        // PUT: api/v1/User/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseViewModel>> PutUser(int id,[FromBody] User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }


            try
            {
                await _userService.UpdateUser(id, user);
            }
            catch (DbUpdateConcurrencyException)
            {
                    throw;
            }

            return new ResponseViewModel { Message = "not found", IsValid = false };
        }

        // POST: api/v1/User
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<ResponseViewModel>> PostUser(RegisterViewModel user)
        {
          var response = await  _userService.RegisterUser(user);

            // return CreatedAtAction("GetUser", new { id = user.UserId }, user);
            return response;
        }

        // DELETE: api/v1/User/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseViewModel>> DeleteUser(int id)
        {
           var response =   await _userService.DeleteUserById(id);

            return response;
        }

      
    }
}
