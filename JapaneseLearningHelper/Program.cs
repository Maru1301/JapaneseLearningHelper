using JapaneseLearningHelper.Model;
using MenuVisualizer;
using VocabularyMemorizationHelper;

namespace JapaneseLearningHelper
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var menu = InitializeMenu();

            ConsoleMenuManager visualizer = new();

            visualizer.Construct(menu);

            visualizer.Show();
        }

        private static Menu InitializeMenu()
        {
            var test = new VocTest();
            return new Menu()
            {
                Name = "MainMenu",
                Options =
                [
                    new FunctionOption()
                {
                    Name = "VocTest",
                    Func = () => test.Start()
                },
                new FunctionOption(){
                    Name = "Exit",
                    Func = ()=> Environment.Exit(0)
                }
                ]
            };
        }
    }
}