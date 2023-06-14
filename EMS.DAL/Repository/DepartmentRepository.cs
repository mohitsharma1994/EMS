using EMS.DAL.IRepository;
using EMS.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        public readonly ApplicationDBContext _context;
        public DepartmentRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<List<Department>> GetDepartments()
           => await _context.Department.ToListAsync();
    }
}
