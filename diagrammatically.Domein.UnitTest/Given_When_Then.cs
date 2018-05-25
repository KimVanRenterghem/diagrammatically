namespace diagrammatically.Domein.UnitTest
{
    public abstract class Given_When_Then
    {
        public Given_When_Then()
        {
            Given();
            When();
        }

        protected abstract void Given();
        protected abstract void When();
    }
}
