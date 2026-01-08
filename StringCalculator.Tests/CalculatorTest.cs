using StringCalculator.Core;
using StringCalculator.Core.Exceptions;

namespace StringCalculator.Tests
{
    public class CalculatorTest
    {
        // System under testing _sut
        private readonly Calculator _sut = new Calculator();

        [Theory]
        [InlineData("", 0)]
        [InlineData(null, 0)]
        [InlineData("5asd,asd", 0)]
        [InlineData("5asd,1", 1)]
        [InlineData("5asd", 0)]
        [InlineData("20", 20)]
        [InlineData("1,5000", 5001)]
        [InlineData("4,-3", 1)]
        [InlineData("-4,3", -1)]

        public void Add_Expression_ShouldParseAndSumValidNumbers(string expression, int expected)
        {
            //Act
            var result = _sut.Add(expression);

            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]

        public void Add_MoreThanTwoNumbers_ShouldThrowsException()
        {
            //Assert
            Assert.Throws<MaxNumberExceededExeception>(() => _sut.Add("1,2,3"));
        }
    }
}