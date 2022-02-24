namespace Fanax.BetList.Allbet.Test.Builder.StatusBuilder
{
    using Fanex.BetList.Core.Builder.StatusBuilder;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Status2499Tests
    {
        [Test]
        public void BuildStatusResult_HideStatusResultLink()
        {
            ////Arrange
            var _status = new Status2499();
            var _ticket = Substitute.For<ITicket>();
            var _ticketHelper = Substitute.For<ITicketHelper>();

            ////Act
            _status.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            Assert.IsTrue(string.IsNullOrEmpty(_status.Template.StatusResult.ToString()));
        }
    }
}
