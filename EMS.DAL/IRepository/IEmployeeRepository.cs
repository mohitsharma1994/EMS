using EMS.Infrastructure.Entities;
using EMS.Infrastructure.RequestModels;
using EMS.Infrastructure.ResultModels;

namespace EMS.DAL.IRepository
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetEmployeeList(GetEmployeeRequest getEmployeeRequest);

        Task<Employee?> GetEmployeeById(int id);

        Task<int> AddEmployee(Employee addEmployee);

        Task<int?> UpdateEmployee(Employee employee);

        Task<int?> DeleteEmployee(int id);
    }
}
