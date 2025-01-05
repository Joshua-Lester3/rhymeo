namespace Rhyme.Utils;

public class FileUtils
{
    public static string GetLyrics(string path)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(path);
        return "hi";
        //return File.ReadAllText(path);
    }
}
