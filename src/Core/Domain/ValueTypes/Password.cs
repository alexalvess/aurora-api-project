using Domain.Abstractions.ValueTypes;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Domain.ValueTypes;

public struct Password : IValueType<string>
{
    private string _password;
    private readonly List<ValidationFailure> _errors;

    private Password(string password)
        : this()
    {
        _password = password;
        _errors = new List<ValidationFailure>();
        Validate();
        Errors = _errors;
    }

    public bool IsValid => Errors?.Any() is false;

    public IReadOnlyCollection<ValidationFailure> Errors { get; private set; }

    public string Value => _password;

    public override string ToString() =>
        _password;

    public static implicit operator Password(string password) =>
        new Password(password);

    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(_password))
        {
            _errors.Add(new ValidationFailure(GetType().Name, "Is necessary to inform the Password."));
            return;
        }

        if (_password.Length < 6)
        {
            _errors.Add(new ValidationFailure(GetType().Name, "The password must have more than 6 chars."));
            return;
        }

        if (Regex.IsMatch(_password, (@"[^a-zA-Z0-9]")))
        {
            _errors.Add(new ValidationFailure(GetType().Name, "The password must not have any special char."));
            return;
        }

        using var sha = SHA256.Create();
        var textData = sha.ComputeHash(Encoding.UTF8.GetBytes(_password));
        var hash = sha.ComputeHash(textData);
        _password = BitConverter.ToString(hash).Replace("-", String.Empty);
    }

    public void Create(object value)
    {
        _password = value.ToString();
        Validate();
    }
}