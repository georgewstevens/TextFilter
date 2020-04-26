using Microsoft.Extensions.Logging;
using Moq;
using System;
using TextFilter;
using TextFilter.Factories;
using TextFilter.Filters;
using TextFilter.Services;
using Xunit;


namespace Test
{
    public class CommandLineProcessorServiceTest
    {

        [Fact]
        public void ProcessCommandLineArguments_FileFoundAndFiltered()
        {
            // arrange
            var filterName = "filterName";
            var filename = "file.txt";
            var filteredString = "filtered string";

            var mockLogger = new Mock<ILogger<CommandLineProcessorService>>();
            var mockOutputService = new Mock<IOutputService>();
            var mockFilterFactory = new Mock<IFilterFactory>();
            var mockFilter = new Mock<IWordFilter>();

            var commandLineProcessorService = new CommandLineProcessorService(
                mockLogger.Object, mockOutputService.Object, mockFilterFactory.Object);

            mockFilterFactory
                .Setup(x => x.CreateFilter(filterName))
                .Returns(mockFilter.Object)
                .Verifiable();
            mockFilter
                .Setup(x => x.Filter(It.IsAny<string>()))
                .Returns(filteredString)
                .Verifiable();
            mockOutputService
                .Setup(x => x.OutputMessage($"string filtered by {filterName}:"))
                .Verifiable();
            mockOutputService
                .Setup(x => x.OutputMessage(filteredString))
                .Verifiable();

            // act
            commandLineProcessorService.ProcessCommandLineArguments(new string[] { filterName, filename });

            // assert
            mockOutputService.VerifyAll();
            mockOutputService.VerifyNoOtherCalls();
            mockFilterFactory.VerifyAll();
            mockFilterFactory.VerifyNoOtherCalls();
            mockFilter.VerifyAll();
            mockFilter.VerifyNoOtherCalls();
        }

        [Fact]
        public void ProcessCommandLineArguments_TooManyArgumentsSupplied()
        {
            // arrange
            var filterName = "filterName";
            var filename = "file.txt";
            var extraArgument = "extra argument";

            var expectedMessage = "Error: Expecting 2 command line arguments";

            var mockLogger = new Mock<ILogger<CommandLineProcessorService>>();
            var mockOutputService = new Mock<IOutputService>();
            var mockFilterFactory = new Mock<IFilterFactory>();

            var commandLineProcessorService = new CommandLineProcessorService(
                mockLogger.Object, mockOutputService.Object, mockFilterFactory.Object);

            // logging extension methods cannot be setup/verified
            mockOutputService
                .Setup(x => x.OutputMessage(expectedMessage))
                .Verifiable();
            mockOutputService
                .Setup(x => x.OutputMessage(commandLineProcessorService.usage))
                .Verifiable();

            // act
            commandLineProcessorService.ProcessCommandLineArguments(
                new string[] { filterName, filename, extraArgument });

            // assert
            mockOutputService.VerifyAll();
            mockOutputService.VerifyNoOtherCalls();
            mockFilterFactory.VerifyAll();
            mockFilterFactory.VerifyNoOtherCalls();
        }

        [Fact]
        public void ProcessCommandLineArguments_TooFewArgumentsSupplied()
        {
            // arrange
            var filterName = "filterName";
            var filename = "file.txt";

            var expectedMessage = "Error: Expecting 2 command line arguments";

            var mockLogger = new Mock<ILogger<CommandLineProcessorService>>();
            var mockOutputService = new Mock<IOutputService>();
            var mockFilterFactory = new Mock<IFilterFactory>();

            var commandLineProcessorService = new CommandLineProcessorService(
                mockLogger.Object, mockOutputService.Object, mockFilterFactory.Object);

            // logging extension methods cannot be setup/verified
            mockOutputService
                .Setup(x => x.OutputMessage(expectedMessage))
                .Verifiable();
            mockOutputService
                .Setup(x => x.OutputMessage(commandLineProcessorService.usage))
                .Verifiable();

            // act
            commandLineProcessorService.ProcessCommandLineArguments(
                new string[] { filterName });

            // assert
            mockOutputService.VerifyAll();
            mockOutputService.VerifyNoOtherCalls();
            mockFilterFactory.VerifyAll();
            mockFilterFactory.VerifyNoOtherCalls();
        }

