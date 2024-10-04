using Menu_Practice.Model.Interface;

namespace Menu_Practice.Model;

public class SubMenuOption : IOption
{
    public string Name { get; set; } = string.Empty;
    public Menu? SubMenu { get; set; }
}
