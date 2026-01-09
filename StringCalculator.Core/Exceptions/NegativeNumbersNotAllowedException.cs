namespace StringCalculator.Core.Exceptions
{
    public class NegativeNumbersNotAllowedException : Exception
    {
        public NegativeNumbersNotAllowedException(IEnumerable<int> negatives)
            : base($"{ErrorMessages.NegativeNumbersNotAllowed} {string.Join(",", negatives)}")
        {
        }
    }
}
