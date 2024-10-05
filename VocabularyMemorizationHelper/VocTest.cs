using System.Text;
using VocabularyMemorizationHelper.Ext;

namespace VocabularyMemorizationHelper;

public class VocTest
{
    public List<KeyValuePair<List<string>, JapaneseSet>> Start()
    {
        return Start([]);
    }

    public List<KeyValuePair<List<string>, JapaneseSet>> Start(List<KeyValuePair<List<string>, JapaneseSet>> input)
    {
        Console.CursorVisible = true;
        Console.Clear();
        try
        {
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;

            List<KeyValuePair<List<string>, JapaneseSet>> pairList;
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
            List<KeyValuePair<List<string>, JapaneseSet>> wrong = [];
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

    private void ShowResult(List<KeyValuePair<List<string>, JapaneseSet>> wrong)
    {
        if (wrong.Count != 0)
        {
            Console.WriteLine("錯誤清單");
            foreach (var c in wrong)
            {
                Console.WriteLine($"{c.Value.Kanji} {c.Value.Kana} : {string.Join(',', c.Key)}");
            }
        }
        else
        {
            Console.WriteLine("太神啦，全對");
        }
    }

    private List<KeyValuePair<List<string>, JapaneseSet>> JapantoChi(List<KeyValuePair<List<string>, JapaneseSet>> pairList, int changeQuestionPoint)
    {
        List<KeyValuePair<List<string>, JapaneseSet>> wrong = [];
        Console.WriteLine("日翻中");
        for (int i = changeQuestionPoint; i < pairList.Count; i++)
        {
            var c = pairList[i];
            Console.WriteLine($"{i + 1}. {c.Value.Kanji} {c.Value.Kana}");
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

    private List<KeyValuePair<List<string>, JapaneseSet>> ChitoJapan(List<KeyValuePair<List<string>, JapaneseSet>> pairList, int changeQuestionPoint)
    {
        List<KeyValuePair<List<string>, JapaneseSet>> wrong = [];
        Console.WriteLine("中翻日");
        for (int i = 0; i < changeQuestionPoint; i++)
        {
            var c = pairList[i];
            Console.WriteLine($"{i + 1}. {string.Join(',', c.Key)}");
            Console.Write("答");
            var ans = Console.ReadLine();
            if (c.Value.Contains(ans) == false)
            {
                Console.WriteLine("錯誤");
                wrong.Add(new(c.Key, c.Value));
            }
        }

        return wrong;
    }

    private Dictionary<List<string>, JapaneseSet> ReadInput()
    {
        string context;
        Dictionary<List<string>, JapaneseSet> chineseJapanesePairs = [];
        while ((context = Console.ReadLine()!) != "done")
        {
            var res = FindChineseJapanesePair(context);
            if (res.chinese.Count != 0)
            {
                chineseJapanesePairs.Add(res.chinese, res.japaneseSet);
            }
        }

        return chineseJapanesePairs;
    }

    private static (List<string> chinese, JapaneseSet japaneseSet) FindChineseJapanesePair(string context)
    {
        if (string.IsNullOrEmpty(context)) return ([], new());
        if (context[0] == ' ' || context[0] == '\t') return ([], new());

        int dotIndex = context.IndexOf('.');
        int parenthesisIndex = context.IndexOf('：');
        var japaneseStr = context[(dotIndex + 1)..parenthesisIndex];
        var chineseStr = context[(parenthesisIndex + 1)..];

        var set = GetJapanese(japaneseStr);

        List<string> chineses = GetChinese(chineseStr);

        return (chineses, set);
    }

    private static JapaneseSet GetJapanese(string japaneseStr)
    {
        bool inParen = false;
        var kanji = string.Empty;
        string kana = string.Empty;
        foreach (var c in japaneseStr)
        {
            if (c == '(')
            {
                inParen = true;
            }
            else if (c == ')')
            {
                break;
            }
            else if (inParen)
            {
                kana += c;
            }
            else
            {
                kanji += c;
            }
        }

        return new(kanji.Trim(), kana.Trim());
    }

    private static List<string> GetChinese(string chineseStr)
    {
        List<string> chineses = [];
        string chinese = string.Empty;
        foreach (var c in chineseStr)
        {
            if (c == '(' || c == '\n')
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

        return chineses;
    }

    public class JapaneseSet
    {
        public string Kanji { get; set; } = string.Empty;

        public string Kana { get; set; } = string.Empty;

        public JapaneseSet()
        {
            
        }

        public JapaneseSet(string kanji, string kana)
        {
            Kanji = kanji;
            Kana = kana;
        }
    }
}
