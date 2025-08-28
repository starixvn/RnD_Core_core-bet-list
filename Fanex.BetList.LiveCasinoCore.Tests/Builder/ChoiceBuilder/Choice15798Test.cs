namespace Fanex.BetList.LiveCasinoCore.Tests.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Core.Builder.ChoiceBuilder;
    using Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice15798Test
    {
        private Choice15798 _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;
        private List<ITicketData> _ticketData;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice15798();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
            _ticketData = new List<ITicketData>();

            // Setup default ticket properties
            _ticket.TransId = 1;
            _ticket.BetCheck = "detail|&|displayDetail";
            _ticket.TransDesc = "tablecode=B001*comms=1*result=P";
            _ticket.BetTypeId = 15798;
            _ticket.SportTypeId = 1;
            _ticket.CustId = 123;
            _ticket.MatchCode = "MATCH001";
            _ticket.SiteId = "SITE001";
            _ticket.CurrencyId = 20;
            _ticket.OddsSpread = 100.0000m;
            _ticket.Odds = 0.0100m;
            _ticket.Currency = "USD";
            _ticket.BetdaqId = 456;
        }

        [Test]
        public void Render_WithDetailTicket_ShouldAppendJackpotContributionInfoWithRate()
        {
            // Arrange
            const string expectedStake = "100.0000";
            const string expectedRate = "1.0000%";
            const string expectedCurrency = "USD";

            _ticket.BetCheck = "someBetCheck"; // Makes it a detail ticket
            _ticket.OddsSpread = 100.0000m;
            _ticket.Odds = 0.0100m;
            _ticket.Currency = expectedCurrency;
            _ticketHelper.GetResourceData("PTL_BB", "B001").Returns("Baccarat Table B001");

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsNotNull(_choice.Template.betTeam);
            Assert.IsTrue(_choice.Template.betTeam.Contains("Baccarat Table B001"));
            Assert.IsTrue(_choice.Template.betTeam.Contains($"Stake: {expectedStake} {expectedCurrency}"));
            Assert.IsTrue(_choice.Template.betTeam.Contains($"@ Rate {expectedRate}"));
            Assert.IsTrue(_choice.Template.betTeam.Contains("<div class=\"favorite\">"));
            Assert.IsTrue(_choice.Template.betTeam.Contains("</div>"));
        }

        [Test]
        public void Render_WithSummaryTicket_ShouldAppendJackpotContributionInfoWithoutRate()
        {
            // Arrange
            const string expectedStake = "100.0000";
            const string expectedCurrency = "USD";

            _ticket.BetCheck = ""; // Makes it a summary ticket
            _ticket.OddsSpread = 100.0000m;
            _ticket.Currency = expectedCurrency;
            _ticketHelper.GetResourceData("PTL_BB", "B001").Returns("Baccarat Table B001");

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsNotNull(_choice.Template.betTeam);
            Assert.IsTrue(_choice.Template.betTeam.Contains($"Stake: {expectedStake} {expectedCurrency}"));
            Assert.IsFalse(_choice.Template.betTeam.Contains("@ Rate"));
            Assert.IsTrue(_choice.Template.betTeam.Contains("<div class=\"favorite\">"));
            Assert.IsTrue(_choice.Template.betTeam.Contains("</div>"));
        }

        [Test]
        public void Render_WithNullBetCheck_ShouldAppendJackpotContributionInfoWithoutRate()
        {
            // Arrange
            const string expectedStake = "100.0000";
            const string expectedCurrency = "USD";

            _ticket.BetCheck = null; // Makes it a summary ticket
            _ticket.OddsSpread = 100.0000m;
            _ticket.Currency = expectedCurrency;
            _ticketHelper.GetResourceData("PTL_BB", "B001").Returns("Baccarat Table B001");

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsNotNull(_choice.Template.betTeam);
            Assert.IsTrue(_choice.Template.betTeam.Contains($"Stake: {expectedStake} {expectedCurrency}"));
            Assert.IsFalse(_choice.Template.betTeam.Contains("@ Rate"));
        }

        [Test]
        public void Render_WithDifferentOddsSpread_ShouldFormatStakeCorrectly()
        {
            // Arrange
            const string expectedStake = "250.5000";
            const string expectedRate = "2.5000%";

            _ticket.BetCheck = "someBetCheck";
            _ticket.OddsSpread = 250.5m;
            _ticket.Odds = 0.025m;
            _ticket.Currency = "EUR";
            _ticketHelper.GetResourceData("PTL_BB", "B001").Returns("Baccarat Table B001");

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsTrue(_choice.Template.betTeam.Contains($"Stake: {expectedStake} EUR"));
            Assert.IsTrue(_choice.Template.betTeam.Contains($"@ Rate {expectedRate}"));
        }

        [Test]
        public void Render_WithZeroOddsSpread_ShouldFormatStakeCorrectly()
        {
            // Arrange
            const string expectedStake = "0.0000";
            const string expectedRate = "0.0000%";

            _ticket.BetCheck = "someBetCheck";
            _ticket.OddsSpread = 0m;
            _ticket.Odds = 0m;
            _ticket.Currency = "USD";
            _ticketHelper.GetResourceData("PTL_BB", "B001").Returns("Baccarat Table B001");

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsTrue(_choice.Template.betTeam.Contains($"Stake: {expectedStake} USD"));
            Assert.IsTrue(_choice.Template.betTeam.Contains($"@ Rate {expectedRate}"));
        }

        [Test]
        public void Render_WithNegativeOddsSpread_ShouldFormatStakeCorrectly()
        {
            // Arrange
            const string expectedStake = "-100.0000";
            const string expectedRate = "-1.0000%";

            _ticket.BetCheck = "someBetCheck";
            _ticket.OddsSpread = -100m;
            _ticket.Odds = -0.01m;
            _ticket.Currency = "USD";
            _ticketHelper.GetResourceData("PTL_BB", "B001").Returns("Baccarat Table B001");

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsTrue(_choice.Template.betTeam.Contains($"Stake: {expectedStake} USD"));
            Assert.IsTrue(_choice.Template.betTeam.Contains($"@ Rate {expectedRate}"));
        }

        [Test]
        public void Render_WithDecimalOddsSpread_ShouldFormatStakeCorrectly()
        {
            // Arrange
            const string expectedStake = "123.4567";
            const string expectedRate = "1.2346%";

            _ticket.BetCheck = "someBetCheck";
            _ticket.OddsSpread = 123.4567m;
            _ticket.Odds = 0.012346m;
            _ticket.Currency = "JPY";
            _ticketHelper.GetResourceData("PTL_BB", "B001").Returns("Baccarat Table B001");

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsTrue(_choice.Template.betTeam.Contains($"Stake: {expectedStake} JPY"));
            Assert.IsTrue(_choice.Template.betTeam.Contains($"@ Rate {expectedRate}"));
        }

        [Test]
        public void Render_WithLargeOddsSpread_ShouldFormatStakeCorrectly()
        {
            // Arrange
            const string expectedStake = "999,999.9999";
            const string expectedRate = "99.9999%";

            _ticket.BetCheck = "someBetCheck";
            _ticket.OddsSpread = 999999.9999m;
            _ticket.Odds = 0.999999m;
            _ticket.Currency = "USD";
            _ticketHelper.GetResourceData("PTL_BB", "B001").Returns("Baccarat Table B001");

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsTrue(_choice.Template.betTeam.Contains($"Stake: {expectedStake} USD"));
            Assert.IsTrue(_choice.Template.betTeam.Contains($"@ Rate {expectedRate}"));
        }

        [Test]
        public void Render_WithDifferentCurrency_ShouldAppendCorrectCurrency()
        {
            // Arrange
            const string expectedCurrency = "EUR";

            _ticket.BetCheck = "someBetCheck";
            _ticket.OddsSpread = 100m;
            _ticket.Odds = 0.01m;
            _ticket.Currency = expectedCurrency;
            _ticketHelper.GetResourceData("PTL_BB", "B001").Returns("Baccarat Table B001");

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsTrue(_choice.Template.betTeam.Contains($"Stake: 100.0000 {expectedCurrency}"));
        }

        [Test]
        public void Render_WithEmptyCurrency_ShouldAppendEmptyCurrency()
        {
            // Arrange
            const string expectedCurrency = "";

            _ticket.BetCheck = "someBetCheck";
            _ticket.OddsSpread = 100m;
            _ticket.Odds = 0.01m;
            _ticket.Currency = expectedCurrency;
            _ticketHelper.GetResourceData("PTL_BB", "B001").Returns("Baccarat Table B001");

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsTrue(_choice.Template.betTeam.Contains($"Stake: 100.0000 {expectedCurrency}"));
        }

        [Test]
        public void Render_WithNullCurrency_ShouldAppendNullCurrency()
        {
            // Arrange
            string expectedCurrency = null;

            _ticket.BetCheck = "someBetCheck";
            _ticket.OddsSpread = 100m;
            _ticket.Odds = 0.01m;
            _ticket.Currency = expectedCurrency;
            _ticketHelper.GetResourceData("PTL_BB", "B001").Returns("Baccarat Table B001");

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsTrue(_choice.Template.betTeam.Contains("Stake: 100.0000 "));
        }

        [Test]
        public void Render_WithDifferentTableCode_ShouldAppendJackpotContributionInfo()
        {
            // Arrange
            const string tableCodeValue = "VIP001";
            const string expectedStake = "500.0000";
            const string expectedRate = "5.0000%";

            _ticket.BetCheck = "someBetCheck";
            _ticket.TransDesc = "tablecode=VIP001*comms=2*result=W";
            _ticket.OddsSpread = 500m;
            _ticket.Odds = 0.05m;
            _ticket.Currency = "JPY";
            _ticketHelper.GetResourceData("PTL_BB", tableCodeValue).Returns("VIP Baccarat Table");

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsTrue(_choice.Template.betTeam.Contains("VIP Baccarat Table"));
            Assert.IsTrue(_choice.Template.betTeam.Contains($"Stake: {expectedStake} JPY"));
            Assert.IsTrue(_choice.Template.betTeam.Contains($"@ Rate {expectedRate}"));
        }

        [Test]
        public void Render_WithMissingTableCode_ShouldAppendJackpotContributionInfo()
        {
            // Arrange
            const string expectedStake = "100.0000";
            const string expectedRate = "1.0000%";

            _ticket.BetCheck = "someBetCheck";
            _ticket.TransDesc = "comms=1*result=P"; // No tablecode
            _ticket.OddsSpread = 100m;
            _ticket.Odds = 0.01m;
            _ticket.Currency = "USD";

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsTrue(_choice.Template.betTeam.Contains($"Stake: {expectedStake} USD"));
            Assert.IsTrue(_choice.Template.betTeam.Contains($"@ Rate {expectedRate}"));
        }

        [Test]
        public void Render_WithEmptyTransDesc_ShouldAppendJackpotContributionInfo()
        {
            // Arrange
            const string expectedStake = "100.0000";
            const string expectedRate = "1.0000%";

            _ticket.BetCheck = "someBetCheck";
            _ticket.TransDesc = "";
            _ticket.OddsSpread = 100m;
            _ticket.Odds = 0.01m;
            _ticket.Currency = "USD";

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsTrue(_choice.Template.betTeam.Contains($"Stake: {expectedStake} USD"));
            Assert.IsTrue(_choice.Template.betTeam.Contains($"@ Rate {expectedRate}"));
        }

        [Test]
        public void Render_WithNullTransDesc_ShouldAppendJackpotContributionInfo()
        {
            // Arrange
            const string expectedStake = "100.0000";
            const string expectedRate = "1.0000%";

            _ticket.BetCheck = "someBetCheck";
            _ticket.TransDesc = null;
            _ticket.OddsSpread = 100m;
            _ticket.Odds = 0.01m;
            _ticket.Currency = "USD";

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsTrue(_choice.Template.betTeam.Contains($"Stake: {expectedStake} USD"));
            Assert.IsTrue(_choice.Template.betTeam.Contains($"@ Rate {expectedRate}"));
        }

        [Test]
        public void Render_WithExtremeValues_ShouldFormatCorrectly()
        {
            // Arrange
            const string expectedStake = "1,234,567.8901";
            const string expectedRate = "123.4568%";

            _ticket.BetCheck = "someBetCheck";
            _ticket.OddsSpread = 1234567.8901m;
            _ticket.Odds = 1.234568m;
            _ticket.Currency = "USD";
            _ticketHelper.GetResourceData("PTL_BB", "B001").Returns("Baccarat Table B001");

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsTrue(_choice.Template.betTeam.Contains($"Stake: {expectedStake} USD"));
            Assert.IsTrue(_choice.Template.betTeam.Contains($"@ Rate {expectedRate}"));
        }

        [Test]
        public void Render_WithMinimalValues_ShouldFormatCorrectly()
        {
            // Arrange
            const string expectedStake = "0.0001";
            const string expectedRate = "0.0001%";

            _ticket.BetCheck = "someBetCheck";
            _ticket.OddsSpread = 0.0001m;
            _ticket.Odds = 0.000001m;
            _ticket.Currency = "USD";
            _ticketHelper.GetResourceData("PTL_BB", "B001").Returns("Baccarat Table B001");

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsTrue(_choice.Template.betTeam.Contains($"Stake: {expectedStake} USD"));
            Assert.IsTrue(_choice.Template.betTeam.Contains($"@ Rate {expectedRate}"));
        }

        [Test]
        public void Render_WithAllPropertiesChanged_ShouldAppendJackpotContributionInfo()
        {
            // Arrange
            const string expectedStake = "750.2500";
            const string expectedRate = "7.5025%";
            const string expectedCurrency = "JPY";

            _ticket.TransId = 999;
            _ticket.BetCheck = "differentBetCheck";
            _ticket.TransDesc = "tablecode=VIP002*comms=2*result=W";
            _ticket.BetTypeId = 9999;
            _ticket.SportTypeId = 5;
            _ticket.CustId = 999;
            _ticket.MatchCode = "DIFFERENT_MATCH";
            _ticket.SiteId = "DIFFERENT_SITE";
            _ticket.CurrencyId = 30;
            _ticket.OddsSpread = 750.25m;
            _ticket.Odds = 0.075025m;
            _ticket.Currency = expectedCurrency;
            _ticket.BetdaqId = 789;

            _ticketHelper.GetResourceData("PTL_BB", "VIP002").Returns("VIP Baccarat Table 002");

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsTrue(_choice.Template.betTeam.Contains("VIP Baccarat Table 002"));
            Assert.IsTrue(_choice.Template.betTeam.Contains($"Stake: {expectedStake} {expectedCurrency}"));
            Assert.IsTrue(_choice.Template.betTeam.Contains($"@ Rate {expectedRate}"));
        }

        [Test]
        public void Render_WithMockedTicketHelper_ShouldAppendJackpotContributionInfo()
        {
            // Arrange
            const string expectedStake = "200.0000";
            const string expectedRate = "2.0000%";

            var mockTicketHelper = Substitute.For<ITicketHelper>();
            mockTicketHelper.GetResourceData("PTL_BB", "B001").Returns("Mocked Baccarat Table");

            _ticket.BetCheck = "someBetCheck";
            _ticket.OddsSpread = 200m;
            _ticket.Odds = 0.02m;
            _ticket.Currency = "EUR";

            // Act
            _choice.Render(_ticket, mockTicketHelper, _ticketData, false);

            // Assert
            Assert.IsTrue(_choice.Template.betTeam.Contains("Mocked Baccarat Table"));
            Assert.IsTrue(_choice.Template.betTeam.Contains($"Stake: {expectedStake} EUR"));
            Assert.IsTrue(_choice.Template.betTeam.Contains($"@ Rate {expectedRate}"));
        }

        [Test]
        public void Render_WithSpecialCharactersInCurrency_ShouldAppendCorrectly()
        {
            // Arrange
            const string expectedCurrency = "JP¥";

            _ticket.BetCheck = "someBetCheck";
            _ticket.OddsSpread = 100m;
            _ticket.Odds = 0.01m;
            _ticket.Currency = expectedCurrency;
            _ticketHelper.GetResourceData("PTL_BB", "B001").Returns("Baccarat Table B001");

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsTrue(_choice.Template.betTeam.Contains($"Stake: 100.0000 {expectedCurrency}"));
        }

        [Test]
        public void Render_WithUnicodeCurrency_ShouldAppendCorrectly()
        {
            // Arrange
            const string expectedCurrency = "元";

            _ticket.BetCheck = "someBetCheck";
            _ticket.OddsSpread = 100m;
            _ticket.Odds = 0.01m;
            _ticket.Currency = expectedCurrency;
            _ticketHelper.GetResourceData("PTL_BB", "B001").Returns("Baccarat Table B001");

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsTrue(_choice.Template.betTeam.Contains($"Stake: 100.0000 {expectedCurrency}"));
        }

        [Test]
        public void Render_WithEmojiCurrency_ShouldAppendCorrectly()
        {
            // Arrange
            const string expectedCurrency = "💰";

            _ticket.BetCheck = "someBetCheck";
            _ticket.OddsSpread = 100m;
            _ticket.Odds = 0.01m;
            _ticket.Currency = expectedCurrency;
            _ticketHelper.GetResourceData("PTL_BB", "B001").Returns("Baccarat Table B001");

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsTrue(_choice.Template.betTeam.Contains($"Stake: 100.0000 {expectedCurrency}"));
        }
    }
}
