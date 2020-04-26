using System;

namespace TextFilter
{
    public class FilterNotFoundException : Exception
    {
        public FilterNotFoundException(string filter) 
            : base($"Specified filter not found : {filter}")
        {
        }
    }
}
