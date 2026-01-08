namespace StringCalculator.Core.Exceptions
{
    public class NegativeNumbersNotAllowedException : Exception
    {
        public NegativeNumbersNotAllowedException(IEnumerable<int> negatives)
            : base($"Negatives numbers are not allowed: {string.Join(",", negatives)}")
        {
        }
    }
}
