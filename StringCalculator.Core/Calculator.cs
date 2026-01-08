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

            int sum = 0;

            foreach (var number in numbers)
            {
                // If TryParse fails, we simply skip that value
                if (int.TryParse(number, out int intNumber))
                {
                    sum += intNumber;
                }        
            }

            return sum;
        }
    }
}

