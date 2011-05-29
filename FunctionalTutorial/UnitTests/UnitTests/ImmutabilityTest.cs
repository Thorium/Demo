using CSharpExamples;
using Xunit;

namespace UnitTests
{
    public class NotThreadSafeTest
    {
        [Fact]
        public void ImmutableListAndObject()
        {
            var list = new ImmutableList<ImmutableObject>();
            list.Add(new ImmutableObject(1)).Add(new ImmutableObject(2));
            Assert.Equal(2, list.Count);
            list[0].CalculateDummyValue();
            Assert.Equal(1, list[0].CallCount);
        }


        [Fact]
        public void ImmutableListButMutableObject()
        {
            var list = new ImmutableList<MutableObject>();
            list.Add(new MutableObject()).Add(new MutableObject());
            Assert.Equal(2, list.Count);
            list[0].CalculateDummyValue();
            list[0].CalculateDummyValue();
            Assert.Equal(2, list[0].CallCount);
        }
    }
}