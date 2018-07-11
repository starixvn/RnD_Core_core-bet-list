namespace Fanex.BetList.PokerGames.Tests.Builder.ChoiceBuilder
{
    using Core.Builder.ChoiceBuilder;
    using Core.Entities;
    using Core.Utils;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice1499Tests
    {
        private const string GemsResourceId = "Gems";
        private const string GemsBeforeBuyingResourceId = "BeforePurchase";
        private const string GemsAfterBuyingResourceId = "AfterPurchase";

        private const string GemsPattern = "{0} Gems";
        private const string GemsBeforeBuyingPattern = "Total Gems before purchase: {0}";
        private const string GemsAfterBuyingPattern = "Total Gems after purchase: {0}";

        private const decimal TotalGems = 1200;
        private const decimal GemsBought = 1000;

        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice1499();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();

            _ticket.BetTypeId = 1499;
            _ticket.ExtraInfo = $"<p33 tg=\"{TotalGems}\" gb=\"{GemsBought}\" fc=\"0\" rc=\"0\"/>";
            _ticket.Stake = 2000;

            _ticketHelper.GetResourceData(Arg.Is(Choice1401.GameInfoRefId), Arg.Is(GemsResourceId)).Returns(GemsPattern);
            _ticketHelper.GetResourceData(Arg.Is(Choice1401.GameInfoRefId), Arg.Is(GemsBeforeBuyingResourceId)).Returns(GemsBeforeBuyingPattern);
            _ticketHelper.GetResourceData(Arg.Is(Choice1401.GameInfoRefId), Arg.Is(GemsAfterBuyingResourceId)).Returns(GemsAfterBuyingPattern);
        }

        [Test]
        public void BuildBetTeam_Always_BetTeamIsEmptyString()
        {
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.IsEmpty(template.betTeam);
        }

        [Test]
        public void BuildMatch_Always_CallRemotingRefToGetTextOfGems()
        {
            // Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            _ticketHelper.Received().GetResourceData(Arg.Is(Choice1401.GameInfoRefId), Arg.Is(GemsResourceId));
        }

        [Test]
        public void BuildMatch_Always_BoughtGamesStoredInExtraInfo()
        {
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);
            var gemsPurchaseInfo = template.Match.ToString();

            // Assert.
            var expectedResult = string.Format(GemsPattern, ConvertByBetType.Stake(GemsBought));

            StringAssert.Contains(expectedResult, gemsPurchaseInfo);
        }

        [Test]
        public void BuildMatch_Always_CallRemotingRefToGetTextOfTotalGemsBeforePurchase()
        {
            // Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            _ticketHelper.Received().GetResourceData(Arg.Is(Choice1401.GameInfoRefId), Arg.Is(GemsBeforeBuyingResourceId));
        }

        [Test]
        public void BuildMatch_Always_TotalGemsBeforePurchaseIsCalculatedBySubtractingGemsBoughtFromTotalGems()
        {
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);
            var gemsPurchaseInfo = template.Match.ToString();

            // Assert.
            var totalGemsBeforePurchase = TotalGems - GemsBought;
            var expectedResult = string.Format(GemsBeforeBuyingPattern, ConvertByBetType.Stake(totalGemsBeforePurchase));

            StringAssert.Contains(expectedResult, gemsPurchaseInfo);
        }

        [Test]
        public void BuildMatch_Always_CallRemotingRefToGetTextOfTotalGemsAfterPurchase()
        {
            // Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            _ticketHelper.Received().GetResourceData(Arg.Is(Choice1401.GameInfoRefId), Arg.Is(GemsAfterBuyingResourceId));
        }

        [Test]
        public void BuildMatch_Always_TotalGemsAfterPurchaseStoredInExtraInfo()
        {
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);
            var gemsPurchaseInfo = template.Match.ToString();

            // Assert.
            var expectedResult = string.Format(GemsAfterBuyingPattern, ConvertByBetType.Stake(TotalGems));

            StringAssert.Contains(expectedResult, gemsPurchaseInfo);
        }

        [Test]
        public void BuildMatch_Always_GemsPurchaseInfoIsInOrder()
        {
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);
            var gemsPurchaseInfo = template.Match.ToString();

            // Assert.
            var expectedGemsBought = string.Format(GemsPattern, ConvertByBetType.Stake(GemsBought));
            var expectedGemsBeforePurchase = string.Format(GemsBeforeBuyingPattern, ConvertByBetType.Stake(TotalGems - GemsBought));
            var expectedGemsAfterPurchase = string.Format(GemsAfterBuyingPattern, ConvertByBetType.Stake(TotalGems));
            var expectedResult = $"<div>{expectedGemsBought}</div><div>{expectedGemsBeforePurchase}</div><div>{expectedGemsAfterPurchase}</div>";

            StringAssert.Contains(expectedResult, gemsPurchaseInfo);
        }

        [Test]
        public void BuildBetTeamsInfo_Always_SecondLineIsSeatAndTableInfo()
        {
            // Arrange.
            var rtfHelper = new RTFHelper().Clone();

            // Act.
            var actualText = _choice.RenderRTF(_ticket, _ticketHelper, null, false, rtfHelper);
            var linesInActualText = actualText[0].String.Split('\n');

            // Assert.
            var expectedText = string.Format(GemsPattern, ConvertByBetType.Stake(GemsBought));

            Assert.AreEqual(expectedText, linesInActualText[1]);
        }

        [Test]
        public void BuildBetTeamsInfo_Always_ThirdLineIsSTableMode()
        {
            // Arrange.
            var rtfHelper = new RTFHelper().Clone();

            // Act.
            var actualText = _choice.RenderRTF(_ticket, _ticketHelper, null, false, rtfHelper);
            var linesInActualText = actualText[0].String.Split('\n');

            // Assert.
            var expectedText = string.Format(GemsBeforeBuyingPattern, ConvertByBetType.Stake(TotalGems - GemsBought));

            Assert.AreEqual(expectedText, linesInActualText[2]);
        }

        [Test]
        public void BuildBetTeamsInfo_Always_ForthLineIsPlayerInfo()
        {
            // Arrange.
            var rtfHelper = new RTFHelper().Clone();

            // Act.
            var actualText = _choice.RenderRTF(_ticket, _ticketHelper, null, false, rtfHelper);
            var linesInActualText = actualText[0].String.Split('\n');

            // Assert.
            var expectedText = string.Format(GemsAfterBuyingPattern, ConvertByBetType.Stake(TotalGems));

            Assert.AreEqual(expectedText, linesInActualText[3]);
        }
    }
}