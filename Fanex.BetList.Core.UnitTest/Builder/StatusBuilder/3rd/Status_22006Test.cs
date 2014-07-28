namespace Fanex.BetList.Core.UnitTest.Builder.StatusBuilder._3rd
{
    using Core.Builder.StatusBuilder._3rd;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    /// The Unit testing for Status_22006 class.
    /// </summary>
    public class Status_22006Test
    {
        private Status_22006 _status;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _status = new Status_22006();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void BuildStatusResult_Always_SetStatusResultBlockIsNull()
        {
            // Act
            _status.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsNull(_status.Template.StatusResult);
        }

        [Test]
        public void BuildIP_Always_SetIPBlockIsNull()
        {
            // Act
            _status.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsNull(_status.Template.ShowIP);
        }
    }
}