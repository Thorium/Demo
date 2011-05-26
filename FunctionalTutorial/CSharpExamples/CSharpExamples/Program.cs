using System;

namespace CSharpExamples
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine(MutableVsImmutableState.CalculateFactorial(5));
            Console.WriteLine("press key to exit");
            Console.ReadKey();
        }
    }
}