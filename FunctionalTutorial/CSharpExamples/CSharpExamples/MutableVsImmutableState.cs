namespace CSharpExamples
{
    public class MutableVsImmutableState
    {
        public static int CalculateFactorial(int end)
        {
            var result = 1;
            for (var i = 1; i <= end; i++)
            {
                result *= i;
            }
            return result;
        }
    }
}