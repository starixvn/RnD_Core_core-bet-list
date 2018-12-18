namespace Fanex.BetList.ESports.Tests.Builder.ChoiceBuilder
{
    using Core.Builder.ChoiceBuilder;
    using Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice9001Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _ticketHelper = Substitute.For<ITicketHelper>();
            _ticket = Substitute.For<ITicket>();
            _choice = new Choice9001();

            _ticket.BetTypeId = 9001;
            _ticket.RefNo = "8888888";
            _ticket.BetCheck = "80505";
            _ticket.BetId = 80505;
        }

        [Test]
        public void BuildBetType_Always_PassBetIdAndBetCheckToRemotingRefWhenGettingBetTypeName()
        {
            // Arrange.
            const string betTypeName = "Bet Type Name";

            _ticketHelper
                .GetBetTypeNameById(
                    Arg.Is(_ticket.BetTypeId),
                    Arg.Is(_ticket.RefNo),
                    Arg.Is(_ticket.BetId),
                    Arg.Is(_ticket.BetCheck))
                .Returns(betTypeName);

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual(betTypeName, template.BetType.betTypeName);
        }

        [TestCase("<p16 in=\"1\" />")]
        [TestCase("<p25 m=\"0\" /><p16 in=\"1\" />")]
        public void BuildScore_TicketIsLiveAndExtraInfoHasInPlayXMLAttribute_ShowScoreIsInPlay(string extraInfo)
        {
            // Arrange.
            _ticket.ExtraInfo = extraInfo;
            _ticket.IsLive = true;
            var expectedScore = "<span class=\"favorite\"> [IN-PLAY]</span>";

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(expectedScore, template.Score.ToString());
        }

        [TestCase("<p16 in=\"0\" />")]
        [TestCase("<p25 m=\"0\" /><p16 in=\"0\" />")]
        [TestCase("<p25 m=\"0\" />")]
        public void BuildScore_TicketIsLiveAndExtraInfoHasNotInPlayXMLAttribute_ShowScoreIsLive(string extraInfo)
        {
            // Arrange.
            _ticket.ExtraInfo = extraInfo;
            _ticket.IsLive = true;
            var expectedScore = "<span class=\"favorite\"> [Live]</span>";

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(expectedScore, template.Score.ToString());
        }

        [TestCase("<p16 in=\"1\" />")]
        [TestCase("<p16 in=\"0\" />")]
        [TestCase("<p25 m=\"0\" /><p16 in=\"0\" />")]
        [TestCase("<p25 m=\"0\" /><p16 in=\"1\" />")]
        public void BuildScore_TicketIsNonLive_ShowNothing(string extraInfo)
        {
            // Arrange.
            _ticket.ExtraInfo = extraInfo;
            _ticket.IsLive = false;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.Null(template.Score.ToString());
        }
    }
}