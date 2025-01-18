using Rhyme.Data;
using Rhyme.FileIO;

namespace Rhyme;

public class Program
{
    public static async Task Main(string[] args)
    {
        using var db = new AppDbContext();
        await Seeder.Seed(db);

        Console.WriteLine("\n\n\nTEST!!!!!!!\n\n\n");
        var result = db.Rhymes.Where(w => w.Word.StartsWith("Z")).Take(10);
        foreach (var word in result)
        {
            Console.WriteLine(word.Word);
        }
        //string sampleText = FileUtils.GetLyrics("sample-text.txt");
        //int[] numberOfWordsEachLine = FileUtils.GetNumberOfWordsByLineList(sampleText);
        //var words = FileUtils.GetWordsList(sampleText);
        //List<List<string>> phonemesByWord = new List<List<string>>();
        //foreach (string word in words)
        //{
        //    List<string> phonemesInWord = new List<string>();
        //    // get phonemes
        //    phonemesByWord.Add(phonemesInWord);
        //}
    }
}
