namespace TextFilter.Filters
{
    public class ContainsTFilter : AbstractWordFilter
    {
        protected override bool IsValidWord(string word)
        {
            return !word.ToLower().Contains('t');
        }
    }
}
