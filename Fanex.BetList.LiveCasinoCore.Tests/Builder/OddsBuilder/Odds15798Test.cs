namespace Fanex.BetList.LiveCasinoCore.Tests.Builder.OddsBuilder
{
    using System.Collections.Generic;
    using Core.Builder.OddsBuilder;
    using Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Odds15798Test
    {
        private Odds15798 _odds;
        private ITicket _ticket;
        private List<ITicketData> _ticketData;
        private GetCachePropertyById _getOddsTypeNameFunc;

        [SetUp]
        public void Setup()
        {
            _odds = new Odds15798();
            _ticket = Substitute.For<ITicket>();
            _ticketData = new List<ITicketData>();
            _getOddsTypeNameFunc = Substitute.For<GetCachePropertyById>();

            // Setup default ticket properties
            _ticket.TransId = 1;
            _ticket.Odds = 1.85m;
            _ticket.OddsType = "MY";
            _ticket.BetTypeId = 15798;
            _ticket.SportTypeId = 1;
            _ticket.Stake = 100;
            _ticket.Currency = "USD";
            _ticket.Status = "Won";
            _ticket.Winlost = 85;
        }

        [Test]
        public void Render_Always_ReturnsTemplateWithHyphenOddsAndEmptyOddsType()
        {
            // Act
            var template = _odds.Render(_ticket, _ticketData, _getOddsTypeNameFunc);

            // Assert
            Assert.IsNotNull(template);
            Assert.AreEqual("-", template.odds);
            Assert.AreEqual(string.Empty, template.oddsType);
        }

        [Test]
        public void Render_WithPositiveOdds_AlwaysReturnsHyphenOdds()
        {
            // Arrange
            _ticket.Odds = 2.50m;

            // Act
            var template = _odds.Render(_ticket, _ticketData, _getOddsTypeNameFunc);

            // Assert
            Assert.AreEqual("-", template.odds);
        }

        [Test]
        public void Render_WithZeroOdds_AlwaysReturnsHyphenOdds()
        {
            // Arrange
            _ticket.Odds = 0;

            // Act
            var template = _odds.Render(_ticket, _ticketData, _getOddsTypeNameFunc);

            // Assert
            Assert.AreEqual("-", template.odds);
        }

        [Test]
        public void Render_WithNegativeOdds_AlwaysReturnsHyphenOdds()
        {
            // Arrange
            _ticket.Odds = -1.50m;

            // Act
            var template = _odds.Render(_ticket, _ticketData, _getOddsTypeNameFunc);

            // Assert
            Assert.AreEqual("-", template.odds);
        }

        [Test]
        public void Render_WithDecimalOdds_AlwaysReturnsHyphenOdds()
        {
            // Arrange
            _ticket.Odds = 1.234567m;

            // Act
            var template = _odds.Render(_ticket, _ticketData, _getOddsTypeNameFunc);

            // Assert
            Assert.AreEqual("-", template.odds);
        }

        [Test]
        public void Render_WithLargeOdds_AlwaysReturnsHyphenOdds()
        {
            // Arrange
            _ticket.Odds = 999999.99m;

            // Act
            var template = _odds.Render(_ticket, _ticketData, _getOddsTypeNameFunc);

            // Assert
            Assert.AreEqual("-", template.odds);
        }

        [Test]
        public void Render_WithDifferentOddsType_AlwaysReturnsEmptyOddsType()
        {
            // Arrange
            _ticket.OddsType = "Dec";

            // Act
            var template = _odds.Render(_ticket, _ticketData, _getOddsTypeNameFunc);

            // Assert
            Assert.AreEqual(string.Empty, template.oddsType);
        }

        [Test]
        public void Render_WithEmptyOddsType_AlwaysReturnsEmptyOddsType()
        {
            // Arrange
            _ticket.OddsType = "";

            // Act
            var template = _odds.Render(_ticket, _ticketData, _getOddsTypeNameFunc);

            // Assert
            Assert.AreEqual(string.Empty, template.oddsType);
        }

        [Test]
        public void Render_WithNullOddsType_AlwaysReturnsEmptyOddsType()
        {
            // Arrange
            _ticket.OddsType = null;

            // Act
            var template = _odds.Render(_ticket, _ticketData, _getOddsTypeNameFunc);

            // Assert
            Assert.AreEqual(string.Empty, template.oddsType);
        }

        [Test]
        public void Render_WithWhitespaceOddsType_AlwaysReturnsEmptyOddsType()
        {
            // Arrange
            _ticket.OddsType = "   ";

            // Act
            var template = _odds.Render(_ticket, _ticketData, _getOddsTypeNameFunc);

            // Assert
            Assert.AreEqual(string.Empty, template.oddsType);
        }

        [Test]
        public void Render_WithDifferentBetTypeId_AlwaysReturnsHyphenOdds()
        {
            // Arrange
            _ticket.BetTypeId = 9999;

            // Act
            var template = _odds.Render(_ticket, _ticketData, _getOddsTypeNameFunc);

            // Assert
            Assert.AreEqual("-", template.odds);
        }

        [Test]
        public void Render_WithZeroBetTypeId_AlwaysReturnsHyphenOdds()
        {
            // Arrange
            _ticket.BetTypeId = 0;

            // Act
            var template = _odds.Render(_ticket, _ticketData, _getOddsTypeNameFunc);

            // Assert
            Assert.AreEqual("-", template.odds);
        }

        [Test]
        public void Render_WithNegativeBetTypeId_AlwaysReturnsHyphenOdds()
        {
            // Arrange
            _ticket.BetTypeId = -1;

            // Act
            var template = _odds.Render(_ticket, _ticketData, _getOddsTypeNameFunc);

            // Assert
            Assert.AreEqual("-", template.odds);
        }

        [Test]
        public void Render_WithDifferentSportTypeId_AlwaysReturnsHyphenOdds()
        {
            // Arrange
            _ticket.SportTypeId = 5;

            // Act
            var template = _odds.Render(_ticket, _ticketData, _getOddsTypeNameFunc);

            // Assert
            Assert.AreEqual("-", template.odds);
        }

        [Test]
        public void Render_WithZeroSportTypeId_AlwaysReturnsHyphenOdds()
        {
            // Arrange
            _ticket.SportTypeId = 0;

            // Act
            var template = _odds.Render(_ticket, _ticketData, _getOddsTypeNameFunc);

            // Assert
            Assert.AreEqual("-", template.odds);
        }

        [Test]
        public void Render_WithNegativeSportTypeId_AlwaysReturnsHyphenOdds()
        {
            // Arrange
            _ticket.SportTypeId = -1;

            // Act
            var template = _odds.Render(_ticket, _ticketData, _getOddsTypeNameFunc);

            // Assert
            Assert.AreEqual("-", template.odds);
        }

        [Test]
        public void Render_WithDifferentStake_AlwaysReturnsHyphenOdds()
        {
            // Arrange
            _ticket.Stake = 500;

            // Act
            var template = _odds.Render(_ticket, _ticketData, _getOddsTypeNameFunc);

            // Assert
            Assert.AreEqual("-", template.odds);
        }

        [Test]
        public void Render_WithZeroStake_AlwaysReturnsHyphenOdds()
        {
            // Arrange
            _ticket.Stake = 0;

            // Act
            var template = _odds.Render(_ticket, _ticketData, _getOddsTypeNameFunc);

            // Assert
            Assert.AreEqual("-", template.odds);
        }

        [Test]
        public void Render_WithNegativeStake_AlwaysReturnsHyphenOdds()
        {
            // Arrange
            _ticket.Stake = -100;

            // Act
            var template = _odds.Render(_ticket, _ticketData, _getOddsTypeNameFunc);

            // Assert
            Assert.AreEqual("-", template.odds);
        }

        [Test]
        public void Render_WithDifferentCurrency_AlwaysReturnsHyphenOdds()
        {
            // Arrange
            _ticket.Currency = "EUR";

            // Act
            var template = _odds.Render(_ticket, _ticketData, _getOddsTypeNameFunc);

            // Assert
            Assert.AreEqual("-", template.odds);
        }

        [Test]
        public void Render_WithEmptyCurrency_AlwaysReturnsHyphenOdds()
        {
            // Arrange
            _ticket.Currency = "";

            // Act
            var template = _odds.Render(_ticket, _ticketData, _getOddsTypeNameFunc);

            // Assert
            Assert.AreEqual("-", template.odds);
        }

        [Test]
        public void Render_WithNullCurrency_AlwaysReturnsHyphenOdds()
        {
            // Arrange
            _ticket.Currency = null;

            // Act
            var template = _odds.Render(_ticket, _ticketData, _getOddsTypeNameFunc);

            // Assert
            Assert.AreEqual("-", template.odds);
        }

        [Test]
        public void Render_WithDifferentStatus_AlwaysReturnsHyphenOdds()
        {
            // Arrange
            _ticket.Status = "Lose";

            // Act
            var template = _odds.Render(_ticket, _ticketData, _getOddsTypeNameFunc);

            // Assert
            Assert.AreEqual("-", template.odds);
        }

        [Test]
        public void Render_WithEmptyStatus_AlwaysReturnsHyphenOdds()
        {
            // Arrange
            _ticket.Status = "";

            // Act
            var template = _odds.Render(_ticket, _ticketData, _getOddsTypeNameFunc);

            // Assert
            Assert.AreEqual("-", template.odds);
        }

        [Test]
        public void Render_WithNullStatus_AlwaysReturnsHyphenOdds()
        {
            // Arrange
            _ticket.Status = null;

            // Act
            var template = _odds.Render(_ticket, _ticketData, _getOddsTypeNameFunc);

            // Assert
            Assert.AreEqual("-", template.odds);
        }

        [Test]
        public void Render_WithDifferentWinlost_AlwaysReturnsHyphenOdds()
        {
            // Arrange
            _ticket.Winlost = 200;

            // Act
            var template = _odds.Render(_ticket, _ticketData, _getOddsTypeNameFunc);

            // Assert
            Assert.AreEqual("-", template.odds);
        }

        [Test]
        public void Render_WithZeroWinlost_AlwaysReturnsHyphenOdds()
        {
            // Arrange
            _ticket.Winlost = 0;

            // Act
            var template = _odds.Render(_ticket, _ticketData, _getOddsTypeNameFunc);

            // Assert
            Assert.AreEqual("-", template.odds);
        }

        [Test]
        public void Render_WithNegativeWinlost_AlwaysReturnsHyphenOdds()
        {
            // Arrange
            _ticket.Winlost = -50;

            // Act
            var template = _odds.Render(_ticket, _ticketData, _getOddsTypeNameFunc);

            // Assert
            Assert.AreEqual("-", template.odds);
        }

        [Test]
        public void Render_WithDifferentTransId_AlwaysReturnsHyphenOdds()
        {
            // Arrange
            _ticket.TransId = 999999;

            // Act
            var template = _odds.Render(_ticket, _ticketData, _getOddsTypeNameFunc);

            // Assert
            Assert.AreEqual("-", template.odds);
        }

        [Test]
        public void Render_WithZeroTransId_AlwaysReturnsHyphenOdds()
        {
            // Arrange
            _ticket.TransId = 0;

            // Act
            var template = _odds.Render(_ticket, _ticketData, _getOddsTypeNameFunc);

            // Assert
            Assert.AreEqual("-", template.odds);
        }

        [Test]
        public void Render_WithNegativeTransId_AlwaysReturnsHyphenOdds()
        {
            // Arrange
            _ticket.TransId = -1;

            // Act
            var template = _odds.Render(_ticket, _ticketData, _getOddsTypeNameFunc);

            // Assert
            Assert.AreEqual("-", template.odds);
        }

        [Test]
        public void Render_WithEmptyTicketData_AlwaysReturnsHyphenOdds()
        {
            // Arrange
            var emptyTicketData = new List<ITicketData>();

            // Act
            var template = _odds.Render(_ticket, emptyTicketData, _getOddsTypeNameFunc);

            // Assert
            Assert.AreEqual("-", template.odds);
        }

        [Test]
        public void Render_WithNullTicketData_AlwaysReturnsHyphenOdds()
        {
            // Arrange
            List<ITicketData> nullTicketData = null;

            // Act
            var template = _odds.Render(_ticket, nullTicketData, _getOddsTypeNameFunc);

            // Assert
            Assert.AreEqual("-", template.odds);
        }

        [Test]
        public void Render_WithMockedTicketData_AlwaysReturnsHyphenOdds()
        {
            // Arrange
            var mockTicketData = new List<ITicketData>
            {
                Substitute.For<ITicketData>(),
                Substitute.For<ITicketData>()
            };

            // Act
            var template = _odds.Render(_ticket, mockTicketData, _getOddsTypeNameFunc);

            // Assert
            Assert.AreEqual("-", template.odds);
        }

        [Test]
        public void Render_WithNullGetOddsTypeNameFunc_AlwaysReturnsHyphenOdds()
        {
            // Arrange
            GetCachePropertyById nullFunc = null;

            // Act
            var template = _odds.Render(_ticket, _ticketData, nullFunc);

            // Assert
            Assert.AreEqual("-", template.odds);
        }

        [Test]
        public void Render_WithMockedGetOddsTypeNameFunc_AlwaysReturnsHyphenOdds()
        {
            // Arrange
            var mockFunc = Substitute.For<GetCachePropertyById>();
            mockFunc(Arg.Any<object>()).Returns("MockedOddsType");

            // Act
            var template = _odds.Render(_ticket, _ticketData, mockFunc);

            // Assert
            Assert.AreEqual("-", template.odds);
        }

        [Test]
        public void Render_WithAllPropertiesChanged_AlwaysReturnsHyphenOddsAndEmptyOddsType()
        {
            // Arrange
            _ticket.TransId = 999;
            _ticket.Odds = 3.75m;
            _ticket.OddsType = "Dec";
            _ticket.BetTypeId = 8888;
            _ticket.SportTypeId = 5;
            _ticket.Stake = 500;
            _ticket.Currency = "JPY";
            _ticket.Status = "Lose";
            _ticket.Winlost = -200;

            // Act
            var template = _odds.Render(_ticket, _ticketData, _getOddsTypeNameFunc);

            // Assert
            Assert.AreEqual("-", template.odds);
            Assert.AreEqual(string.Empty, template.oddsType);
        }

        [Test]
        public void Render_WithExtremeValues_AlwaysReturnsHyphenOdds()
        {
            // Arrange
            _ticket.Odds = decimal.MaxValue;
            _ticket.Stake = decimal.MaxValue;
            _ticket.Winlost = decimal.MinValue;
            _ticket.TransId = long.MaxValue;
            _ticket.BetTypeId = int.MaxValue;
            _ticket.SportTypeId = int.MaxValue;

            // Act
            var template = _odds.Render(_ticket, _ticketData, _getOddsTypeNameFunc);

            // Assert
            Assert.AreEqual("-", template.odds);
        }

        [Test]
        public void Render_WithMinimalValues_AlwaysReturnsHyphenOdds()
        {
            // Arrange
            _ticket.Odds = decimal.MinValue;
            _ticket.Stake = decimal.MinValue;
            _ticket.Winlost = decimal.MinValue;
            _ticket.TransId = long.MinValue;
            _ticket.BetTypeId = int.MinValue;
            _ticket.SportTypeId = int.MinValue;

            // Act
            var template = _odds.Render(_ticket, _ticketData, _getOddsTypeNameFunc);

            // Assert
            Assert.AreEqual("-", template.odds);
        }

        [Test]
        public void Render_WithSpecialCharactersInOddsType_AlwaysReturnsEmptyOddsType()
        {
            // Arrange
            _ticket.OddsType = "!@#$%^&*()";

            // Act
            var template = _odds.Render(_ticket, _ticketData, _getOddsTypeNameFunc);

            // Assert
            Assert.AreEqual(string.Empty, template.oddsType);
        }

        [Test]
        public void Render_WithLongOddsType_AlwaysReturnsEmptyOddsType()
        {
            // Arrange
            _ticket.OddsType = "very_long_odds_type_string_that_contains_many_characters";

            // Act
            var template = _odds.Render(_ticket, _ticketData, _getOddsTypeNameFunc);

            // Assert
            Assert.AreEqual(string.Empty, template.oddsType);
        }

        [Test]
        public void Render_WithUnicodeOddsType_AlwaysReturnsEmptyOddsType()
        {
            // Arrange
            _ticket.OddsType = "OddsType中文";

            // Act
            var template = _odds.Render(_ticket, _ticketData, _getOddsTypeNameFunc);

            // Assert
            Assert.AreEqual(string.Empty, template.oddsType);
        }

        [Test]
        public void Render_WithEmojiOddsType_AlwaysReturnsEmptyOddsType()
        {
            // Arrange
            _ticket.OddsType = "🎯🎲🎮";

            // Act
            var template = _odds.Render(_ticket, _ticketData, _getOddsTypeNameFunc);

            // Assert
            Assert.AreEqual(string.Empty, template.oddsType);
        }

        [Test]
        public void Render_WithHtmlTagsInOddsType_AlwaysReturnsEmptyOddsType()
        {
            // Arrange
            _ticket.OddsType = "<span>OddsType</span>";

            // Act
            var template = _odds.Render(_ticket, _ticketData, _getOddsTypeNameFunc);

            // Assert
            Assert.AreEqual(string.Empty, template.oddsType);
        }

        [Test]
        public void Render_WithSqlInjectionInOddsType_AlwaysReturnsEmptyOddsType()
        {
            // Arrange
            _ticket.OddsType = "'; DROP TABLE users; --";

            // Act
            var template = _odds.Render(_ticket, _ticketData, _getOddsTypeNameFunc);

            // Assert
            Assert.AreEqual(string.Empty, template.oddsType);
        }

        [Test]
        public void Render_WithXssInOddsType_AlwaysReturnsEmptyOddsType()
        {
            // Arrange
            _ticket.OddsType = "<script>alert('xss')</script>";

            // Act
            var template = _odds.Render(_ticket, _ticketData, _getOddsTypeNameFunc);

            // Assert
            Assert.AreEqual(string.Empty, template.oddsType);
        }

        [Test]
        public void Render_WithNullTicket_AlwaysReturnsHyphenOdds()
        {
            // Arrange
            ITicket nullTicket = null;

            // Act
            var template = _odds.Render(nullTicket, _ticketData, _getOddsTypeNameFunc);

            // Assert
            Assert.AreEqual("-", template.odds);
        }

        [Test]
        public void Render_WithAllNullParameters_AlwaysReturnsHyphenOdds()
        {
            // Arrange
            ITicket nullTicket = null;
            List<ITicketData> nullTicketData = null;
            GetCachePropertyById nullFunc = null;

            // Act
            var template = _odds.Render(nullTicket, nullTicketData, nullFunc);

            // Assert
            Assert.AreEqual("-", template.odds);
        }
    }
}
