namespace StringCalculator.Core
{
    public class Calculator
    {
        private const char DELIMITER = ',';

        public int Add(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return 0;

            string sanitizeInput = input.Replace('\n', DELIMITER);
            var numbers = sanitizeInput.Split(DELIMITER);

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

