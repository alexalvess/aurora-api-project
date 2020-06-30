using System;

namespace Aurora.Domain.Models
{
    public class UpdateUserModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        public string Cpf { get; set; }
    }
}
