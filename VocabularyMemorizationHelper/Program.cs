using Menu_Practice.Model;

namespace VocabularyMemorizationHelper
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.CursorVisible = false;

            try
            {
                var menu = InitializeMenu();

                int optionPointer = 0;

                while (true)
                {
                    DisplayMenu(menu, optionPointer);
                    var key = Console.ReadKey().Key;

                    optionPointer = HandleInput(key, optionPointer, menu.Options.Count);

                    if (key == ConsoleKey.Enter && menu.Options[optionPointer] is FunctionOption funcOption)
                    {
                        ExecuteOption(funcOption);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
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

        private static void DisplayMenu(Menu menu, int optionPointer)
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(menu.Name);

            for (int i = 0; i < menu.Options.Count; i++)
            {
                if (i == optionPointer)
                {
                    Console.WriteLine($"=> {menu.Options[i].Name}");
                }
                else
                {
                    Console.WriteLine($"   {menu.Options[i].Name}");
                }
            }
        }

        private static int HandleInput(ConsoleKey key, int optionPointer, int optionsCount)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow:
                case ConsoleKey.W:
                    if (optionPointer > 0)
                    {
                        optionPointer--;
                    }
                    break;

                case ConsoleKey.DownArrow:
                case ConsoleKey.S:
                    if (optionPointer < optionsCount - 1)
                    {
                        optionPointer++;
                    }
                    break;
            }

            return optionPointer;
        }

        private static void ExecuteOption(FunctionOption option)
        {
            option.Func.DynamicInvoke(null);
            Console.CursorVisible = false;
            Console.Clear();
        }
    }
}