namespace TextFilter.Filters
{
    public class LessThan3Filter : AbstractWordFilter
    {
        protected override bool IsValidWord(string word)
        {
            return word.Length > 2;
        }
    }
}
