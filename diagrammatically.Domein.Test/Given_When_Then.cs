using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace diagrammatically.Domein.Test
{
    public abstract class Given_When_Then
    {
        [TestInitialize]
        public void Init()
        {
            Given();
            When();
        }

        protected abstract void Given();
        protected abstract void When();
    }
}
