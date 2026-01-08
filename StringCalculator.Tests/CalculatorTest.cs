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
        public void Add_WhenInputIsEmptyOrNull_ShouldReturnsZero(string expression, int expected)
        {
            // Act
            var result = _sut.Add(expression);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("5asd,asd", 0)]
        [InlineData("5asd,1", 1)]
        [InlineData("5asd", 0)]
        [InlineData("1,2,3,4,5,6,7,8,9,10,11,12,abc", 78)]
        [InlineData("1,5000", 1)]
        [InlineData("10000", 0)]

        public void Add_WhenExpressionContainsInvalidNumbers_ShouldIgnoresThem(string expression, int expected)
        {
            // Act
            var result = _sut.Add(expression);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("20", 20)]
        [InlineData("1,500", 501)]
        [InlineData("1,2,3,4,5,6,7,8,9,10,11,12", 78)]
        [InlineData("1\n500", 501)]
        [InlineData("1\n2\n3\n4\n5\n6\n7\n8\n9\n10\n11\n12", 78)]
        public void Add_WithDefaultDelimiters_ShouldReturnsSum(string expression, int expected)
        {
            // Act
            var result = _sut.Add(expression);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("//*\n1*2*3*4", 10)]
        [InlineData("//*\n1*2,3\n4", 10)]
        [InlineData("//$\n1,2$3\n4", 10)]
        [InlineData("//&\n1&2&3&4", 10)]
        public void Add_WithCustomDelimiters_ShouldReturnsSum(string expression, int expected)
        {
            // Act
            var result = _sut.Add(expression);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("-4", new[] {-4})]
        [InlineData("-4,3", new[] { -4 })]
        [InlineData("4,-3", new[] { -3 })]
        [InlineData("-4,-3", new[] { -4, -3 })]
        [InlineData("5asd,-4", new[] { -4 })]
        [InlineData("1,2,3,4,5,6,7,8,9,10,11,12,-1", new[] { -1 })]

        public void Add_ExpressionWithNegativeNumbers_ShouldThrowsNegativeNumberException(string expression, int[] expectedNegatives)
        {
            //Act
            var exception = Assert.Throws<NegativeNumbersNotAllowedException>(() =>
            {
                //Act
                var result = _sut.Add(expression);
            });

            //Assert
            foreach (var negative in expectedNegatives)
            {
                Assert.Contains(negative.ToString(), exception.Message);
            }
        }
    }
}