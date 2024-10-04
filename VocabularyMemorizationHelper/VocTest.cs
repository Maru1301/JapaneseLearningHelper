using System.Text;

namespace VocabularyMemorizationHelper;

public class VocTest
{
    public List<KeyValuePair<List<string>, string>> Start()
    {
        return Start([]);
    }

    public List<KeyValuePair<List<string>, string>> Start(List<KeyValuePair<List<string>, string>> input)
    {
        Console.CursorVisible = true;
        Console.Clear();
        try
        {
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;

            List<KeyValuePair<List<string>, string>> pairList;
            if (input.Count == 0)
            {
                var chineseJapanesePairs = ReadInput();
                pairList = [.. chineseJapanesePairs];
            }
            else
            {
                pairList = input;
            }

            pairList = pairList.Randomize();

            int count = pairList.Count;
            Console.WriteLine($"總數:{count}");
            List<KeyValuePair<List<string>, string>> wrong = [];
            int changeQuestionPoint = count % 2 == 1 ? count / 2 + 1 : count / 2;

            var tempWrong = ChitoJapan(pairList, changeQuestionPoint);
            wrong.AddRange(tempWrong);

            tempWrong = JapantoChi(pairList, changeQuestionPoint);
            wrong.AddRange(tempWrong);

            ShowResult(wrong);

            Console.Read();

            return wrong;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.Read();

            return [];
        }
    }

    private void ShowResult(List<KeyValuePair<List<string>, string>> wrong)
    {
        if (wrong.Count != 0)
        {
            Console.WriteLine("錯誤清單");
            foreach (var c in wrong)
            {
                Console.WriteLine($"{c.Value} : {string.Join(',', c.Key)}");
            }
        }
        else
        {
            Console.WriteLine("太神啦，全對");
        }
    }

    private List<KeyValuePair<List<string>, string>> JapantoChi(List<KeyValuePair<List<string>, string>> pairList, int changeQuestionPoint)
    {
        List<KeyValuePair<List<string>, string>> wrong = [];
        Console.WriteLine("日翻中");
        for (int i = changeQuestionPoint; i < pairList.Count; i++)
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

        return wrong;
    }

    private List<KeyValuePair<List<string>, string>> ChitoJapan(List<KeyValuePair<List<string>, string>> pairList, int changeQuestionPoint)
    {
        List<KeyValuePair<List<string>, string>> wrong = [];
        Console.WriteLine("中翻日");
        for (int i = 0; i < changeQuestionPoint; i++)
        {
            var c = pairList[i];
            Console.WriteLine($"{i + 1}. {string.Join(',', c.Key)}");
            Console.Write("答");
            var ans = Console.ReadLine();
            if (ans != c.Value)
            {
                Console.WriteLine("錯誤");
                wrong.Add(new(c.Key, c.Value));
            }
        }

        return wrong;
    }

    private Dictionary<List<string>, string> ReadInput()
    {
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

        return chineseJapanesePairs;
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
        var japaneseStr = context[(dotIndex + 1)..parenthesisIndex];
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
        while (tempCount != count)
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