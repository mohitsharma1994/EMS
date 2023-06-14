using EMS.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.IRepository
{
    public interface IDepartmentRepository
    {
        Task<List<Department>> GetDepartments();
    }
}
