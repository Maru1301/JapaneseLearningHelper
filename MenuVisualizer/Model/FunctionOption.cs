using JapaneseLearningHelper.Model.Interface;

namespace JapaneseLearningHelper.Model;

public class FunctionOption : IOption
{
    public string Name { get; set; } = string.Empty;

    public required Delegate Func { get; set; }
}
