using System.Text;

namespace VocabularyMemoryHelper
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.InputEncoding = Encoding.Unicode;
                Console.OutputEncoding = Encoding.Unicode;
                string context;
                Dictionary<List<string>, string> chineseJapanesePairs = [];
                while ((context = Console.ReadLine()!) != "done")
                {
                    var res = context.FindChineseJapanesePair();
                    if (string.IsNullOrEmpty(res.japanese) == false)
                    {
                        chineseJapanesePairs.Add(res.chinese, res.japanese);
                    }
                }

                var pairList = chineseJapanesePairs.ToList();
                pairList = pairList.Randomize();

                int count = pairList.Count;
                Console.WriteLine($"總數:{count}");
                List<KeyValuePair<List<string>, string>> wrong = [];
                int changeQuestionPoint = count % 2 == 1 ? count / 2 + 1 : count;
                
                Console.WriteLine("中翻日");
                for (int i = 0; i < changeQuestionPoint; i++)
                {
                    var c = pairList[i];
                    Console.WriteLine($"{i+1}. {string.Join(',', c.Key)}");
                    Console.Write("答");
                    var ans = Console.ReadLine();
                    if (ans != c.Value)
                    {
                        Console.WriteLine("錯誤");
                        wrong.Add(new(c.Key, c.Value));
                    }
                }

                Console.WriteLine("日翻中");
                for (int i = changeQuestionPoint; i < count; i++)
                {
                    var c = pairList[i];
                    Console.WriteLine($"{i + 1}. {c.Value}");
                    Console.Write("答");
                    var ans = Console.ReadLine();
                    var chiAns = c.Key;

                    if (!chiAns.Contains(ans!))
                    {
                        Console.WriteLine("錯誤");
                        wrong.Add(new(c.Key, c.Value));
                    }
                }

                if (wrong.Any())
                {
                    Console.WriteLine("錯誤清單");
                    foreach (var c in wrong)
                    {
                        Console.WriteLine($"{c.Key} : {c.Value}");
                    }
                }

                Console.Read();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Read();
            }
        }
    }

    public static class StrExt
    {
        public static (List<string> chinese, string japanese) FindChineseJapanesePair(this string context)
        {
            if (string.IsNullOrEmpty(context)) return ([], string.Empty);
            if (context[0] == ' ' || context[0] == '\t') return ([], string.Empty);

            int dotIndex = context.IndexOf('.');
            int parenthesisIndex = context.IndexOf('：');
            var japaneseStr = context[(dotIndex+1)..parenthesisIndex];
            var chineseStr = context[(parenthesisIndex + 1)..];

            bool isParen = false;
            string japanese = string.Empty;
            string parenthesesJapanese = string.Empty;
            foreach (var c in japaneseStr)
            {
                if (c == '(')
                {
                    isParen = true;
                }
                else if (c == ')')
                {
                    break;
                }
                else if (isParen)
                {
                    parenthesesJapanese += c;
                }
                else
                {
                    japanese += c;
                }
            }

            List<string> chineses = [];
            string chinese = string.Empty;
            foreach (var c in chineseStr)
            {
                if (c == '(')
                {
                    chineses.Add(chinese.Trim());
                    break;
                }
                else if (c == '，')
                {
                    chineses.Add(chinese.Trim());
                    chinese = string.Empty;
                }
                else
                {
                    chinese += c;
                }
            }

            return (chineses, string.IsNullOrEmpty(parenthesesJapanese) == false ? parenthesesJapanese.Trim() : japanese.Trim());
        }
    }

    public static class ListExt
    {
        public static List<T> Randomize<T>(this List<T> list)
        {
            int count = list.Count;
            List<T> newList = new();
            bool[] indexes = new bool[count];
            Random random = new();
            int tempCount = 0;
            while(tempCount != count)
            {
                int tempRandomVal = random.Next(0, count);
                if (indexes[tempRandomVal] == false)
                {
                    newList.Add(list[tempRandomVal]);
                    indexes[tempRandomVal] = true;
                    tempCount++;
                }
            }

            return newList;
        }
    }
}