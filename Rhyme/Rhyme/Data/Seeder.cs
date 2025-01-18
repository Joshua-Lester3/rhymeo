using Rhyme.FileIO;
using Rhyme.Models;

namespace Rhyme.Data;

public class Seeder
{
    public static async Task Seed(AppDbContext db)
    {
        if (!db.WordsWithPhonemes.Any())
        {
            var cmuWordsWithPhonemes = FileUtils.GetCmuDict();
            foreach (var cmuWordWithPhonemes in cmuWordsWithPhonemes)
            {
                string[] pair = cmuWordWithPhonemes.Split("  ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                if (pair.Length != 2)
                {
                    throw new InvalidOperationException("Cmu Dictionary operation: splitting" +
                        " by word-phonemes delimiter does not result in two strings.");
                }
                WordWithPhonemes wordWithPhonemes = new()
                {
                    Word = pair[0],
                    Phonemes = pair[1].Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                };
                Console.WriteLine(wordWithPhonemes.Word);
                await db.WordsWithPhonemes.AddAsync(wordWithPhonemes);
            }
            await db.SaveChangesAsync();
        }
    }
}
