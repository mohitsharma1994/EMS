using EMS.Infrastructure.Entities;
using EMS.Infrastructure.RequestModels;
using EMS.Infrastructure.ResultModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text.Json;

namespace EMS.WEB.Controllers
{
    public class EmployeeController : Controller
    {
        private const string API_URL = "ApiURL";
        private readonly IConfiguration _config;

        public EmployeeController(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IActionResult> List([FromQuery]string search)
        {
            var employees = new List<EmployeeResult>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_config.GetValue<string>(API_URL));
                //HTTP GET
                var responseTask = await client.GetAsync("Employee" + (!string.IsNullOrWhiteSpace(search) ? "?search=" + search : ""));

                if (responseTask.IsSuccessStatusCode)
                {
                    var response = responseTask.Content.ReadAsStringAsync().Result;
                    employees = JsonConvert.DeserializeObject<List<EmployeeResult>>(response);
                }  
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

            }
            return View(employees);
        }

        public async Task<IActionResult> AddNewEmployee()
        {
            var employees = new EmployeeResult();
            await GetDepartments();
            return View("AddEditEmployee", employees);
        }

        [HttpPost]
        public async Task<IActionResult> AddEditEmployee(NewEmployeeRequest newEmployee)
        {
            if (newEmployee.Id == 0)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_config.GetValue<string>(API_URL));

                    var postTask = await client.PostAsJsonAsync<NewEmployeeRequest>("Employee", newEmployee);

                    if (postTask.IsSuccessStatusCode)
                    {
                        return RedirectToAction("List", "Employee", new { area = "" });
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    }
                }
            }
            else
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_config.GetValue<string>(API_URL));

                    var postTask = await client.PutAsJsonAsync<NewEmployeeRequest>("Employee", newEmployee);

                    if (postTask.IsSuccessStatusCode)
                    {
                        return RedirectToAction("List", "Employee", new { area = "" });
                    }
                }
            }

            return View();
        }

        public async Task<IActionResult> UpdateEmployee(int id)
        {
            await GetDepartments();
            var employee = new EmployeeResult();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_config.GetValue<string>(API_URL));
                //HTTP GET
                var responseTask = await client.GetAsync("Employee/Id?id=" + id);

                if (responseTask.IsSuccessStatusCode)
                {
                    var EmpResponse = responseTask.Content.ReadAsStringAsync().Result;
                    employee = JsonConvert.DeserializeObject<EmployeeResult>(EmpResponse);
                }
            }

            return View("AddEditEmployee", employee);
        }

        public async Task<IActionResult> DeleteEmployee(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_config.GetValue<string>(API_URL));

                var postTask = await client.DeleteAsync("Employee?id=" + id);

                if (postTask.IsSuccessStatusCode)
                {
                    return RedirectToAction("List", "Employee", new { area = "" });
                }
            }

            return View();
        }

        public async Task<bool> GetDepartments()
        {
            var departments = new List<Department>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_config.GetValue<string>(API_URL));
                //HTTP GET
                var responseTask = await client.GetAsync("Department");

                if (responseTask.IsSuccessStatusCode)
                {
                    var deptResponse = responseTask.Content.ReadAsStringAsync().Result;
                    departments = JsonConvert.DeserializeObject<List<Department>>(deptResponse);
                }
            }
            ViewBag.Departments = departments;

            return true;
        }
    }
}
