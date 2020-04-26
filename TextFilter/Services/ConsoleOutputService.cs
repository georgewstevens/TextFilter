using System;

namespace TextFilter.Services
{
    public class ConsoleOutputService : IOutputService
    {
        public void OutputMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
