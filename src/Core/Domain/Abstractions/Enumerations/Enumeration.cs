using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Domain.Abstractions.Enumerations;

public abstract record Enumeration : IComparable
{
    public Enumeration(int id, string name)
        => (Id, Name) = (id, name);
    
    public int Id { get; private set; }

    public string Name { get; private set; }

    public override string ToString() 
        => Name;

    public static IEnumerable<T> GetAll<T>() 
        where T : Enumeration
        => typeof(T).GetFields(
            BindingFlags.Public |
            BindingFlags.Static |
            BindingFlags.DeclaredOnly)
                .Select(f => f.GetValue(null)).Cast<T>();

    public int CompareTo(object obj)
        => Id.CompareTo(((Enumeration)obj).Id);
}