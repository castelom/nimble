
namespace StringCalculator.Core.Exceptions
{
    public class MaxNumberExceededExeception : Exception
    {
        public MaxNumberExceededExeception() : base("The maximum number of allowed numbers has been exceeded.")
        {
        }
    }
}
