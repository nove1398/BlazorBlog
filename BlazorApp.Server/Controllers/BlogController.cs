using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorApp.Server.Interfaces;
using BlazorApp.Shared.Models;
using BlazorApp.Shared.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp.Server.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BlogController : ControllerBase
    {
        private IUserService _userService;

        public BlogController(IUserService service)
        {
            _userService = service;
        }

        //Test service
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("It Works");
        }

        //GET: api/v1/blog/users
        [HttpGet("users")]
        public IEnumerable<User> GetAllUsers()
        {
            return null;
        }

        //GET: api/v1/blog/users/4
        [HttpGet("users/{id}")]
        public IEnumerable<User> GetAllUsers(int id)
        {
            return null;
        }

        
    }
}