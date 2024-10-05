namespace JapaneseLearningHelper.Model.Interface;

public interface IMenu
{
    public string Name { get; set; }
    public List<IOption> Options { get; set; }
}
