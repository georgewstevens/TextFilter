using TextFilter.Filters;
using Xunit;

namespace Test
{
    public class ContainsTFilterTest
    {
        [Theory]
        [InlineData("", "")]
        [InlineData(" ", " ")]
        [InlineData("abc atc tbc abc", "abc   abc")]
        [InlineData("t", "")]
        [InlineData("abt atc tbc", "  ")]
        [InlineData("abc abc", "abc abc")]
        [InlineData("A", "A")]
        [InlineData("T", "")]
        public void TestContainsTFilteredOutput(string input, string expectedOutput)
        {
            // arrange
            var filter = new ContainsTFilter();

            // act
            var result = filter.Filter(input);

            // assert
            Assert.Equal(expectedOutput, result);
        }
    }
}
