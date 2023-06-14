using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Infrastructure.Entities
{
    public class Employee
    {
        public int  Id { get; set; }

        [StringLength(250)]
        public string Name { get; set; } = String.Empty;

        [StringLength(250)]
        public string Email { get; set; } = string.Empty;

        public DateTime DOB { get; set; }

        public int DepartmentId { get; set; }

        public Department Department { get; set; }
    }
}
