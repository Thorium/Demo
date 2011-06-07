using CSharpExamples;
using Xunit;

namespace UnitTests
{
    public class FunctionsAsCitizensTest
    {
        [Fact]
        public void SingleCombinationAsFunction()
        {
            var citizen = new FunctionsAsCitizens();
            Assert.Equal(6, citizen.GetFunctionOfType(FunctionType.Combine).Invoke(1, 5));
        }

        [Fact]
        public void DoubleCombinationAsFunction()
        {
            var citizen = new FunctionsAsCitizens();
            var func = citizen.GetFunctionOfType(FunctionType.Combine);
            Assert.Equal(6, func.Invoke(func.Invoke(1, 2), 3));
        }

        [Fact]
        public void LambdaFunction()
        {
            var citizen = new FunctionsAsCitizens();
            var func = citizen.GetFunctionOfType(FunctionType.Lambda);
            Assert.Equal(12, func.Invoke(2, 6));
        }
    }
}