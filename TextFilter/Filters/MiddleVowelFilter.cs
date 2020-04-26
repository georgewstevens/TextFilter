using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Test")]

namespace TextFilter.Filters
{
    public class MiddleVowelFilter : AbstractWordFilter, IWordFilter
    {
        private readonly char[] vowels = {'a', 'e', 'i', 'o', 'u'};

        protected override bool IsValidWord(string word)
        {
            var middleChars = GetMiddleChars(word);
            return !ContainsVowels(middleChars);
        }

        internal string GetMiddleChars(string word)
        {
            // edge case: empty string
            if(word.Length == 0)
            {
                return word;
            }

            // odd length
            if(word.Length % 2 == 1)
            {
                var letterIndex = (word.Length / 2);
                return word.Substring(letterIndex, 1);
            }
            // even length
            else
            {
                var letterIndex = (word.Length / 2) - 1;
                return word.Substring(letterIndex, 2);
            }
        }

        internal bool ContainsVowels(string word)
        {
            word = word.ToLower();
            return vowels.Any(vowel => word.Contains(vowel));
        }
    }
}
