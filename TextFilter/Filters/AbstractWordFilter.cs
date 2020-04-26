using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TextFilter.Filters
{
    public abstract class AbstractWordFilter : IWordFilter
    {
        private const string wordRegexPattern = "[a-zA-Z]+";
        private const string separator = " ";

        public virtual string Filter(string input)
        {
            var wordRegex = new Regex(wordRegexPattern);
            var regexMatches = wordRegex.Matches(input);

            var filteredString = new StringBuilder();
            int inputStringIndex = 0;
            foreach(var foundWord in regexMatches.ToList())
            {
                // add any text between previous word and current one
                filteredString.Append(
                    input.Substring(inputStringIndex, foundWord.Index - inputStringIndex)
                    );

                if (IsValidWord(foundWord.Value))
                {
                    filteredString.Append(foundWord.Value);
                }

                // set inputstringindex to end of match
                inputStringIndex = foundWord.Index + foundWord.Length;
            }

            // add rest of text after last word
            filteredString.Append(input.Substring(inputStringIndex));

            return filteredString.ToString();
        }

        protected abstract bool IsValidWord(string word);
    }
}
