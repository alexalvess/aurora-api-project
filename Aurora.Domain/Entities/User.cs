using Aurora.Domain.ValueTypes;
using Flunt.Validations;
using System;

namespace Aurora.Domain.Entities
{
    public class User : BaseEntity<int, User>
    {
        public User(int id, string name, DateTime birthDate, Cpf cpf) : base(id)
        {
            AddNotifications(
                ValidateName(name),
                ValidateBirthDate(birthDate),
                cpf.contract);

            if (Valid)
            {
                Name = name;
                BirthDate = birthDate;
                Cpf = cpf;
            }
        }

        public string Name { get; protected set; }

        public DateTime BirthDate { get; protected set; }

        public Cpf Cpf { get; protected set; }

        public int CalculateAge()
        {
            var age = DateTime.Now.Year - BirthDate.Year;

            if (BirthDate.Date > DateTime.Now.AddDays(-age))
                age--;

            return age;
        }

        private Contract ValidateName(string name) =>
            new Contract()
                .IsNotNullOrWhiteSpace(name, nameof(name), "Is necessary to inform the name.")
                .HasMinLen(name, 3, nameof(name), "Name should have at least 3 chars.")
                .HasMaxLen(name, 50, nameof(name), "Name should have no more than 50 chars.");

        private Contract ValidateBirthDate(DateTime birthDate) =>
            new Contract()
                .IsLowerThan(new DateTime(1900, 12, 1), birthDate, nameof(birthDate), "You are very old.")
                .IsGreaterThan(DateTime.Now.AddYears(-8), birthDate, nameof(birthDate), "You are very young.");
    }
}

