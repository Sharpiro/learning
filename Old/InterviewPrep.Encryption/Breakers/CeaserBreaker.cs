using System.Collections.Generic;
using System.Linq;
using InterviewPrep.Encryption.Cyphers;

namespace InterviewPrep.Encryption.Breakers
{
    public class CeaserBreaker
    {
        public string BreakByVowels(string cryptoText)
        {
            var vowelsHashset = new HashSet<char> { 'a', 'e', 'i', 'o', 'u', 'y' };
            var potentialsDictionary = new Dictionary<int, string>();
            var bestCandidateKey = int.MinValue;

            for (var i = 0; i < 26; i++)
            {
                var plainText = Ceaser.DecryptString(cryptoText, i);
                var vowelCount = plainText.Count(c => vowelsHashset.Contains(c));
                if (vowelCount <= bestCandidateKey) continue;
                bestCandidateKey = vowelCount;
                potentialsDictionary.Add(vowelCount, plainText);
            }
            potentialsDictionary.TryGetValue(bestCandidateKey, out string bestCandidateValue);
            return bestCandidateValue;
        }
    }
}