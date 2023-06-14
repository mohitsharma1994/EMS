using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Infrastructure.RequestModels
{
    public class NewEmployeeRequest
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public DateTime DOB { get; set; }

        public int DepartmentId { get; set; }
    }
}
