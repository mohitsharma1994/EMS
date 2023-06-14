﻿using EMS.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.BLL.IServices
{
    public interface IDepartmentServices
    {
        Task<List<Department>> GetDepartments();
    }
}
