using System;

namespace Aurora.Domain.Models
{
    public class UserModel
    {
        public UserModel(int id, string name, DateTime birthDate, string cpf)
        {
            Id = id;
            Name = name;
            BirthDate = birthDate;
            Cpf = cpf;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        public string Cpf { get; set; }
    }
}
