using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace diagrammatically.Domein.Test
{
    [TestClass]
    public class When_having_input : Given_When_Then
    {
        private InputProseser _sut;

        protected override void Given()
        {
            _sut = new InputProseser();
        }

        protected override void When()
        {
            _sut.Loockup("test");
        }

        [TestMethod]
        public void Then_There_Will_Be_A_List_Of_Posibiletys()
        {
        }
    }
}
