using EMS.Infrastructure.RequestModels;
using EMS.Infrastructure.ResultModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.BLL.IServices
{
    public interface IEmployeeServices
    {
        Task<List<EmployeeResult>> GetEmployeeList(GetEmployeeRequest employeeParameters);

        Task<EmployeeResult?> GetEmployeeById(int id);

        Task<BaseResult> AddEmployee(NewEmployeeRequest addEmployee);

        Task<BaseResult> UpdateEmployee(UpdateEmployeeRequest updateEmployee);
        Task<BaseResult> DeleteEmployee(int id);
    }
}