        [Fact]
        public void ProcessCommandLineArguments_FileNotFound()
        {
            // arrange
            var filterName = "filterName";
            var filename = "fileabc.txt";
            var expectedErrorMessage = "Error: Accessing filepath: fileabc.txt";

            var mockLogger = new Mock<ILogger<CommandLineProcessorService>>();
            var mockOutputService = new Mock<IOutputService>();
            var mockFilterFactory = new Mock<IFilterFactory>();

            var commandLineProcessorService = new CommandLineProcessorService(
                mockLogger.Object, mockOutputService.Object, mockFilterFactory.Object);

            // logger extension methods cannot be setup/verified
            mockOutputService
                .Setup(x => x.OutputMessage(expectedErrorMessage))
                .Verifiable();
            mockOutputService
                .Setup(x => x.OutputMessage(commandLineProcessorService.usage))
                .Verifiable();

            // act
            commandLineProcessorService.ProcessCommandLineArguments(new string[] { filterName, filename });

            // assert
            mockOutputService.VerifyAll();
            mockOutputService.VerifyNoOtherCalls();
            mockFilterFactory.VerifyAll();
            mockFilterFactory.VerifyNoOtherCalls();
        }
        
        [Fact]
        public void ProcessCommandLineArguments_CannotFindFilter()
        {
            // arrange
            var filterName = "filterName";
            var filename = "file.txt";
            var expectedErrorMessage = "Error: filter \"filterName\" not found";

            var mockLogger = new Mock<ILogger<CommandLineProcessorService>>();
            var mockOutputService = new Mock<IOutputService>();
            var mockFilterFactory = new Mock<IFilterFactory>();

            var commandLineProcessorService = new CommandLineProcessorService(
                mockLogger.Object, mockOutputService.Object, mockFilterFactory.Object);

            mockFilterFactory
                .Setup(x => x.CreateFilter(filterName))
                .Throws(new FilterNotFoundException(filterName))
                .Verifiable();
            mockOutputService
                .Setup(x => x.OutputMessage(expectedErrorMessage))
                .Verifiable();
            mockOutputService
                .Setup(x => x.OutputMessage(commandLineProcessorService.usage))
                .Verifiable();

            // act
            commandLineProcessorService.ProcessCommandLineArguments(new string[] { filterName, filename });

            // assert
            mockOutputService.VerifyAll();
            mockOutputService.VerifyNoOtherCalls();
            mockFilterFactory.VerifyAll();
            mockFilterFactory.VerifyNoOtherCalls();
        }

        [Fact]
        public void ProcessCommandLineArguments_OtherExceptionThrown()
        {
            // arrange
            var filterName = "filterName";
            var filename = "file.txt";
            var expectedErrorMessage = "Something went wrong";

            var mockLogger = new Mock<ILogger<CommandLineProcessorService>>();
            var mockOutputService = new Mock<IOutputService>();
            var mockFilterFactory = new Mock<IFilterFactory>();
            var mockFilter = new Mock<IWordFilter>();

            var commandLineProcessorService = new CommandLineProcessorService(
                mockLogger.Object, mockOutputService.Object, mockFilterFactory.Object);

            mockFilterFactory
                .Setup(x => x.CreateFilter(filterName))
                .Returns(mockFilter.Object)
                .Verifiable();
            mockFilter
                .Setup(x => x.Filter(It.IsAny<string>()))
                .Throws(new Exception("just because"))
                .Verifiable();

            mockOutputService
                .Setup(x => x.OutputMessage(expectedErrorMessage))
                .Verifiable();
            mockOutputService
                .Setup(x => x.OutputMessage(commandLineProcessorService.usage))
                .Verifiable();

            // act
            commandLineProcessorService.ProcessCommandLineArguments(new string[] { filterName, filename });

            // assert
            mockOutputService.VerifyAll();
            mockOutputService.VerifyNoOtherCalls();
            mockFilterFactory.VerifyAll();
            mockFilterFactory.VerifyNoOtherCalls();
            mockFilter.VerifyAll();
            mockFilter.VerifyNoOtherCalls();
        }
    }
}
