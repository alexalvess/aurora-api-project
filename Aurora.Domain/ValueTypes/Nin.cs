using Flunt.Validations;

namespace Aurora.Domain.ValueTypes
{
    public struct Nin
    {
        private readonly string _value;
        public readonly Contract contract;

        private Nin(string value)
        {
            _value = value;
            contract = new Contract();
            Validate();
        }

        public override string ToString() =>
            _value;

        public static implicit operator Nin(string input) =>
            new Nin(input);

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(_value))
            {
                contract.AddNotification(nameof(Nin), "Is necessary to inform the CPF.");
                return;
            }

            int[] multiplierOne = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplierTwo = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string aux;
            string digit;
            int sum, rest;

            var value = _value.Trim();
            value = _value.Replace(".", "").Replace("-", "");

            if (value.Length != 11)
            {
                contract.AddNotification(nameof(Nin), "CPF should have 11 chars.");
                return;
            }

            aux = value.Substring(0, 9);
            sum = 0;

            for (int i = 0; i < 9; i++)
                sum += int.Parse(aux[i].ToString()) * multiplierOne[i];

            rest = sum % 11;

            if (rest < 2)
                rest = 0;
            else
                rest = 11 - rest;

            digit = rest.ToString();
            aux = aux + digit;
            sum = 0;

            for (int i = 0; i < 10; i++)
                sum += int.Parse(aux[i].ToString()) * multiplierTwo[i];

            rest = sum % 11;

            if (rest < 2)
                rest = 0;
            else
                rest = 11 - rest;

            digit = digit + rest.ToString();

            if (!value.EndsWith(digit))
                contract.AddNotification(nameof(Nin), "This CPF is invalid.");
        }
    }
}
