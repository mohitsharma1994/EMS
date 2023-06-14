using EMS.BLL.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentServices _departmentServices;
        public DepartmentController(IDepartmentServices departmentServices)
        {
            _departmentServices = departmentServices;
        }

        /// <summary>
        /// Get Department List
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetDepartments()
        {
            var result = await _departmentServices.GetDepartments();
            return Ok(result);
        }
    }
}
