namespace Fanex.BetList.LiveCasinoCore.Tests.Builder.StakeBuilder
{
    using Core.Builder.StakeBuilder;
    using Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Stake15798Test
    {
        private Stake15798 _stake;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _stake = new Stake15798();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();

            // Setup default ticket properties
            _ticket.TransId = 1;
            _ticket.Stake = 100;
            _ticket.ActualStake = 100;
            _ticket.Currency = "USD";
            _ticket.BetTypeId = 15798;
            _ticket.RefCode = 0;
            _ticket.WalletType = 2; // Cash Wallet
            _ticket.ExtraInfo = string.Empty;
        }

        [Test]
        public void BuildStake_WithPositiveStake_AlwaysReturnsHyphen()
        {
            // Arrange
            _ticket.Stake = 100;

            // Act
            var template = _stake.Render(_ticket, _ticketHelper);

            // Assert
            Assert.AreEqual("-", template.stake);
        }

        [Test]
        public void BuildStake_WithZeroStake_AlwaysReturnsHyphen()
        {
            // Arrange
            _ticket.Stake = 0;

            // Act
            var template = _stake.Render(_ticket, _ticketHelper);

            // Assert
            Assert.AreEqual("-", template.stake);
        }

        [Test]
        public void BuildStake_WithNegativeStake_AlwaysReturnsHyphen()
        {
            // Arrange
            _ticket.Stake = -50;

            // Act
            var template = _stake.Render(_ticket, _ticketHelper);

            // Assert
            Assert.AreEqual("-", template.stake);
        }

        [Test]
        public void BuildStake_WithLargeStake_AlwaysReturnsHyphen()
        {
            // Arrange
            _ticket.Stake = 999999.99m;

            // Act
            var template = _stake.Render(_ticket, _ticketHelper);

            // Assert
            Assert.AreEqual("-", template.stake);
        }

        [Test]
        public void BuildStake_WithDecimalStake_AlwaysReturnsHyphen()
        {
            // Arrange
            _ticket.Stake = 123.45m;

            // Act
            var template = _stake.Render(_ticket, _ticketHelper);

            // Assert
            Assert.AreEqual("-", template.stake);
        }

        [Test]
        public void BuildStake_WithDifferentActualStake_AlwaysReturnsHyphen()
        {
            // Arrange
            _ticket.Stake = 100;
            _ticket.ActualStake = 80; // Different from Stake

            // Act
            var template = _stake.Render(_ticket, _ticketHelper);

            // Assert
            Assert.AreEqual("-", template.stake);
        }

        [Test]
        public void BuildStake_WithZeroActualStake_AlwaysReturnsHyphen()
        {
            // Arrange
            _ticket.Stake = 100;
            _ticket.ActualStake = 0;

            // Act
            var template = _stake.Render(_ticket, _ticketHelper);

            // Assert
            Assert.AreEqual("-", template.stake);
        }

        [Test]
        public void BuildStake_WithNegativeActualStake_AlwaysReturnsHyphen()
        {
            // Arrange
            _ticket.Stake = 100;
            _ticket.ActualStake = -20;

            // Act
            var template = _stake.Render(_ticket, _ticketHelper);

            // Assert
            Assert.AreEqual("-", template.stake);
        }

        [Test]
        public void BuildStake_WithDifferentCurrency_AlwaysReturnsHyphen()
        {
            // Arrange
            _ticket.Stake = 100;
            _ticket.Currency = "EUR";

            // Act
            var template = _stake.Render(_ticket, _ticketHelper);

            // Assert
            Assert.AreEqual("-", template.stake);
        }

        [Test]
        public void BuildStake_WithEmptyCurrency_AlwaysReturnsHyphen()
        {
            // Arrange
            _ticket.Stake = 100;
            _ticket.Currency = "";

            // Act
            var template = _stake.Render(_ticket, _ticketHelper);

            // Assert
            Assert.AreEqual("-", template.stake);
        }

        [Test]
        public void BuildStake_WithNullCurrency_AlwaysReturnsHyphen()
        {
            // Arrange
            _ticket.Stake = 100;
            _ticket.Currency = null;

            // Act
            var template = _stake.Render(_ticket, _ticketHelper);

            // Assert
            Assert.AreEqual("-", template.stake);
        }

