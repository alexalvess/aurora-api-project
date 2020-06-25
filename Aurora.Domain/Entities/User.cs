using System;

namespace Aurora.Domain.Entities
{
    public class User : BaseEntity<int>
    {
        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        public string Cpf { get; set; }
    }
}

