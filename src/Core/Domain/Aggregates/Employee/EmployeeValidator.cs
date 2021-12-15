using FluentValidation;
using System;

namespace Domain.Aggregates.Employee;

public class EmployeeValidator : AbstractValidator<Employee>
{
    public EmployeeValidator()
    {
        RuleFor(employee => employee.BirthDate)
                .GreaterThan(new DateTime(1900, 12, 1))
                    .WithMessage("You are very old!")
                .LessThan(DateTime.Now.AddYears(-8))
                    .WithMessage("You are very young!");
    }
}