using Microsoft.VisualStudio.TestTools.UnitTesting;
using VocabularyMemorizationHelper;
using System.Collections.Generic;

namespace VocabularyMemorizationHelper.Tests
{
    [TestClass]
    public class VocTestParserTests
    {
        [TestMethod]
        public void GetJapanese_ShouldParseKanjiAndKana()
        {
            // Arrange
            var input = "日本語(にほんご)";
            var expectedKanji = "日本語";
            var expectedKana = "にほんご";

            // Act
            var result = VocTest.GetJapanese(input);

            // Assert
            Assert.AreEqual(expectedKanji, result.Kanji);
            Assert.AreEqual(expectedKana, result.Kana);
        }

        [TestMethod]
        public void GetJapanese_ShouldHandleExtraSpaces()
        {
            // Arrange
            var input = "  日本語  (  にほんご  )  ";
            var expectedKanji = "日本語";
            var expectedKana = "にほんご";

            // Act
            var result = VocTest.GetJapanese(input);

            // Assert
            Assert.AreEqual(expectedKanji, result.Kanji);
            Assert.AreEqual(expectedKana, result.Kana);
        }

        [TestMethod]
        public void GetJapanese_ShouldHandleOnlyKanji()
        {
            // Arrange
            var input = "日本語";
            var expectedKanji = "日本語";
            var expectedKana = "";

            // Act
            var result = VocTest.GetJapanese(input);

            // Assert
            Assert.AreEqual(expectedKanji, result.Kanji);
            Assert.AreEqual(expectedKana, result.Kana);
        }

        [TestMethod]
        public void GetChinese_ShouldParseSingleMeaning()
        {
            // Arrange
            var input = "中文";
            var expected = new List<string> { "中文" };

            // Act
            var result = VocTest.GetChinese(input);

            // Assert
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetChinese_ShouldParseMultipleMeanings()
        {
            // Arrange
            var input = "意思一，意思二，意思三";
            var expected = new List<string> { "意思一", "意思二", "意思三" };

            // Act
            var result = VocTest.GetChinese(input);

            // Assert
            CollectionAssert.AreEqual(expected, result);
        }
        
        [TestMethod]
        public void GetChinese_ShouldHandleTrailingContentInParentheses()
        {
            // Arrange
            var input = "中文(某某)";
            var expected = new List<string> { "中文" };

            // Act
            var result = VocTest.GetChinese(input);

            // Assert
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void FindChineseJapanesePair_ShouldParseCorrectly()
        {
            // Arrange
            var input = "1.日本語(にほんご)：中文，日文";
            var expectedKanji = "日本語";
            var expectedKana = "にほんご";
            var expectedChinese = new List<string> { "中文", "日文" };

            // Act
            var result = VocTest.FindChineseJapanesePair(input);

            // Assert
            Assert.AreEqual(expectedKanji, result.japaneseSet.Kanji);
            Assert.AreEqual(expectedKana, result.japaneseSet.Kana);
            CollectionAssert.AreEqual(expectedChinese, result.chinese);
        }

        [TestMethod]
        public void FindChineseJapanesePair_ShouldReturnEmptyForInvalidInput()
        {
            // Arrange
            var input = " just some random text ";

            // Act
            var result = VocTest.FindChineseJapanesePair(input);

            // Assert
            Assert.AreEqual(0, result.chinese.Count);
            Assert.AreEqual(string.Empty, result.japaneseSet.Kanji);
        }
    }
}
