using FluentValidation;
using System;

namespace Domain.Aggregates.Employee;

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