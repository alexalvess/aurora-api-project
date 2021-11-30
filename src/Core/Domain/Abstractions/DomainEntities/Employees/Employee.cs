using Domain.Abstractions.Entities;
using Domain.ValueTypes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstractions.DomainEntities.Employees
{
    public abstract class Employee : Entity<ObjectId>
    {
        public Name Name { get; init; }

        public Nin Nin { get; init; }

        public Password Password { get; init; }

        public DateOnly BirthDate { get; init; }

        protected override bool Validate()
        {
            AddErrors(Name.Errors);
            AddErrors(Nin.Errors);
            AddErrors(Password.Errors);

            return OnValidate<EmployeeValidator, Employee>();
        }
    }
}
