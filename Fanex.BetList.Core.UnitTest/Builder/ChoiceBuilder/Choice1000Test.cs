namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using App_GlobalResources;
    using Common.Enums;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    /// Unit test for Choice1000 class.
    /// </summary>
    [TestFixture]
    public class Choice1000Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice1000();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.P2P;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void Render_WhenCalled_ReturnTemplatep2pGameResource()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedTemplate = string.Format("<div><span class='favorite'>{0}</span></div>", CoreBetList.p2pGame);
            Assert.AreEqual(expectedTemplate, _choice.Template.ToString());
        }
    }
}