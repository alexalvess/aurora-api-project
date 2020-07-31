using Aurora.Domain.ValueTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aurora.Domain.Entities
{
    public class Manager : BaseEntity<int>
    {
        public Manager(Name name, Nin nin, Password password)
        {
            AddNotifications(
                name.contract,
                nin.contract,
                password.contract);

            if (Valid)
            {
                Name = name;
                Nin = nin;
                Password = password;
            }
        }

        protected Manager() { }

        public Name Name { get; }

        public Nin Nin { get; }

        public Password Password { get; }
    }
}
