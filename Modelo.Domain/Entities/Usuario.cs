using System;
using System.Collections.Generic;
using System.Text;

namespace Modelo.Domain.Entities
{
    public class Usuario : BaseEntity
    {
        public string Nome { get; set; }

        public string DataNascimento { get; set; }

        public string Cpf { get; set; }
    }
}

