namespace VocabularyMemorizationHelper
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var test = new VocTest();

            var res = test.Start();
            res = test.Start(res);
        }
    }
}