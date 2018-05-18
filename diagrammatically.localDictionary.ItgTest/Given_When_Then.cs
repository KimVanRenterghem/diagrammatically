using System;

namespace diagrammatically.localDictionary.ItgTest
{
    public abstract class Given_When_Then :  IDisposable
    {
        protected Given_When_Then()
        {
            Given();
            When();
        }

        protected abstract void Given();
        protected abstract void When();

        public virtual void Dispose()
        {
        }
    }
}
