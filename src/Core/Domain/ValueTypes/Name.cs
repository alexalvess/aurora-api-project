using Domain.Abstractions.ValueTypes;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Domain.ValueTypes;

public struct Name : IValueType<string>
{
    private string _name;
    private readonly List<ValidationFailure> _errors;

    private Name(string name)
        : this()
    {
        _name = name;
        _errors = new List<ValidationFailure>();
        Validate();
    }

    public bool IsValid => Errors?.Any() is false;

    public IReadOnlyCollection<ValidationFailure> Errors { get; private set; }

    public string Value => _name;

    public override string ToString() =>
        _name;

    public static implicit operator Name(string name) =>
        new Name(name);

    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(_name))
            _errors.Add(new ValidationFailure(GetType().Name, "Inform a valid name."));

        if (_name.Length < 10)
            _errors.Add(new ValidationFailure(GetType().Name, "The name must have more than 10 chars."));

        if (!Regex.IsMatch(_name, (@"[^a-zA-Z0-9]")))
            _errors.Add(new ValidationFailure(GetType().Name, "The name must not have any special char."));

        Errors = _errors;
    }

    public void Create(object value)
    {
        _name = value.ToString();
        Validate();
    }
}
