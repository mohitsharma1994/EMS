using AutoMapper;
using EMS.BLL.IServices;
using EMS.DAL.IRepository;
using EMS.Infrastructure.Entities;
using EMS.Infrastructure.RequestModels;
using EMS.Infrastructure.ResultModels;

namespace EMS.BLL.Services
{
    public class EmployeeServices : IEmployeeServices
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeServices(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<List<EmployeeResult>> GetEmployeeList(GetEmployeeRequest getEmployeeRequest)
        {
            var employees = await _employeeRepository.GetEmployeeList(getEmployeeRequest);
            return _mapper.Map<List<EmployeeResult>>(employees);
        }

        public async Task<EmployeeResult?> GetEmployeeById(int id)
        {
            var employees = await _employeeRepository.GetEmployeeById(id);
            return _mapper.Map<EmployeeResult>(employees);
        }

        public async Task<BaseResult> AddEmployee(NewEmployeeRequest addEmployee)
        {
            var employee = _mapper.Map<Employee>(addEmployee);

            await _employeeRepository.AddEmployee(employee);

            return new BaseResult { StatusCode = System.Net.HttpStatusCode.Created, Message = "Success" };
        }

        public async Task<BaseResult> UpdateEmployee(UpdateEmployeeRequest updateEmployee)
        {
            var employee = _mapper.Map<Employee>(updateEmployee);

            var result = await _employeeRepository.UpdateEmployee(employee);
            if (result == null)
            {
                return new BaseResult { StatusCode = System.Net.HttpStatusCode.NotFound, Message = "Employee Detail Not Found !" };
            }

            return new BaseResult { StatusCode = System.Net.HttpStatusCode.OK, Message = "Success" };
        }

        public async Task<BaseResult> DeleteEmployee(int id)
        {
            var result = await _employeeRepository.DeleteEmployee(id);
            if(result == null)
            {
                return new BaseResult { StatusCode = System.Net.HttpStatusCode.NotFound, Message = "Employee Detail Not Found !" };
            }

            return new BaseResult { StatusCode = System.Net.HttpStatusCode.OK, Message = "Success" };
        }
    }
}
