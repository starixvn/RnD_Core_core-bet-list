namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Core.Builder.ChoiceBuilder;
    using Core.Templates;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice451Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice451();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [TestCase("Y1X")]
        [TestCase("y1X")]
        [TestCase("y1x")]
        [TestCase("Y1x")]
        public void BuildBetTeam_BetTeamIsY1X_BetTeamIsYesAndHomeDraw(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            const string ExpectedBetOption = "Yes <span class=\"underdog\">&</span> Home/Draw";

            Assert.AreEqual(ExpectedBetOption, template.betTeam);
        }

        [TestCase("Y12")]
        [TestCase("y12")]
        public void BuildBetTeam_BetTeamIsY12_BetTeamIsYesAndHomeAway(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            const string ExpectedBetOption = "Yes <span class=\"underdog\">&</span> Home/Away";

            Assert.AreEqual(ExpectedBetOption, template.betTeam);
        }

        [TestCase("Y2X")]
        [TestCase("y2X")]
        [TestCase("y2x")]
        [TestCase("Y2x")]
        public void BuildBetTeam_BetTeamIsY2X_BetTeamIsYesAndHAwayDraw(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            const string ExpectedBetOption = "Yes <span class=\"underdog\">&</span> Away/Draw";

            Assert.AreEqual(ExpectedBetOption, template.betTeam);
        }

        [TestCase("N1X")]
        [TestCase("n1X")]
        [TestCase("n1x")]
        [TestCase("N1x")]
        public void BuildBetTeam_BetTeamIsN1X_BetTeamIsNoAndHomeDraw(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            const string ExpectedBetOption = "No <span class=\"underdog\">&</span> Home/Draw";

            Assert.AreEqual(ExpectedBetOption, template.betTeam);
        }

        [TestCase("N12")]
        [TestCase("n12")]
        public void BuildBetTeam_BetTeamIsN12_BetTeamIsNoAndHomeAway(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            const string ExpectedBetOption = "No <span class=\"underdog\">&</span> Home/Away";

            Assert.AreEqual(ExpectedBetOption, template.betTeam);
        }

        [TestCase("N2X")]
        [TestCase("n2X")]
        [TestCase("n2x")]
        [TestCase("N2x")]
        public void BuildBetTeam_BetTeamIsN2X_BetTeamIsNoAndHAwayDraw(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            const string ExpectedBetOption = "No <span class=\"underdog\">&</span> Away/Draw";

            Assert.AreEqual(ExpectedBetOption, template.betTeam);
        }

        [TestCase("abc")]
        [TestCase("N2Xx")]
        [TestCase("NX")]
        [TestCase("N2")]
        [TestCase("Y2")]
        [TestCase("")]
        [TestCase(null)]
        public void BuildBetTeam_BetTeamIsNotInExpectedValueList_BetTeamIsEmpty(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            Assert.IsEmpty(template.betTeam);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetHandicapToNull()
        {
            // Act.
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            Assert.Null(template.Handicap.handicap);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetBetTeamClassNameToFavorite()
        {
            // Act.
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            const string Favorite = "favorite";

            Assert.AreEqual(Favorite, template.betTeamClassName);
        }
    }
}