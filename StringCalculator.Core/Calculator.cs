using System;

namespace StringCalculator.Core
{
    public class Calculator
    {
        private static readonly char[] defaultDelimiters = { ',', '\n' };
        private const string CUSTOM_DELIMITER_PREFIX = "//";

        public int Add(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return 0;

            IList<char> delimiters = new List<char>(defaultDelimiters);

            //Custom delimiter
            if (input.StartsWith(CUSTOM_DELIMITER_PREFIX)) 
            {
                char customDelimiter = input[2];
                input = input.Substring(4); //Remove the custom delimiter definition
                delimiters.Add(customDelimiter);
            }

            string[] numbers = input.Split(delimiters.ToArray());

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
    }
}

