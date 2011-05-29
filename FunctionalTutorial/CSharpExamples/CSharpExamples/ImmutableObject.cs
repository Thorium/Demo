using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CSharpExamples
{
    public class ImmutableObject
    {
        private readonly int callCount;

        public int CallCount
        {
            get { return callCount; }
        }

        public ImmutableObject(int callCount)
        {
            this.callCount = callCount;
        }

        public ImmutableObject CalculateDummyValue()
        {
            return new ImmutableObject(callCount + 1);
        }
    }

    public class ImmutableList<T> : ReadOnlyCollection<T>
    {
        public ImmutableList() : this(new List<T>())
        {
        }

        public ImmutableList(IList<T> list) : base(list)
        {
        }

        public ImmutableList<T> Add(T item)
        {
            Items.Add(item);
            return new ImmutableList<T>(Items);
        }
    }
}