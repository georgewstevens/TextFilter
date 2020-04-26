using TextFilter.Filters;
using Xunit;

namespace Test
{
    public class LessThan3FilterTest
    {
        [Theory]
        [InlineData("","")]
        [InlineData(" ", " ")]
        [InlineData("a","")]
        [InlineData("ab","")]
        [InlineData("a ab"," ")]
        [InlineData("abc","abc")]
        [InlineData("a abc ab", " abc ")]
        [InlineData("abcd", "abcd")]
        [InlineData("abcd a ab abc a", "abcd   abc ")]
        [InlineData("A", "")]
        [InlineData("ABC","ABC")]
        public void TestLessThan3FilterOutput(string inputString, string expectedResult)
        {
            // arrange
            var filter = new LessThan3Filter();

            // act
            var result = filter.Filter(inputString);

            // assert
            Assert.Equal(expectedResult, result);
        }

    }
}
