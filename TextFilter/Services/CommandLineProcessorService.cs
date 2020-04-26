using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Runtime.CompilerServices;
using TextFilter.Factories;

[assembly: InternalsVisibleTo("Test")]

namespace TextFilter.Services
{
    public class CommandLineProcessorService: ICommandLineProcessorService
    {
        private readonly ILogger<CommandLineProcessorService> logger;
        private readonly IOutputService outputService;
        private readonly IFilterFactory filterFactory;
        internal readonly string usage;

        public CommandLineProcessorService(ILogger<CommandLineProcessorService> logger, IOutputService outputService, IFilterFactory filterFactory)
        {
            this.logger = logger;
            this.outputService = outputService;
            this.filterFactory = filterFactory;
            usage = CreateUsage();
        }

        private string CreateUsage()
        {
            var baseUsageString = @"
TextFilter.exe <filter> ""<filepath>""

<filter> is the filter you wish to use. It should be one of [{0}].
<filepath> is a filepath to the text to filter. If it contains spaces, please wrap in quotes as shown above.
";
            var enumsString = string.Join('|', Enum.GetNames(typeof(WordFilterType)));
            return string.Format(baseUsageString, enumsString);
        }

        public void ProcessCommandLineArguments(string[] args)
        {
            try
            {
                if (args.Length != 2)
                {
                    HandleError("Error: Expecting 2 command line arguments");
                    return;
                }

                var filterName = args[0];
                var filepath = args[1];
                // throws a number of exception that inherit from IOException
                var inputString = File.ReadAllText(filepath);

                // throws filterNotFoundException
                var filter = filterFactory.CreateFilter(filterName);
                var filteredString = filter.Filter(inputString);

                outputService.OutputMessage($"string filtered by {filterName}:");
                outputService.OutputMessage(filteredString);
            }
            catch (IOException ex)
            {
                HandleError($"Error: Accessing filepath: {args[1]}", ex);
            }
            catch (FilterNotFoundException ex)
            {
                HandleError($"Error: filter \"{args[0]}\" not found", ex);
            }
            catch (Exception ex)
            {
                HandleError("Something went wrong", ex);
            }
        }

        private void HandleError(string message, Exception ex = null) 
        {
            if(ex == null)
            {
                logger.LogError(message);
            }
            else
            {
                logger.LogError(ex, message);
            }

            outputService.OutputMessage(message);
            outputService.OutputMessage(usage);
        }
    }
}
