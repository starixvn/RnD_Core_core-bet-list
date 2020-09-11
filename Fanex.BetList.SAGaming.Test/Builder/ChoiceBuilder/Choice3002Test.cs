namespace Fanex.BetList.SAGaming.Tests.Builder.ChoiceBuilder
{
    using Core.Builder.ChoiceBuilder;
    using Core.Constants;
    using Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice3002Test
    {
        private Choice3002 _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice3002();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();

            _ticket.BetTypeId = SAGamingBetTypes.DragonTiger;
            _ticket.TransId = 1;
            _ticket.CustId = 1000;
        }

        [Test]
        public void GetRefResourceId_Always_ReturnResourceForDragonTiger()
        {
            ////Arrange

            ////Act
            var act = _choice.GetRefResourceId();

            ////Assert
            Assert.AreEqual("SA_DT", act);
        }
    }
}