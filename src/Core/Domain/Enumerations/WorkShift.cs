using Domain.Abstractions.Enumerations;

namespace Domain.Enumerations;

public record WorkShift : Enumeration
{
    public readonly static WorkShift NightShift = new(1, nameof(NightShift));
    public readonly static WorkShift MorningShift = new(2, nameof(MorningShift));
    public readonly static WorkShift DawnShift = new(3, nameof(DawnShift));

    private WorkShift(int id, string name) 
        : base(id, name) { }
}