        [Test]
        public void BuildStake_WithDifferentBetTypeId_AlwaysReturnsHyphen()
        {
            // Arrange
            _ticket.Stake = 100;
            _ticket.BetTypeId = 9999;

            // Act
            var template = _stake.Render(_ticket, _ticketHelper);

            // Assert
            Assert.AreEqual("-", template.stake);
        }

        [Test]
        public void BuildStake_WithZeroBetTypeId_AlwaysReturnsHyphen()
        {
            // Arrange
            _ticket.Stake = 100;
            _ticket.BetTypeId = 0;

            // Act
            var template = _stake.Render(_ticket, _ticketHelper);

            // Assert
            Assert.AreEqual("-", template.stake);
        }

        [Test]
        public void BuildStake_WithNegativeBetTypeId_AlwaysReturnsHyphen()
        {
            // Arrange
            _ticket.Stake = 100;
            _ticket.BetTypeId = -1;

            // Act
            var template = _stake.Render(_ticket, _ticketHelper);

            // Assert
            Assert.AreEqual("-", template.stake);
        }

        [Test]
        public void BuildStake_WithDifferentRefCode_AlwaysReturnsHyphen()
        {
            // Arrange
            _ticket.Stake = 100;
            _ticket.RefCode = 123;

            // Act
            var template = _stake.Render(_ticket, _ticketHelper);

            // Assert
            Assert.AreEqual("-", template.stake);
        }

        [Test]
        public void BuildStake_WithZeroRefCode_AlwaysReturnsHyphen()
        {
            // Arrange
            _ticket.Stake = 100;
            _ticket.RefCode = 0;

            // Act
            var template = _stake.Render(_ticket, _ticketHelper);

            // Assert
            Assert.AreEqual("-", template.stake);
        }

        [Test]
        public void BuildStake_WithNegativeRefCode_AlwaysReturnsHyphen()
        {
            // Arrange
            _ticket.Stake = 100;
            _ticket.RefCode = -1;

            // Act
            var template = _stake.Render(_ticket, _ticketHelper);

            // Assert
            Assert.AreEqual("-", template.stake);
        }

        [Test]
        public void BuildStake_WithDifferentWalletType_AlwaysReturnsHyphen()
        {
            // Arrange
            _ticket.Stake = 100;
            _ticket.WalletType = 1; // Bonus Wallet

            // Act
            var template = _stake.Render(_ticket, _ticketHelper);

            // Assert
            Assert.AreEqual("-", template.stake);
        }

        [Test]
        public void BuildStake_WithZeroWalletType_AlwaysReturnsHyphen()
        {
            // Arrange
            _ticket.Stake = 100;
            _ticket.WalletType = 0;

            // Act
            var template = _stake.Render(_ticket, _ticketHelper);

            // Assert
            Assert.AreEqual("-", template.stake);
        }

        [Test]
        public void BuildStake_WithExtraInfo_AlwaysReturnsHyphen()
        {
            // Arrange
            _ticket.Stake = 100;
            _ticket.ExtraInfo = "<p25 s=\"100\" a=\"100\"/>";

            // Act
            var template = _stake.Render(_ticket, _ticketHelper);

            // Assert
            Assert.AreEqual("-", template.stake);
        }

        [Test]
        public void BuildStake_WithEmptyExtraInfo_AlwaysReturnsHyphen()
        {
            // Arrange
            _ticket.Stake = 100;
            _ticket.ExtraInfo = "";

            // Act
            var template = _stake.Render(_ticket, _ticketHelper);

            // Assert
            Assert.AreEqual("-", template.stake);
        }

        [Test]
        public void BuildStake_WithNullExtraInfo_AlwaysReturnsHyphen()
        {
            // Arrange
            _ticket.Stake = 100;
            _ticket.ExtraInfo = null;

            // Act
            var template = _stake.Render(_ticket, _ticketHelper);

            // Assert
            Assert.AreEqual("-", template.stake);
        }

        [Test]
        public void BuildStake_WithSpecialCharactersInExtraInfo_AlwaysReturnsHyphen()
        {
            // Arrange
            _ticket.Stake = 100;
            _ticket.ExtraInfo = "<p1 d=\"10\" />";

            // Act
            var template = _stake.Render(_ticket, _ticketHelper);

            // Assert
            Assert.AreEqual("-", template.stake);
        }

