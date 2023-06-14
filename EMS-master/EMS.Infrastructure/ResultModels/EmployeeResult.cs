using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Infrastructure.ResultModels
{
    public class EmployeeResult 
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter name")]
        public string Name { get; set; }

        public string Email { get; set; }

        public DateTime DOB { get; set; }

        public int DepartmentId { get; set; }
         
        public string DepartmentName { get; set; }
    }
}
