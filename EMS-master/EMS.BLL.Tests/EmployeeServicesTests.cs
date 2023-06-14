using AutoMapper;
using EMS.BLL.Services;
using EMS.DAL.IRepository;
using EMS.Infrastructure.Entities;
using EMS.Infrastructure.Mappers;
using EMS.Infrastructure.RequestModels;
using EMS.Infrastructure.ResultModels;
using Moq;
using System.Net;

namespace EMS.BLL.Tests
{
    public class EmployeeServicesTests
    {
        private readonly Mock<IEmployeeRepository> _mockEmployeeRepository;
        private readonly IMapper _mapper;

        #region Constructor
        public EmployeeServicesTests()
        {
            _mockEmployeeRepository = new Mock<IEmployeeRepository>();
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new EmployeeMapperProfile());
                });
                _mapper = mappingConfig.CreateMapper();
            }
        }
        #endregion

        #region GetEmployeeList Tests
        [Fact]
        public async Task GetEmployeeList_Invoke_EmployeeRepository_GetEmployeeList()
        {
            // Arrange
            var getEmployeeRequest = new GetEmployeeRequest()
            {
                PageNumber = 1,
                PageSize = 50
            };

            _mockEmployeeRepository.Setup(s => s.GetEmployeeList(getEmployeeRequest)).ReturnsAsync(GetTestEmployees());
            var service = GetTestInstance();

            // Act
            var employees = await service.GetEmployeeList(getEmployeeRequest);

            //Assert
            _mockEmployeeRepository.Verify(g => g.GetEmployeeList(It.IsAny<GetEmployeeRequest>()), Times.Once);
        }

        [Fact]
        public async Task GetEmployeeList_Returns_EmployeeResult_Successfully()
        {
            // Arrange
            var getEmployeeRequest = new GetEmployeeRequest()
            {
                PageNumber = 1,
                PageSize = 50
            };

            _mockEmployeeRepository.Setup(s => s.GetEmployeeList(getEmployeeRequest)).ReturnsAsync(GetTestEmployees());
            var service = GetTestInstance();

            // Act
            var employees = await service.GetEmployeeList(getEmployeeRequest);

            //Assert
            Assert.IsType<List<EmployeeResult>>(employees);
            Assert.Equal(2, employees.Count);
        }
        #endregion

        #region GetEmployeeById Tests
        [Fact]
        public async Task GetEmployeeId_Invoke_EmployeeRepository_GetEmployeeId()
        {
            // Arrange
            var testEmployee = GetTestEmployees().FirstOrDefault();

            _mockEmployeeRepository.Setup(s => s.GetEmployeeById(It.IsAny<int>())).ReturnsAsync(testEmployee);
            var service = GetTestInstance();

            // Act
            var employee = await service.GetEmployeeById(1);

            //Assert
            _mockEmployeeRepository.Verify(g => g.GetEmployeeById(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task GetEmployeeId_Returns_EmployeeResult_Successfully()
        {
            // Arrange
            var testEmployee = GetTestEmployees().FirstOrDefault();

            _mockEmployeeRepository.Setup(s => s.GetEmployeeById(It.IsAny<int>())).ReturnsAsync(testEmployee);
            var service = GetTestInstance();

            // Act
            var employee = await service.GetEmployeeById(1);

            //Assert
            Assert.IsType<EmployeeResult>(employee);
            Assert.Equal(testEmployee?.Id, employee?.Id);
        }
        #endregion

        #region AddEmployee Tests
        [Fact]
        public async Task AddEmployee_Invoke_EmployeeRepository_AddEmployee()
        {
            // Arrange
            var newEmployeeRequest = new NewEmployeeRequest()
            {
                Name = "Test Employee",
                Email = "test@newtest.com",
                DOB = new DateTime(1999, 11, 22),
                DepartmentId = 1
            };

            _mockEmployeeRepository.Setup(s => s.AddEmployee(It.IsAny<Employee>())).ReturnsAsync(1);
            var service = GetTestInstance();

            // Act
            await service.AddEmployee(newEmployeeRequest);

            //Assert
            _mockEmployeeRepository.Verify(g => g.AddEmployee(It.IsAny<Employee>()), Times.Once);
        }

        [Fact]
        public async Task AddEmployee_Returns_BaseResult_With_201_Status_Code()
        {
            // Arrange
            var newEmployeeRequest = new NewEmployeeRequest()
            {
                Name = "Test Employee",
                Email = "test@newtest.com",
                DOB = new DateTime(1999, 11, 22),
                DepartmentId = 1
            };

            _mockEmployeeRepository.Setup(s => s.AddEmployee(It.IsAny<Employee>())).ReturnsAsync(1);
            var service = GetTestInstance();

            // Act
            var result = await service.AddEmployee(newEmployeeRequest);

            //Assert
            Assert.IsType<BaseResult>(result);
            Assert.Equal(HttpStatusCode.Created, result.StatusCode);
        }
        #endregion

        #region UpdateEmployee Tests
        [Fact]
        public async Task UpdateEmployee_Invoke_EmployeeRepository_UpdateEmployee()
        {
            // Arrange
            var updateEmployeeRequest = new UpdateEmployeeRequest()
            {
                Id = 1,
                Name = "Test Employee",
                Email = "test@newtest.com",
                DOB = new DateTime(1999, 11, 22),
                DepartmentId = 1
            };

            _mockEmployeeRepository.Setup(s => s.UpdateEmployee(It.IsAny<Employee>())).ReturnsAsync(1);
            var service = GetTestInstance();

            // Act
            await service.UpdateEmployee(updateEmployeeRequest);

            //Assert
            _mockEmployeeRepository.Verify(g => g.UpdateEmployee(It.IsAny<Employee>()), Times.Once);
        }

        [Fact]
        public async Task UpdateEmployee_Returns_BaseResult_With_200_Status_Code()
        {
            // Arrange
            var updateEmployeeRequest = new UpdateEmployeeRequest()
            {
                Id = 1,
                Name = "Test Employee",
                Email = "test@newtest.com",
                DOB = new DateTime(1999, 11, 22),
                DepartmentId = 1
            };

            _mockEmployeeRepository.Setup(s => s.UpdateEmployee(It.IsAny<Employee>())).ReturnsAsync(1);
            var service = GetTestInstance();

            // Act
            var result = await service.UpdateEmployee(updateEmployeeRequest);

            //Assert
            Assert.IsType<BaseResult>(result);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task UpdateEmployee_Returns_BaseResult_With_400_Status_Code_If_Employee_Not_Found()
        {
            // Arrange
            var updateEmployeeRequest = new UpdateEmployeeRequest()
            {
                Id = 1,
                Name = "Test Employee",
                Email = "test@newtest.com",
                DOB = new DateTime(1999, 11, 22),
                DepartmentId = 1
            };

            _mockEmployeeRepository.Setup(s => s.UpdateEmployee(It.IsAny<Employee>())).ReturnsAsync((int?)null);
            var service = GetTestInstance();

            // Act
            var result = await service.UpdateEmployee(updateEmployeeRequest);

            //Assert
            Assert.IsType<BaseResult>(result);
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }
        #endregion

        #region DeleteEmployee Tests
        [Fact]
        public async Task DeleteEmployee_Invoke_EmployeeRepository_DeleteEmployee()
        {
            // Arrange
            _mockEmployeeRepository.Setup(s => s.DeleteEmployee(It.IsAny<int>())).ReturnsAsync(1);
            var service = GetTestInstance();

            // Act
            await service.DeleteEmployee(1);

            //Assert
            _mockEmployeeRepository.Verify(g => g.DeleteEmployee(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task DeleteEmployee_Returns_BaseResult_With_200_Status_Code()
        {
            // Arrange
            _mockEmployeeRepository.Setup(s => s.DeleteEmployee(It.IsAny<int>())).ReturnsAsync(1);
            var service = GetTestInstance();

            // Act
            var result = await service.DeleteEmployee(1);

            //Assert
            Assert.IsType<BaseResult>(result);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task DeleteEmployee_Returns_BaseResult_With_400_Status_Code_If_Employee_Not_Found()
        {
            // Arrange
            _mockEmployeeRepository.Setup(s => s.DeleteEmployee(It.IsAny<int>())).ReturnsAsync((int?)null);
            var service = GetTestInstance();

            // Act
            var result = await service.DeleteEmployee(999);

            //Assert
            Assert.IsType<BaseResult>(result);
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }
        #endregion

        private List<Employee> GetTestEmployees()
        {
            var employees = new List<Employee>();
            employees.Add(new Employee { Id = 1, Name = "Test Employee", Email = "test@test.com", DOB = new DateTime(1986, 11, 23, 20, 50, 0), DepartmentId = 1 });
            employees.Add(new Employee { Id = 2, Name = "Test Employee 2", Email = "test2@test.com", DOB = new DateTime(1988, 11, 23, 20, 50, 0), DepartmentId = 2 });
            return employees;
        }

        private EmployeeServices GetTestInstance()
        {
            return new EmployeeServices(_mockEmployeeRepository.Object, _mapper);
        }
    }
}