namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Constants;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice9410Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice9410();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetId = 98;
            _ticket.BetTypeId = BetTypes.HomeGroup1XRuns;
            _ticketHelper = Substitute.For<ITicketHelper>();
            _ticketHelper.GetBetTypeNameById(BetTypes.HomeGroup1XRuns, Arg.Any<object>(), (long?)98, Arg.Any<object>()).Returns("Bettype name 98");
        }

        [Test]
        public void BuildBetType_Always_ShowBetTypeNameAndReplaceX()
        {
            // Arrange

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual("Bettype name 98", _choice.Template.BetType.betTypeName);
        }
    }
}