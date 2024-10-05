namespace VocabularyMemorizationHelper.Ext;

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
