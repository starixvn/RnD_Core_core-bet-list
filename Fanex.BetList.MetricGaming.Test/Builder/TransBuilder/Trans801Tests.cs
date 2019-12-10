namespace Fanex.BetList.MetricGaming.Test.Builder.TransBuilder
{
    using Fanex.BetList.Core.Builder.TransBuilder;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Trans801Tests
    {
        private ITrans _trans;
        private ITicket _ticket;

        [SetUp]
        public void Setup()
        {
            _trans = new Trans801();
            _ticket = Substitute.For<ITicket>();
        }

        [Test]
        public void BuildRefNo_Always_SetTicketRefNoToResultLinkRefNo()
        {
            // Arrange.
            _ticket.RefNo = "100";

            // Act.
            _trans.Render(_ticket);

            // Assert.
            Assert.AreEqual(_ticket.RefNo, _trans.Template.TransTime.refNo);
        }
    }
}