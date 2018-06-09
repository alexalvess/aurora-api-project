using System;
using System.Collections.Generic;
using System.Text;

namespace Modelo.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        public string Cpf { get; set; }
    }
}

