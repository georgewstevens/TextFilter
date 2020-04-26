using TextFilter.Filters;

namespace TextFilter.Factories
{
    public interface IFilterFactory
    {
        IWordFilter CreateFilter(string type);
    }
}
