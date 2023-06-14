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
    public class DepartmentServicesTests
    {
        private readonly Mock<IDepartmentRepository> _mockDepartmentRepository;
        private readonly IMapper _mapper;

        #region Constructor
        public DepartmentServicesTests()
        {
            _mockDepartmentRepository = new Mock<IDepartmentRepository>();
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

        [Fact]
        public async Task GetDepartments_Invoke_DepartmentRepository_GetDepartments()
        {
            // Arrange
            _mockDepartmentRepository.Setup(s => s.GetDepartments()).ReturnsAsync(GetTestDepartments());
            var service = GetTestInstance();

            // Act
            await service.GetDepartments();

            //Assert
            _mockDepartmentRepository.Verify(g => g.GetDepartments(), Times.Once);
        }

        [Fact]
        public async Task GetDepartments_Returns_Departments_Successfully()
        {
            // Arrange
            _mockDepartmentRepository.Setup(s => s.GetDepartments()).ReturnsAsync(GetTestDepartments());
            var service = GetTestInstance();

            // Act
            var departments = await service.GetDepartments();

            //Assert
            Assert.IsType<List<Department>>(departments);
            Assert.Equal(GetTestDepartments().Count, departments.Count);
        }

        private List<Department> GetTestDepartments()
        {
            var departments = new List<Department>();
            departments.Add(new Department { ID = 1, Name = "IT" });
            departments.Add(new Department { ID = 2, Name = "HR" });
            departments.Add(new Department { ID = 3, Name = "Accounts" });
            return departments;
        }

        private DepartmentServices GetTestInstance()
        {
            return new DepartmentServices(_mockDepartmentRepository.Object);
        }
    }
}