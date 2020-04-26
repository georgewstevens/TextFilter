using System;

namespace TextFilter.Filters
{
    public class All3Filter : AbstractWordFilter
    {
        private readonly MiddleVowelFilter middleVowelFilter;
        private readonly LessThan3Filter lessThan3Filter;
        private readonly ContainsTFilter containsTFilter;

        public All3Filter(MiddleVowelFilter middleVowelFilter, LessThan3Filter lessThan3Filter, ContainsTFilter containsTFilter)
        {
            this.middleVowelFilter = middleVowelFilter;
            this.lessThan3Filter = lessThan3Filter;
            this.containsTFilter = containsTFilter;
        }

        public override string Filter(string input)
        {
            var filteredString = middleVowelFilter.Filter(input);
            filteredString = lessThan3Filter.Filter(filteredString);
            filteredString = containsTFilter.Filter(filteredString);
            return filteredString;
        }

        protected override bool IsValidWord(string word)
        {
            throw new NotImplementedException();
        }
    }
}
