using Rhyme.Utils;

namespace Rhyme.Tests;

[TestClass]
public class FileUtilsTests
{
    [TestMethod]
    public void GetLyrics_ValidPath_ReturnsNonNullNonEmptyString()
    {
        // Arrange
        string path = "/hello";

        // Act
        string lyrics = FileUtils.GetLyrics(path);

        // Assert
        Assert.IsFalse(string.IsNullOrEmpty(lyrics));
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
}
