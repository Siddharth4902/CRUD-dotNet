using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;
using CRUD_PG.Models;
using CRUD_PG.DAO;
using System.Numerics;
namespace CRUD_PG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeDAO _empDao;

        public EmployeeController(IEmployeeDAO empDao)
        {
            _empDao = empDao;
        }
        [HttpGet("GetEmployees")]
        public async Task<ActionResult<List<Employee>>> GetEmployee()
        {
            var emp = await _empDao.GetEmployee();
            if (emp == null)
            {
                return NotFound();
            }
            return Ok(emp);
        }
        [HttpPost("CreateEmployee")]
        public async Task<ActionResult<Employee?>> CreatePlayer(Employee employee)
        {
            if (employee != null)
            {
                if (ModelState.IsValid)
                {
                    int res = await _empDao.InsertEmployee(employee);
                    if (res > 0)
                    {
                        return Ok();
                    }
                }
                return BadRequest("Failed to add employee");
            }
            else
            {
                return BadRequest("No employee Found");
            }
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<int>> DeleteEmployee(int id)
        {
            int res = await _empDao.DeleteEmployee(id);
            if (res != 0) return Ok(id);
            else return NotFound($"Id {id} not found");
        }
        [HttpPut("{Id}")]
        public async Task<ActionResult<Employee>> UpdateEmployee(int Id, string Name, string DOB, string R_add, string P_add, string Contact, string Email, string M_Status, string Gender, string Occupation, string Aadhaar, string Pan)
        {
            int res = 0;
            // Product? product = null;
            res = await _empDao.UpdateEmployee(Id, Name, DOB, R_add, P_add, Contact, Email, M_Status, Gender, Occupation, Aadhaar, Pan);
            if (res != null)
            {
                return NoContent();
            }
            else
            {
                return NotFound("Id not found");
            }
        }
    }
}
