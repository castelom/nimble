using StringCalculator.Core.Exceptions;
using System;
using System.ComponentModel;

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

            IList<string> delimiters = [.. defaultDelimiters];

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
        /// 
        /// Supports custom delimiters of 1 char using the format:
        /// //{delimiter}\n{expression}
        /// 
        /// Supports custom delimiters of any length using the format:
        /// [{delimiter}]\n{expression} 
        ///
        /// Supports multiple custom delimiters of any length using the format:
        /// //[{delimiter}][{delimiter}]...[{delimiter}]\n{expression}
        /// </summary>
        private static string ExtractCustomDelimiter(string input, IList<string> delimiters)
        {
            var delimiterEndIndex = input.IndexOf("\n", StringComparison.Ordinal);
            string expressionWithoutDelimiter = input;
            string delimiter = string.Empty;

            if (delimiterEndIndex > 0)
            {
                expressionWithoutDelimiter = input.Substring(delimiterEndIndex + 1);
                var delimiterSection = input.Substring(2, delimiterEndIndex -2);

                int index = 0;

                if(!delimiterSection.StartsWith("[") || !delimiterSection.EndsWith("]"))
                {
                    if (string.IsNullOrEmpty(delimiterSection) || delimiterSection.Contains('[') || delimiterSection.Contains(']'))
                        throw new FormatException(ErrorMessages.InvalidDelimiter);
                    delimiters.Add(delimiterSection);
                }

                else
                {
                    while (index < delimiterSection.Length)
                    {
                        if (delimiterSection[index] == '[')
                        {
                            int closingBracketIndex = delimiterSection.IndexOf(']', index);
                            if (closingBracketIndex == -1)
                                break;

                            delimiter = delimiterSection.Substring(index + 1, closingBracketIndex - index - 1);

                            if (string.IsNullOrEmpty(delimiter) || delimiter.Contains('[') || delimiter.Contains(']'))
                                throw new FormatException(ErrorMessages.InvalidDelimiter);

                            delimiters.Add(delimiter);

                            index = closingBracketIndex + 1;
                        }
                        else
                        {
                            index++;
                        }
                    }
                }
            }

            return expressionWithoutDelimiter;
        }
    }
}

