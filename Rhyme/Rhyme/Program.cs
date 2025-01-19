using Rhyme.Data;
using Rhyme.FileIO;

namespace Rhyme;

public class Program
{
    public static async Task Main(string[] args)
    {
        using var db = new AppDbContext();
        await Seeder.Seed(db);

        string sampleText = FileUtils.GetLyrics("sample-text.txt");
        int[] numberOfWordsEachLine = FileUtils.GetNumberOfWordsByLineList(sampleText);
        var words = FileUtils.GetWordsList(sampleText);
        List<string[]> phonemesByWord = new List<string[]>();
        foreach (string word in words)
        {
            var foundRhyme = db.Rhymes.FirstOrDefault(x => x.Word.Equals(word.ToUpper()));
            if (foundRhyme is null)
            {
                phonemesByWord.Add([word]);
            }
            else
            {
                phonemesByWord.Add(foundRhyme.PhonemeSyllables);
            }
        }
        int syllablesCount = 0;
        foreach (string[] word in phonemesByWord)
        {
            syllablesCount += word.Length;
        }
        int[,] rhymeScoreMapper = new int[syllablesCount, syllablesCount];
        List<List<int>> rhymeMatchMapper = new();
        for (int i = 0; i < syllablesCount; i++)
        {
            rhymeMatchMapper.Add(new List<int>());
        }

        for (int outerIndex = 0; outerIndex < syllablesCount; outerIndex++)
        {
            for (int innerIndex = 0; innerIndex < syllablesCount; innerIndex++)
            {
                if (outerIndex != innerIndex)
                {
                    string firstSyllable = GetSyllableByIndex(outerIndex, phonemesByWord);
                    string secondSyllable = GetSyllableByIndex(innerIndex, phonemesByWord);
                    int score = ScoreSyllables(firstSyllable, secondSyllable);
                    if (score > 0)
                    {
                        rhymeMatchMapper[outerIndex].Add(innerIndex);
                    }
                    rhymeScoreMapper[innerIndex, outerIndex] = score;
                    rhymeScoreMapper[outerIndex, innerIndex] = score;
                }
            }
        }
    }

    public static int ScoreSyllables(string firstSyllable, string secondSyllable)
    {
        string[] vowels = [ "AA", "AE", "AH", "AO", "AW", "AY", "EH", 
            "ER", "EY", "IH", "IY", "OW", "OY", "UH", "UW" ];
        int score = 0;
        string[] firstPhonemes = firstSyllable.Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        string[] secondPhonemes = secondSyllable.Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        for (int i = 0; i < firstPhonemes.Length; i++)
        {
            string phonemeWithoutStress = firstPhonemes[i].Substring(0, 2);
            if (vowels.Contains(phonemeWithoutStress))
            {
                string? equalPhoneme = secondPhonemes.FirstOrDefault(x => x.StartsWith(phonemeWithoutStress));
                if (equalPhoneme is not null)
                {
                    return 1;
                }
            }
        }
        return 0;
    }

    public static string GetSyllableByIndex(int index, List<string[]> syllables)
    {
        int counter = 0;
        foreach (string[] word in syllables)
        {
            foreach (string syllable in word)
            {
                if (counter == index)
                {
                    return syllable;
                }
                counter++;
            }
        }
        // change this when you refactor to check against member property containing number of syllables (at start of method)
        throw new ArgumentException("Index is greater than or equal to number of syllables in words.");
    }
}
