using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorApp.Server.Interfaces;
using BlazorApp.Shared.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IUserService _userService;
        public AuthController(IUserService service)
        {
            _userService = service;
        }

        //POST:/api/v1/blog/auth/register
        [HttpPost("auth/register")]
        public async Task<IActionResult> RegisterUser([FromBody]RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Check the form and try again");
            }
            var response = await _userService.RegisterUser(model);

            return Ok(response);
        }

        //POST:/api/v1/blog/auth/login
        [HttpPost("auth/register")]
        public async Task<IActionResult> RegisterUser([FromBody]LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Check the form and try again");
            }
            var response = await _userService.LoginUser(model);

            return Ok(response);
        }
    }
}