using JapaneseLearningHelper.Model;

namespace MenuVisualizer
{
    public class ConsoleMenuManager
    {
        private Menu menu = new();
        public void Construct(Menu menu)
        {
            this.menu = menu;
        }

        public void Show()
        {
            Console.CursorVisible = false;

            try
            {
                int optionPointer = 0;
                object? result = null;
                bool isExit = false;
                while (!isExit)
                {
                    DisplayMenu(menu, optionPointer);
                    var key = Console.ReadKey().Key;

                    optionPointer = HandleInput(key, optionPointer, menu.Options.Count);

                    if (key == ConsoleKey.Enter)
                    {
                        if(menu.Options[optionPointer] is FunctionOption funcOption)
                        {
                            result = ExecuteOption(funcOption, result);
                            menu = funcOption.AfterFuncSubMenu ?? menu;
                        }
                        else if (menu.Options[optionPointer] is SubMenuOption subMenuOption)
                        {
                            menu = subMenuOption.SubMenu!;
                        }

                        optionPointer = 0;
                    }

                    if(result is OptionDefault && result != null)
                    {
                        isExit = (OptionDefault)result == OptionDefault.Exit;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
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

        private static object? ExecuteOption(FunctionOption option, object? input)
        {
            var result = option.Func.DynamicInvoke(input);
            Console.CursorVisible = false;
            Console.Clear();

            return result;
        }
    }
}
