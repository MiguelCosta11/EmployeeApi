using EmployeeApi.Data;
using EmployeeApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly UserDBContext _context;
        public EmployeeController(UserDBContext userDBContext)
        {
            _context = userDBContext;
        }
        //Adiciona employees
        [HttpPost("add_employee")]
        public IActionResult SignUp([FromBody] Employee employeeObj)
        {
            if (employeeObj == null)
            {
                return BadRequest();
            }
            else
            {
                _context.Employees.Add(employeeObj);
                _context.SaveChanges();
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Employee Added Successfully"
                });
            }
        }

        [HttpPut("update_employee")]
        public IActionResult UpdateEmployee([FromBody] Employee employeeObj)
        {
            if (employeeObj == null)
            {
                return BadRequest();
            }

            var user = _context.Employees.AsNoTracking().FirstOrDefault(a => a.Id == employeeObj.Id);
         
            if (user == null)
            {
                return Ok(new
                {
                    StatusCode = 404,
                    Message = "User Not Found",
                    
                });
            }
            else
            {
                _context.Entry(employeeObj).State = EntityState.Modified;
                _context.SaveChanges();
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Employee Updated Successfully",

                });
            }
            
        }

        [HttpDelete("delete_employee/{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            var user = _context.Employees.Find(id);
            if (user == null)
            {
                return BadRequest();
            }
            else
            {
                _context.Remove(user);
                _context.SaveChanges();
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Employee Removed Successfully"
                });
            }
        }

        [HttpGet("get_all_employees")]
        public IActionResult GetAllEmployees()
        {
            var employee = _context.Employees.AsQueryable();
            return Ok(new 
            {
                StatusCode = 200,
                EmployeeDetails = employee
            });
        }

        [HttpGet("get_employee/id")]
        public IActionResult GetEmployee(int id)
        {
            var employee = _context.Employees.Find(id);
            if(employee == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "User Not Found"
                });
            }
            else
            {
                return Ok(new
                {
                    StatusCode = 200,
                    EmployeeDetails = employee
                });
            }
        }
    }
}
