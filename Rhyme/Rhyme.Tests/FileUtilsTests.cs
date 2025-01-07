using Rhyme.FileIO;

namespace Rhyme.Tests;

[TestClass]
public class FileUtilsTests
{
    #region GetLyrics tests
    [TestMethod]
    public void GetLyrics_ValidPath_ReturnsNonNullNonEmptyString()
    {
        // Arrange

        // Act
        string result = FileUtils.GetLyrics("../Rhyme.Tests/test-file.txt");

        // Assert
        Assert.IsFalse(string.IsNullOrEmpty(result));
    }

    [TestMethod]
    [DataRow(null!)]
    public void GetLyrics_NullPath_ThrowsArgumentNullException(string path)
    {
        // Arrange

        // Act

        // Assert
        Assert.ThrowsException<ArgumentNullException>(() => FileUtils.GetLyrics(path));
    }

    [TestMethod]
    [DataRow("")]
    [DataRow("  ")]
    public void GetLyrics_EmptyOrWhiteSpacePath_ThrowsArgumentException(string path)
    {
        // Arrange

        // Act

        // Assert
        Assert.ThrowsException<ArgumentException>(() => FileUtils.GetLyrics(path));
    }


    [TestMethod]
    public void GetLyrics_InvalidPath_ThrowsFileNotFoundException()
    {
        // Arrange

        // Act
        string path = "../Rhyme.Tests/DOES-NOT-EXIST.txt";

        // Assert
        Assert.ThrowsException<FileNotFoundException>(() => FileUtils.GetLyrics(path));
    }
    #endregion GetLyrics tests

    #region GetWordsList tests

    [TestMethod]
    [DataRow("")]
    [DataRow("   ")]
    public void GetWordsList_EmptyOrWhiteSpaceArgument_ThrowsArgumentException(string lyrics)
    {
        // Arrange

        // Act

        // Assert
        Assert.ThrowsException<ArgumentException>(() => FileUtils.GetWordsList(lyrics));
    }

    [TestMethod]
    public void GetWordsList_NullArgument_ThrowsArgumentException()
    {
        // Arrange

        // Act

        // Assert
        Assert.ThrowsException<ArgumentNullException>(() => FileUtils.GetWordsList(null!));
    }

    [TestMethod]
    [DataRow("hello\ni'm mark", new string[] { "hello", "i'm", "mark" })]
    [DataRow("hello\ni'm jimbob the third  ", new string[] { "hello", "i'm", "jimbob", "the", "third" })]
    [DataRow("hello\ni'm mark the fourth   \n", new string[] { "hello", "i'm", "mark", "the", "fourth" })]
    [DataRow("\nhello\ni'm mark the fourth   \n", new string[] { "hello", "i'm", "mark", "the", "fourth" })]

    public void GetWordsList_DifferingAmountOfLinesAndWords_ReturnsCorrectWordsAndCount(string lyrics, string[] result)
    {
        // Arrange


        // Act
        var wordsList = FileUtils.GetWordsList(lyrics).ToList();

        // Assert
        CollectionAssert.AreEquivalent(result, wordsList);
    }

    #endregion GetWordsList tests

    [TestMethod]
    public void GetCmuDict_Success()
    {
        // Arrange

        // Act
        IEnumerable<string> dict = FileUtils.GetCmuDict("../../../../Rhyme/cmudict.txt");

        // Assert
        Assert.AreEqual(133311, dict.Count());
    }
}
