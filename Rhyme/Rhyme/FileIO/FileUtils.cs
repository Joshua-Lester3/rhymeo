namespace Rhyme.FileIO;

public class FileUtils
{
    private static readonly string[] _Delimiters = ["\r\n", "\r", "\n", " "];
    public static string GetLyrics(string path)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(path);
        string adjustedPath = $"../../../{path}";
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

    public static IEnumerable<string> GetCmuDict(string path = "../../../cmudict.txt")
    {
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
