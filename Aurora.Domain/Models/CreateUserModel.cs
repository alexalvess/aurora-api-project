using System;

namespace Aurora.Domain.Models
{
    public class CreateUserModel
    {
        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        public string Cpf { get; set; }
    }
}
