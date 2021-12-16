using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Application.Envelop;

public record Response
{
    private List<string> _errors;

    public Response(object data = default)
        => Data = data;

    public Response(string errorMessage)
        => AddError(errorMessage);

    public Response(IList<string> errorMessages)
        => AddErrors(errorMessages);

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public object Data { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IReadOnlyCollection<string> Errors
        => _errors?.AsReadOnly();

    public void AddError(string errorMessage)
    {
        if(_errors is null)
            _errors = new List<string>();

        _errors.Add(errorMessage);
    }

    public void AddErrors(IList<string> errorMessages)
    {
        if (_errors is null)
            _errors = new List<string>();

        _errors.AddRange(errorMessages);
    }
}