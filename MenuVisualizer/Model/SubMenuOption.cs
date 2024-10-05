using JapaneseLearningHelper.Model.Interface;

namespace JapaneseLearningHelper.Model;

public class SubMenuOption : IOption
{
    public string Name { get; set; } = string.Empty;
    public Menu? SubMenu { get; set; }
}
