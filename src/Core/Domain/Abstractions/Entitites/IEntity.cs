﻿using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstractions.Entities;

public interface IEntity<out TId>
    where TId : struct
{
    TId Id { get; }

    bool IsDeleted { get; }

    bool IsValid { get; }

    public IEnumerable<ValidationFailure> Errors { get; }
}