namespace StringCalculator.Core
{
    public class Calculator
    {
        private const char DELIMITER = ',';
        private const int MAXNUMBERALLOWED = 2;

        public int Add(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return 0;

            var numbers = input.Split(DELIMITER);

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