        [Test]
        public void BuildStake_WithLongExtraInfo_AlwaysReturnsHyphen()
        {
            // Arrange
            _ticket.Stake = 100;
            _ticket.ExtraInfo = "<p25 s=\"1000\" a=\"1000\"/><p1 d=\"5\" /><p37 t=\"2\" />";

            // Act
            var template = _stake.Render(_ticket, _ticketHelper);

            // Assert
            Assert.AreEqual("-", template.stake);
        }

        [Test]
        public void BuildStake_WithAllPropertiesChanged_AlwaysReturnsHyphen()
        {
            // Arrange
            _ticket.TransId = 999;
            _ticket.Stake = 500;
            _ticket.ActualStake = 450;
            _ticket.Currency = "JPY";
            _ticket.BetTypeId = 8888;
            _ticket.RefCode = 777;
            _ticket.WalletType = 3;
            _ticket.ExtraInfo = "<p25 s=\"500\" a=\"450\"/>";

            // Act
            var template = _stake.Render(_ticket, _ticketHelper);

            // Assert
            Assert.AreEqual("-", template.stake);
        }

        [Test]
        public void BuildStake_WithNullTicketHelper_AlwaysReturnsHyphen()
        {
            // Arrange
            _ticket.Stake = 100;
            ITicketHelper nullTicketHelper = null;

            // Act
            var template = _stake.Render(_ticket, nullTicketHelper);

            // Assert
            Assert.AreEqual("-", template.stake);
        }

        [Test]
        public void BuildStake_WithMockedTicketHelper_AlwaysReturnsHyphen()
        {
            // Arrange
            _ticket.Stake = 100;
            var mockTicketHelper = Substitute.For<ITicketHelper>();
            mockTicketHelper.GetResourceData(Arg.Any<string>(), Arg.Any<string>()).Returns("MockedValue");

            // Act
            var template = _stake.Render(_ticket, mockTicketHelper);

            // Assert
            Assert.AreEqual("-", template.stake);
        }

        [Test]
        public void BuildStake_WithDifferentTransId_AlwaysReturnsHyphen()
        {
            // Arrange
            _ticket.Stake = 100;
            _ticket.TransId = 999999;

            // Act
            var template = _stake.Render(_ticket, _ticketHelper);

            // Assert
            Assert.AreEqual("-", template.stake);
        }

        [Test]
        public void BuildStake_WithZeroTransId_AlwaysReturnsHyphen()
        {
            // Arrange
            _ticket.Stake = 100;
            _ticket.TransId = 0;

            // Act
            var template = _stake.Render(_ticket, _ticketHelper);

            // Assert
            Assert.AreEqual("-", template.stake);
        }

        [Test]
        public void BuildStake_WithNegativeTransId_AlwaysReturnsHyphen()
        {
            // Arrange
            _ticket.Stake = 100;
            _ticket.TransId = -1;

            // Act
            var template = _stake.Render(_ticket, _ticketHelper);

            // Assert
            Assert.AreEqual("-", template.stake);
        }

        [Test]
        public void BuildStake_WithExtremeValues_AlwaysReturnsHyphen()
        {
            // Arrange
            _ticket.Stake = decimal.MaxValue;
            _ticket.ActualStake = decimal.MinValue;
            _ticket.TransId = long.MaxValue;
            _ticket.BetTypeId = int.MaxValue;
            _ticket.RefCode = int.MaxValue;
            _ticket.WalletType = 255; // byte.MaxValue

            // Act
            var template = _stake.Render(_ticket, _ticketHelper);

            // Assert
            Assert.AreEqual("-", template.stake);
        }

        [Test]
        public void BuildStake_WithMinimalValues_AlwaysReturnsHyphen()
        {
            // Arrange
            _ticket.Stake = decimal.MinValue;
            _ticket.ActualStake = decimal.MinValue;
            _ticket.TransId = long.MinValue;
            _ticket.BetTypeId = int.MinValue;
            _ticket.RefCode = int.MinValue;
            _ticket.WalletType = 0; // byte.MinValue

            // Act
            var template = _stake.Render(_ticket, _ticketHelper);

            // Assert
            Assert.AreEqual("-", template.stake);
        }
    }
}
