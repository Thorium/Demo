namespace CSharpExamples
{
    public class MutableObject
    {
        public int CallCount { get; private set; }

        public int CalculateDummyValue()
        {
            CallCount++;
            return CallCount;
        }

    }
}