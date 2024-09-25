using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace VocabularyMemoryHelper
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;
            string context;
            Dictionary<string, string> chineseJapanesePairs = [];
            while((context = Console.ReadLine()!) != "done")
            {
                (var chinese, var japanese ) = context.FindChineseJapanesePair();
                if(string.IsNullOrEmpty(chinese) == false)
                {
                    chineseJapanesePairs.Add(chinese, japanese);
                }
            }

            Console.WriteLine($"總數:{chineseJapanesePairs.Count}");
            Dictionary<string, string> wrong = [];
            foreach (var c in chineseJapanesePairs)
            {
                Console.WriteLine(c.Key);
                Console.Write("答");
                var ans = Console.ReadLine();
                if(ans != c.Value)
                {
                    Console.WriteLine("錯誤");
                    wrong.Add(c.Key, c.Value);
                }
            }

            Console.WriteLine("錯誤清單");
            foreach(var c in wrong)
            {
                Console.WriteLine($"{c.Key} : {c.Value}");
            }

            Console.Read();
        }
    }

    public static class StrExt
    {
        public static (string, string) FindChineseJapanesePair(this string context)
        {
            if (context[0] == ' ' || context[0] == '\t') return (string.Empty, string.Empty);
            string chinese = string.Empty;
            string japanese = string.Empty;
            string parenthesesJapanese = string.Empty;
            bool isChi = false;
            bool isJa = false;
            bool isParen = false;
            foreach (var c in context)
            {
                if (c == '：')
                {
                    isChi = true;
                    isJa = false;
                    continue;
                }
                if (isParen)
                {
                    if(c == ')')
                    {
                        isParen = false;
                    }
                    else
                    {
                        parenthesesJapanese += c;
                    }
                }
                else if (isJa)
                {
                    if (c == '(')
                    {
                        isParen = true;
                    }
                    else
                    {
                        japanese += c;
                    }
                }
                else if (isChi)
                {
                    if(c == '\n' || c == '(')
                    {
                        break;
                    }
                    else
                    {
                        chinese += c;
                    }
                }
                else if(c == '.')
                {
                    isJa = true;
                }
            }

            return (chinese.Trim(), string.IsNullOrEmpty(parenthesesJapanese) == false ? parenthesesJapanese.Trim() : japanese.Trim());
        }
    }
}