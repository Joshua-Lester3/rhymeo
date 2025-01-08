namespace Rhyme.FileIO;

public class FileUtils
{
    private static readonly string[] _Delimiters = ["\r\n", "\r", "\n", " "];
    public static string GetLyrics(string path)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(path);
        string adjustedPath = Path.Combine("..", "..", "..", path);
        if (!File.Exists(adjustedPath))
        {
            throw new FileNotFoundException(adjustedPath);
        }
        return File.ReadAllText(adjustedPath);
    }

    public static IEnumerable<string> GetWordsList(string lyrics)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(lyrics);

        return lyrics.Split(_Delimiters,
            StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries); ;
    }

    public static IEnumerable<string> GetCmuDict(string? path = null)
    {
        path ??= Path.Combine("..", "..", "..", "cmudict.txt");
        if (path.Trim().Length is 0)
        {
            throw new ArgumentException($"{nameof(path)} cannot be empty or whitespace.");
        }
        if (!File.Exists(path))
        {
            throw new FileNotFoundException(path);
        }
        List<string> result = [];
        using (FileStream fileStream = File.Open(path, FileMode.Open)) 
        {
            using (StreamReader sr = new StreamReader(fileStream))
            {
                string? line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (!line.StartsWith(";;;"))
                    {
                        result.Add(line);
                    }
                }
            }
        }

        return result;
    }
}
