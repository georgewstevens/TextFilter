using System;
using TextFilter.Filters;

namespace TextFilter.Factories
{
    public class FilterFactory: IFilterFactory
    {
        public IWordFilter CreateFilter(string filterName)
        {
            WordFilterType filterType;
            if (!Enum.TryParse<WordFilterType>(filterName, true, out filterType))
            {
                throw new FilterNotFoundException(filterName);
            }

            switch (filterType)
            {
                case WordFilterType.MiddleVowel:
                    return new MiddleVowelFilter();

                case WordFilterType.LessThan3:
                    return new LessThan3Filter();

                case WordFilterType.ContainsT:
                    return new ContainsTFilter();

                case WordFilterType.All3:
                    return new All3Filter(new MiddleVowelFilter(), new LessThan3Filter(), new ContainsTFilter());

                default:
                    throw new FilterNotFoundException(filterType.ToString());
            }
        }
    }
}
