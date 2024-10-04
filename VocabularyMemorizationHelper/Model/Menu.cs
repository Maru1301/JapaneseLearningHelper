using Menu_Practice.Model.Interface;

namespace Menu_Practice.Model;

public class Menu : IMenu
{
    public string Name { get; set; } = string.Empty;
    public List<IOption> Options { get; set; } = [];
}
