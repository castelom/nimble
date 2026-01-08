using System;

namespace StringCalculator.Core
{
    public class Calculator
    {
        private static readonly string[] defaultDelimiters = { ",", "\n" };
        private const string CUSTOM_DELIMITER_PREFIX = "//";

        public int Add(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return 0;

            IList<string> delimiters = new List<string>(defaultDelimiters);

            //Custom delimiter
            if (input.StartsWith(CUSTOM_DELIMITER_PREFIX)) 
            {
                //Execute extraction of custom delimiter(s)
                input = ExtractCustomDelimiter(input, delimiters);
            }

            string[] numbers = input.Split(delimiters.ToArray(), StringSplitOptions.None);

            IList<int> negativeNumbers = new List<int>();

            int sum = 0;

            foreach (var number in numbers)
            {
                // If TryParse fails, we simply skip that value
                if (!int.TryParse(number, out int intNumber))
                    continue;

                if (intNumber < 0)
                {
                    negativeNumbers.Add(intNumber);
                    continue;
                }

                //Skip numbers greater than 1000
                if (intNumber > 1000)
                    continue;

                sum += intNumber;
            }

            if(negativeNumbers.Any())
            {
                throw new Exceptions.NegativeNumbersNotAllowedException(negativeNumbers);
            }

            return sum;
        }

        /// <summary>
        /// Extracts a custom delimiter definition from the input expression
        /// and updates the list of delimiters while returning the remaining numeric expression.
        ///
        /// Supports custom delimiters of any length using the format:
        /// //[{delimiter}]\n{numbers}
        /// 
        /// Supports custom delimiters of 1 char using the format:
        /// //{delimiter}\n{numbers}
        ///
        /// </summary>
        private static string ExtractCustomDelimiter(string input, IList<string> delimiters)
        {
            var delimiterEndIndex = input.IndexOf("]\n", StringComparison.Ordinal);
            string expressionWithoutDelimiter = string.Empty;
            string delimiter = string.Empty;

            if (delimiterEndIndex > 0)
            {
                delimiter = input.Substring(3, delimiterEndIndex - 3);
                delimiters.Add(delimiter);
                expressionWithoutDelimiter = input.Substring(delimiterEndIndex + 2);
            }
            else
            {
                delimiter = input[2].ToString();
                delimiters.Add(delimiter);
                expressionWithoutDelimiter = input.Substring(4);
            }

            return expressionWithoutDelimiter;
        }
    }
}

