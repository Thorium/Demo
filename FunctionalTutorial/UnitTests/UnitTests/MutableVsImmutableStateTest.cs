using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpExamples
{
    [TestClass]
    public class MutableVsImmutableStateTest
    {
        [TestMethod]
        public void TestMutableFactorial()
        {
            Assert.AreEqual(1, MutableVsImmutableState.CalculateFactorial(0));
            Assert.AreEqual(1, MutableVsImmutableState.CalculateFactorial(1));
            Assert.AreEqual(2, MutableVsImmutableState.CalculateFactorial(2));
            Assert.AreEqual(6, MutableVsImmutableState.CalculateFactorial(3));
            Assert.AreEqual(720, MutableVsImmutableState.CalculateFactorial(6));
        }
    }
}
