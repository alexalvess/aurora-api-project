using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Aurora.Domain.ValueTypes
{
    public struct Password
    {
        private readonly string _value;
        public readonly Contract contract;

        private Password(string value)
        {
            _value = value;
            contract = new Contract();
            Validate();

            if(contract.Valid)
                _value = Convert.ToBase64String(new UTF8Encoding().GetBytes(_value));
        }

        public override string ToString() =>
            _value;

        public static implicit operator Password(string input) =>
            new Password(input.Trim());

        private bool Validate()
        {
            if (string.IsNullOrWhiteSpace(_value))
                return AddNotification("Is necessary to inform the Password.");

            if(_value.Length < 6)
                return AddNotification("The password must have more than 6 chars.");

            if (Regex.IsMatch(_value, (@"[^a-zA-Z0-9]")))
                return AddNotification("The password must not have any special char.");

            return true;
        }

        private bool AddNotification(string message)
        {
            contract.AddNotification(nameof(Password), message);
            return false;
        }
    }
}
