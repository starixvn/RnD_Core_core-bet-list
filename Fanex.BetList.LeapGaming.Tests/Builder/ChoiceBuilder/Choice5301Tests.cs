namespace Fanex.BetList.LeapGaming.Tests.Builder.ChoiceBuilder
{
    using Core.Builder.ChoiceBuilder;
    using Core.Constants;
    using Core.Entities;
    using Fanex.BetList.Core.Utils;
    using NPOI.HSSF.UserModel;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice5301Tests
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice5301();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();

            _ticket.BetTypeId = LeapGamingBetTypes.VirtualFootball;
            _ticket.SportTypeId = 231;

            _ticketHelper.GetBetTypeNameById(LeapGamingBetTypes.VirtualFootball).Returns("Virtual Football");
            _ticketHelper.GetSportNameById(231).Returns("LG Virtual Sports");
            _ticketHelper.GetResourceData("LG_STMT", "mix").Returns("Mix Parlay");
            _ticketHelper.GetResourceData("LG_STMT", "md").Returns("Match Day");
            _ticketHelper.GetResourceData("LG_STMT", "mid").Returns("Match");
            _ticketHelper.GetResourceData("LG_STMT", "se").Returns("Season");
            _ticketHelper.GetResourceData("LG_STMT", "lose").Returns("Lost");
            _ticketHelper.GetResourceData("LG_GT", "GT03").Returns("4-Fold");

            _ticketHelper.GetResourceData("LG_SEL", "S03").Returns("Full Time Result");
        }

        [Test]
        public void BuildChoice_MultipleBet_HideHtml()
        {
            // Arrange
            _ticket.ExtraInfo = "<p49 mt=\"GT03, 4\" />";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsTrue(_choice.Template.League.ToString().Contains("style='display:none;'"));
        }

        [Test]
        public void BuildChoice_Always_ContainLeapGamingSection()
        {
            // Arrange
            _ticket.RefNo = "refno";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsTrue(_choice.Template.League.ToString().Contains("<div id='leapGaming_refno'"));
        }

        [Test]
        public void BuildChoice_SingleBet_NoContainLineHtml()
        {
            // Arrange            
            _ticket.ExtraInfo = "<p49 mt=\"GT14, 4\" />";
            _ticket.TransDesc = "md=5;mid=710897;se=2954;m=London:Florence;s=S03 : Florence;o=4.0";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsFalse(_choice.Template.League.ToString().Contains("<div class='line'></div>"));
        }

        [Test]
        public void BuildChoice_MultipleBet_ContainLineHtml()
        {
            // Arrange            
            _ticket.ExtraInfo = "<p49 mt=\"GT03, 4\" />";
            _ticket.TransDesc = "md=5;mid=710897;se=2954;m=London:Florence;s=S03 : Florence;o=4.0";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsTrue(_choice.Template.League.ToString().Contains("<div class='line'></div>"));
        }       

        [Test]
        public void BuildLeague_Running_SingleBet_IsNull()
        {
            // Arrange
            _ticket.BetCheck = "GT14, 4";
            _ticket.Status = "running";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsNull(_choice.Template.League.LeagueName);
        }  

        [Test]
        public void BuildLeague_MultipleBet_ShowMixParlayText()
        {
            // Arrange
            _ticket.ExtraInfo = "<p49 mt=\"GT03, 4\" />";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsTrue(_choice.Template.League.LeagueName.leagueName.Contains("Mix Parlay"));
        }

        [Test]
        public void BuildLeague_MultipleBet_ShowBetMode()
        {
            // Arrange
            _ticket.ExtraInfo = "<p49 mt=\"GT03, 4\" />";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsTrue(_choice.Template.League.LeagueName.leagueName.Contains("4-Fold (4)"));
        }


        [Test]
        public void BuildLeague_MultipleBet_SupportJavascript()
        {
            // Arrange            
            _ticket.TransDesc = "md=5;mid=710897;se=2954;m=London:Florence;s=S03 : Florence;o=4.0|md=5;mid=710898;se=2954;m=Munich:Paris;s=S03 : Munich;o=2.25";
            _ticket.RefNo = "refno";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsTrue(_choice.Template.League.LeagueName.leagueName.Contains("javascript:showDetail(\"leapGaming_refno\""));
        }

        [Test]
        public void RenderRTF_SingleBet_ShowFullTicket()
        {
            // Arrange
            _ticket.ExtraInfo = "<p49 mt=\"GT14, 4\" />";
            _ticket.TransDesc = "md=5;mid=710897;se=2954;m=London:Florence;s=S03 : Florence;o=4.0";
            var workbook = new HSSFWorkbook();
            var rtfHelper = new RTFHelper
            {
                NegFont = workbook.CreateFont(),
                NormalFont = workbook.CreateFont(),
                PosFont = workbook.CreateFont(),
                NegFontCrossed = workbook.CreateFont(),
                NormalFontCrossed = workbook.CreateFont(),
                PosFontCrossed = workbook.CreateFont(),
                RTFRenderer = new RtfTextRender()
            };
            
            // Act
            var act = _choice.RenderRTF(_ticket, _ticketHelper, null, false, rtfHelper);
            var linesInActualText = act[0].String.Split('\n');

            // Assert
            Assert.AreEqual("Florence", linesInActualText[0]);
            Assert.AreEqual("Full Time Result", linesInActualText[1]);
            Assert.AreEqual("London - vs - Florence", linesInActualText[2]);
            Assert.AreEqual("Match Day 5, Match 710897, Season 2954", linesInActualText[3]);
            Assert.AreEqual("LG Virtual Sports Virtual Football", linesInActualText[4]);
        }

        [Test]
        public void RenderRTF_MultipleBet_ShowBetMode()
        {
            // Arrange
            _ticket.Status = "WON";
            _ticket.ExtraInfo = "<p49 mt=\"GT03, 4\" />";
            _ticket.TransDesc = "md=5;mid=710897;se=2954;m=London:Florence;s=S03 : Florence;o=4.0;mo=lose|md=5;mid=710897;se=2954;m=London:Florence;s=S03 : Florence;o=4.0;mo=lose";
            var workbook = new HSSFWorkbook();
            var rtfHelper = new RTFHelper
            {
                NegFont = workbook.CreateFont(),
                NormalFont = workbook.CreateFont(),
                PosFont = workbook.CreateFont(),
                NegFontCrossed = workbook.CreateFont(),
                NormalFontCrossed = workbook.CreateFont(),
                PosFontCrossed = workbook.CreateFont(),
                RTFRenderer = new RtfTextRender()
            };

            // Act
            var act = _choice.RenderRTF(_ticket, _ticketHelper, null, false, rtfHelper);
            var linesInActualText = act[0].String.Split('\n');

            // Assert
            Assert.AreEqual("Mix Parlay", linesInActualText[0]);
            Assert.AreEqual("4-Fold (4)", linesInActualText[1]);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_WhenCalled_AlwaysSetBetTeamClassNameIsEmpty()
        {
            _choice.Render(_ticket, _ticketHelper, null, false);

            Assert.IsEmpty(_choice.Template.betTeamClassName);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_WhenCalled_AlwaysHideHandicap()
        {
            _choice.Render(_ticket, _ticketHelper, null, false);

            Assert.IsFalse(_choice.Template.Handicap.Visible);
        }

        [Test]
        public void BuildBetTeam_Always_IsEmpty()
        {
            _choice.Render(_ticket, _ticketHelper, null, false);

            Assert.IsEmpty(_choice.Template.betTeam);
        }

        [Test]
        public void BuildMatch_WhenCalled_AlwaysNull()
        {
            _choice.Render(_ticket, _ticketHelper, null, false);

            Assert.IsNull(_choice.Template.Match);
        }

        [Test]
        public void BuildBetType_WhenCalled_AlwaysNull()
        {
            _choice.Render(_ticket, _ticketHelper, null, false);

            Assert.IsNull(_choice.Template.BetType);
        }

        [Test]
        public void BuildScore_WhenCalled_AlwaysHide()
        {
            _choice.Render(_ticket, _ticketHelper, null, false);

            Assert.False(_choice.Template.Score.Visible);
        }

        [Test]
        public void BuildSport_Always_IsEmpty()
        {
            _choice.Render(_ticket, _ticketHelper, null, false);

            Assert.IsEmpty(_choice.Template.League.sportTypeName);
        }
    }
}