using System;
using Xunit;

namespace c_sharp_kata
{
    public class StringCalculatorTest
    {
        [Fact]
        public void ShouldReturnZeroForEmptyString()
        {
            var stringCal = new StringCalculator();

            var result = stringCal.Add("");

            Assert.Equal(0, result);
        }

        [Fact]
        public void ShouldReturnSingleNumber()
        {
            var stringCal = new StringCalculator();

            var result = stringCal.Add("1");

            Assert.Equal(1, result);
        }

        [Theory]
        [InlineData(3, "1,2")]
        [InlineData(13, "8,5")]
        [InlineData(21, "4,3,14")]
        public void ShouldSumMultipleNumbers(int expectedSum, string numbers)
        {
            var stringCal = new StringCalculator();

            var result = stringCal.Add(numbers);

            Assert.Equal(expectedSum, result);
        }

        [Fact]
        public void ShouldSumWhenNewLineAsDelimiter()
        {
            var stringCal = new StringCalculator();

            var result = stringCal.Add("1\n2,3");

            Assert.Equal(6, result);
        }

        [Theory]
        [InlineData(3, "//;\n1;2")]
        [InlineData(10, "//(\n7(3")]
        public void ShouldSumWithCustomDelimiter(int expectedSum, string numbers)
        {
            var stringCal = new StringCalculator();

            var result = stringCal.Add(numbers);

            Assert.Equal(expectedSum, result);
        }

        [Theory]
        [InlineData("-1", "-1")]
        [InlineData("-7, -3", "//(\n-7(-3")]
        public void ShouldThrowErrorForNegativeNumber(string expectedErrorMessage, string numbers)
        {
            var stringCal = new StringCalculator();

            var exception = Assert.Throws<Exception>(() => stringCal.Add(numbers));

            Assert.Equal($"negatives not allowed: {expectedErrorMessage}", exception.Message);
        }

        [Fact]
        public void ShouldIgnoreNumberBiggerThanAThousand()
        {
            var stringCal = new StringCalculator();

            var result = stringCal.Add("1001\n2,3");

            Assert.Equal(5, result);
        }

        [Theory]
        [InlineData(6, "//[***]\n1***2***3")]
        [InlineData(10, "//[*][%]\n5*2%3")]
        [InlineData(10, "//[*][%#]\n5*2%#3")]
        public void ShouldSumWithMultiCharDelimiter(int expectedSum, string numbers)
        {
            var stringCal = new StringCalculator();

            var result = stringCal.Add(numbers);

            Assert.Equal(expectedSum, result);
        }
    }
}
