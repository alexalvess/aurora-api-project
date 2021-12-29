using Domain.Abstractions.ValueTypes;
using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;

namespace Domain.ValueTypes;

public struct Nin : IValueType<string>
{
    private string _nin;
    private readonly List<ValidationFailure> _errors;

    private Nin(string nin)
        : this()
    {
        _nin = nin;
        _errors = new List<ValidationFailure>();
        Validate();
    }

    public bool IsValid { get => Errors?.Any() ?? true; }

    public IReadOnlyCollection<ValidationFailure> Errors { get; private set; }

    public string Value => _nin;

    public override string ToString() =>
        _nin;

    public static implicit operator Nin(string nin) =>
        new Nin(nin);

    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(_nin))
            _errors.Add(new ValidationFailure(GetType().Name, "Is necessary to inform the CPF."));

        int[] multiplierOne = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplierTwo = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        string aux;
        string digit;
        int sum, rest;

        var value = _nin.Trim();
        value = _nin.Replace(".", "").Replace("-", "");

        if (value.Length != 11)
            _errors.Add(new ValidationFailure(GetType().Name, "CPF should have 11 chars."));

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
            _errors.Add(new ValidationFailure(GetType().Name, "This CPF is invalid."));

        Errors = _errors;
    }

    public void Create(object value)
    {
        _nin = value.ToString();
        Validate();
    }
}
