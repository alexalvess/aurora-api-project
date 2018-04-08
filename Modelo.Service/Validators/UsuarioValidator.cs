using FluentValidation;
using Modelo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modelo.Service.Validators
{
    public class UsuarioValidator : AbstractValidator<Usuario>
    {
        public UsuarioValidator()
        {
            RuleFor(c => c.Cpf)
                .NotEmpty().WithMessage("É necessário informar o CPF.")
                .NotNull().WithMessage("É necessário informar o CPF");

            RuleFor(c => c.DataNascimento)
                .NotEmpty().WithMessage("É necessário informar a data de nascimento.")
                .NotNull().WithMessage("É necessário informar a data de nascimento");

            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("É necessário informar o nome.")
                .NotNull().WithMessage("É necessário informar o nome");
        }
    }
}