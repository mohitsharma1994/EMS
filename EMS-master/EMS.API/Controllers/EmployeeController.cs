using EMS.BLL.IServices;
using EMS.Infrastructure.RequestModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeServices _employeeServices;
        public EmployeeController(IEmployeeServices employeeServices)
        {
            _employeeServices = employeeServices;
        }

        /// <summary>
        /// Get Employee List
        /// </summary>
        /// <param name="employeeParameters"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetEmployeeList([FromQuery] GetEmployeeRequest employeeParameters)
        {
            var result = await _employeeServices.GetEmployeeList(employeeParameters);
            return Ok(result);
        }

        /// <summary>
        /// Get Employee By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Id")]
        public async Task<IActionResult> GetEmployeeById([FromQuery] int id)
        {
            var result = await _employeeServices.GetEmployeeById(id);
            return Ok(result);
        }

        /// <summary>
        /// Add Employee
        /// </summary>
        /// <param name="addEmployee"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddEmployee(NewEmployeeRequest addEmployee)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }
            var result = await _employeeServices.AddEmployee(addEmployee);
            return Ok(result);
        }

        /// <summary>
        /// Update Employee
        /// </summary>
        /// <param name="updateEmployee"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateEmployee(UpdateEmployeeRequest updateEmployee)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }
            var result = await _employeeServices.UpdateEmployee(updateEmployee);
            return Ok(result);
        }

        /// <summary>
        /// Delete Employee
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteEmployee([FromQuery] int id)
        {
            var result = await _employeeServices.DeleteEmployee(id);
            return Ok(result);
        }
    }
}
