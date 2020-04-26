using TextFilter.Filters;
using Xunit;

namespace Test
{
    public class MiddelVowelFilterTest
    {
        [Theory]
        [InlineData("","")]
        [InlineData("a","a")]
        [InlineData("ab","ab")]
        [InlineData("abc","b")]
        [InlineData("abcd","bc")]
        [InlineData("abcde", "c")]
        [InlineData("abcdef", "cd")]
        public void TestGetMiddleChars(string inputWord, string expectedResult)
        {
            // arrange
            var filter = new MiddleVowelFilter();

            // act
            var result = filter.GetMiddleChars(inputWord);

            // assert
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData("",false)]
        [InlineData("a",true)]
        [InlineData("e",true)]
        [InlineData("i",true)]
        [InlineData("o",true)]
        [InlineData("u",true)]
        [InlineData("b",false)]
        [InlineData("c",false)]
        [InlineData("ab", true)]
        [InlineData("A", true)]
        [InlineData("B", false)]

        public void TestContainsVowel(string inputWord, bool expectedResult)
        {
            // arrange
            var filter = new MiddleVowelFilter();

            // act
            var result = filter.ContainsVowels(inputWord);

            // assert
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData("","")]
        [InlineData(" ", " ")]
        [InlineData("a", "")]
        [InlineData("a b", " b")]
        [InlineData("ab cd", " cd")]
        [InlineData("abc def", "abc ")]
        [InlineData("abc def ghi", "abc  ghi")]
        [InlineData("ABC DEF", "ABC ")]
        public void TestMiddleVowelFilterOutput(string inputString, string expectedResult)
        {
            // arrange
            var filter = new MiddleVowelFilter();

            // act
            var result = filter.Filter(inputString);

            // assert
            Assert.Equal(expectedResult, result);
        }
    }
}
