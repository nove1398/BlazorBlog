using System.Collections.Generic;
using BlazorApp.Server.Interfaces;
using BlazorApp.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BlazorApp.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BlogController : ControllerBase
    {
        private IUserService _userService;
        private IConfiguration _config;
        public BlogController(IUserService service, IConfiguration config)
        {
            _userService = service;
            _config = config;
        }

        //credentials
        //24b94131800a363fdfd70688b3c25eb53dd346c48e2f5f30e817a205d0c01e50      _________________           => uaQ6HKPRiVx9n4D02U+P2g==
        //Test service
        [HttpGet]
        public IActionResult Test()
        {
            
            return Ok(_config["AuthSettings:Key"]);
        }

        //GET: api/v1/blog/blogs
        [HttpGet("blogs")]
        public IEnumerable<User> GetBlogs()
        {
            return null;
        }

        //GET: api/v1/blog/blogs/4
        [HttpGet("blogs/{id}")]
        public IEnumerable<User> GetBlog(int id)
        {
            return null;
        }

        
    }
}