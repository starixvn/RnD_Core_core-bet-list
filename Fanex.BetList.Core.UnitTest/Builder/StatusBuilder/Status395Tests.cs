namespace Fanex.BetList.Core.UnitTest.Builder.StatusBuilder
{
    using System.Globalization;
    using Fanex.BetList.Core.Builder.StatusBuilder;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.UnitTest.Common;
    using NSubstitute;
    using NUnit.Framework;

    public class Status395Tests
    {
        private IStatus _status;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _status = new Status395();
            _ticket = TestData.CreateTicket(BetTypes.ExtraTimeNextGoalMethod);
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        public void BuildStatusResult_BetIdOfTicketIsNull_BetIdInResultLinkIsZero()
        {
            // Arrange.
            _ticket.BetId = null;

            // Act.
            _status.Render(_ticket, _ticketHelper, null, true);

            // Assert.
            const string expectedBetIdInResultLink = "0";

            Assert.AreEqual(expectedBetIdInResultLink, _status.Template.StatusResult.betId);
        }

        [Test]
        public void BuildStatusResult_BetIdOfTicketTakesAnyValue_BetIdInResultLinkIsBetIdOfTicket()
        {
            // Arrange.
            const long betId = 801;

            _ticket.BetId = betId;

            // Act.
            _status.Render(_ticket, _ticketHelper, null, true);

            // Assert.
            var expectedBetIdInResultLink = betId.ToString(CultureInfo.InvariantCulture);

            Assert.AreEqual(expectedBetIdInResultLink, _status.Template.StatusResult.betId);
        }
    }
}