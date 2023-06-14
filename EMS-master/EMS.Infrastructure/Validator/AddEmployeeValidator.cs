using EMS.Infrastructure.Entities;
using EMS.Infrastructure.RequestModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Infrastructure.Validator
{
    public class AddEmployeeValidator : AbstractValidator<NewEmployeeRequest>
    {
        public AddEmployeeValidator()
        {
            RuleFor(employee => employee.Name).NotNull().NotEmpty().Length(1, 250);
            RuleFor(employee => employee.Email).NotNull().NotEmpty().Length(1, 250); 
        }
    }
}
