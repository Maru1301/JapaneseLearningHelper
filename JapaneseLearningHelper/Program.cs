using JapaneseLearningHelper.Model;
using MenuVisualizer;
using VocabularyMemorizationHelper;
using static VocabularyMemorizationHelper.VocTest;

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

            var againMenu = new Menu()
            {
                Name = "==VocTest==",
                Options =
                [
                    new FunctionOption()
                    {
                        Name = "Again",
                        Func = (object? input) => test.Start((List<KeyValuePair<List<string>, JapaneseSet>>?)input)
                    },
                    new SubMenuOption()
                    {
                        Name = "Exit",
                    }
                ]
            };

            var mainMenu = new Menu()
            {
                Name = "==MainMenu==",
                Options =
                [
                    new FunctionOption()
                    {
                        Name = "VocTest",
                        Func = (object? input) => test.Start((List<KeyValuePair<List<string>, JapaneseSet>>?)input),
                        AfterFuncSubMenu = againMenu
                    },
                    new FunctionOption()
                    {
                        Name = "Exit",
                        Func = (object? input) => OptionDefault.Exit
                    },
                ]
            };

            ((FunctionOption)againMenu.Options.First(option => option.Name == "Again")).AfterFuncSubMenu = againMenu;
            ((SubMenuOption)againMenu.Options.First(option => option.Name == "Exit")).SubMenu = mainMenu;

            return mainMenu;
        }
    }
}