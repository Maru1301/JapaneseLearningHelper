using JapaneseLearningHelper.Model.Interface;

namespace JapaneseLearningHelper.Model;

public class Menu : IMenu
{
    public string Name { get; set; } = string.Empty;
    public List<IOption> Options { get; set; } = [];
}
