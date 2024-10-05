using static VocabularyMemorizationHelper.VocTest;

namespace VocabularyMemorizationHelper.Ext;

public static class JapaneseSetExt
{
    public static bool CheckSetAvailability(this JapaneseSet set)
    {
        if (string.IsNullOrEmpty(set.Kanji) && string.IsNullOrEmpty(set.Kana))
        {
            return false;
        }

        return true;
    }

    public static bool Contains(this JapaneseSet set, string? val)
    {
        if (val is null)
        {
            return false;
        }

        if (set.Kanji == val || set.Kana == val)
        {
            return true;
        }

        return false;
    }
}