using EMS.DAL.IRepository;
using EMS.Infrastructure.Entities;
using EMS.Infrastructure.RequestModels;
using EMS.Infrastructure.ResultModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public readonly ApplicationDBContext _context;
        public EmployeeRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Employee>> GetEmployeeList(GetEmployeeRequest getEmployeeRequest)
        {
            var query = _context.Employee.Include(x => x.Department).AsQueryable();
            var search = getEmployeeRequest.Search;

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(x => x.Name.Contains(search) || x.Email.Contains(search) || x.Department.Name.Contains(search));
            }
            
            return await query.Skip((getEmployeeRequest.PageNumber - 1) * getEmployeeRequest.PageSize)
                .Take(getEmployeeRequest.PageSize).ToListAsync();
        }

        public async Task<Employee?> GetEmployeeById(int id)
            => await _context.Employee.Include(x => x.Department).FirstOrDefaultAsync(x => x.Id == id);

        public async Task<int> AddEmployee(Employee employee)
        {
            await _context.Employee.AddAsync(employee);

            var result = await _context.SaveChangesAsync();
            return result;
        }

        public async Task<int?> UpdateEmployee(Employee employee)
        {
            var employeeDetail = await _context.Employee.FirstOrDefaultAsync(x => x.Id == employee.Id);

            if (employeeDetail == null)
            {
                return null;
            }

            employeeDetail.Name = employee.Name;
            employeeDetail.Email = employee.Email;
            employeeDetail.DepartmentId = employee.DepartmentId;
            employeeDetail.DOB = employee.DOB;
            var result = await _context.SaveChangesAsync();
            
            return result;
        }

        public async Task<int?> DeleteEmployee(int id)
        {
            var employeeDetail = await _context.Employee.FirstOrDefaultAsync(x => x.Id == id);

            if (employeeDetail == null)
            {
                return null;
            }

            _context.Employee.Remove(employeeDetail);
            var result = await _context.SaveChangesAsync();

            return result;
        }
    }
}
