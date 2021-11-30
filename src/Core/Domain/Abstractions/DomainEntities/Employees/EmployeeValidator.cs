using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstractions.DomainEntities.Employees
{
    public class EmployeeValidator : AbstractValidator<Employee>
    {
        public EmployeeValidator()
        {
            RuleFor(employee => employee.BirthDate)
                .GreaterThan(new DateOnly(1900, 12, 1))
                    .WithMessage("You are very old!")
                .LessThan(DateOnly.FromDateTime(DateTime.Now.AddYears(-8)))
                    .WithMessage("You are very young!");
        }
    }
}
