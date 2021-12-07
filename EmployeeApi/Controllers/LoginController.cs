using EmployeeApi.Data;
using EmployeeApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UserDBContext _context;
        public LoginController(UserDBContext userDBContext)
        {
            _context = userDBContext;
        }

        [HttpGet("users")]
        public IActionResult GetUsers()
        {
            var userdetails = _context.Users.AsQueryable();
            return Ok(userdetails);
        }

        [HttpPost("signup")]
        public IActionResult SignUp([FromBody] User userObj)
        {
            if(userObj == null)
            {
                return BadRequest();
            }
            else
            {
                _context.Users.Add(userObj);
                _context.SaveChanges();
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "User Added Successfully"
                });
            }
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] User userObj)
        {
            if(userObj == null)
            {
                return BadRequest();
            }
            else
            {
                var user = _context.Users.Where(a => a.UserName == userObj.UserName && a.Password == userObj.Password).FirstOrDefault();
                if(user != null)
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Logged Successfully",
                        UserData = userObj.FullName
                    });
                }
                else
                {
                    return Ok(new
                    {
                        StatusCode = 400,
                        Message = "User Not Found",
                    });
                }
            }
        }
    }
}
