using Menu_Practice.Model.Interface;

namespace Menu_Practice.Model;

public class FunctionOption : IOption
{
    public string Name { get; set; } = string.Empty;

    public required Delegate Func { get; set; }
}
