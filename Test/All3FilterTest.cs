using Moq;
using TextFilter.Filters;
using Xunit;

namespace Test
{
    public class All3FilterTest
    {
        [Fact]
        public void TestAll3FilteredOutput()
        {
            var inputString = "input string";
            var onceFitleredString = "once filtered string";
            var twiceFilteredString = "twice filtered string";
            var thriceFilteredString = "thrice filtered string";

            // arrange
            var mockMiddleVowelFilter = new Mock<MiddleVowelFilter>();
            var mockLessThan3Filter = new Mock<LessThan3Filter>();
            var mockContainsTFilter = new Mock<ContainsTFilter>();

            var filter = new All3Filter(
                mockMiddleVowelFilter.Object,
                mockLessThan3Filter.Object,
                mockContainsTFilter.Object);

            mockMiddleVowelFilter
                .Setup(x => x.Filter(inputString))
                .Returns(onceFitleredString)
                .Verifiable();
            mockLessThan3Filter
                .Setup(x => x.Filter(onceFitleredString))
                .Returns(twiceFilteredString)
                .Verifiable();
            mockContainsTFilter
                .Setup(x => x.Filter(twiceFilteredString))
                .Returns(thriceFilteredString)
                .Verifiable();

            // act
            var result = filter.Filter(inputString);

            // assert
            Assert.Equal(thriceFilteredString, result);
            mockMiddleVowelFilter.VerifyAll();
            mockMiddleVowelFilter.VerifyNoOtherCalls();
            mockLessThan3Filter.VerifyAll();
            mockLessThan3Filter.VerifyNoOtherCalls();
            mockContainsTFilter.VerifyAll();
            mockContainsTFilter.VerifyNoOtherCalls();
        }
    }
}
