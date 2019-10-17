namespace Fanex.BetList.ColossusBets.Tests.Builder.TransBuilder
{
    using Fanex.BetList.Core.Builder.TransBuilder;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Trans18000Tests
    {
        private ITrans _trans;
        private ITicket _ticket;

        [SetUp]
        public void Setup()
        {
            _trans = new Trans18000();
            _ticket = Substitute.For<ITicket>();
        }

        [Test]
        public void BuildRefNo_Always_SetRefNoIsRefNo()
        {
            // Arrange
            _ticket.RefNo = "100";

            // Act
            _trans.Render(_ticket);

            // Assert
            Assert.AreEqual(_ticket.RefNo, _trans.Template.TransTime.refNo);
        }
    }
}