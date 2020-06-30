using System;
using Flunt.Validations;

namespace Aurora.Domain.Entities
{
    public class User : BaseEntity<int, User>
    {
        public User(int id, string name, DateTime birthDate, string cpf) : base(id)
        {
            AddNotifications(
                ValidateName(name),
                ValidateBirthDate(birthDate),
                ValidateCpf(cpf));

            if (Valid)
            {
                Name = name;
                BirthDate = birthDate;
                Cpf = cpf;
            }
        }

        public string Name { get; protected set; }

        public DateTime BirthDate { get; protected set; }

        public string Cpf { get; protected set; }

        private Contract ValidateName(string name) =>
            new Contract()
                .IsNotNullOrWhiteSpace(name, nameof(name), "Is necessary to inform the name.")
                .HasMinLen(name, 3, nameof(name), "Name should have at least 3 chars.")
                .HasMaxLen(name, 50, nameof(name), "Name should have no more than 50 chars.");

        private Contract ValidateBirthDate(DateTime birthDate) =>
            new Contract()
                .IsLowerThan(new DateTime(1900, 12, 1), birthDate, nameof(birthDate), "You are very old.")
                .IsGreaterThan(DateTime.Now.AddYears(-8), birthDate, nameof(birthDate), "You are very young.");

        private Contract ValidateCpf(string cpf) =>
            new Contract()
                .IsNotNullOrWhiteSpace(cpf, nameof(cpf), "Is necessary to inform the CPF.")
                .HasExactLengthIfNotNullOrEmpty(cpf, 11, nameof(cpf), "CPF should have 11 chars.");
    }
}

