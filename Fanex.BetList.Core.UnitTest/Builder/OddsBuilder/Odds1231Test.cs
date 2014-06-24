using System;
using Fanex.BetList.Core.UnitTest.Common.Enums;

namespace Fanex.BetList.Core.UnitTest.Builder.OddsBuilder
{
    using Core.Builder.OddsBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Odds1231Test
    {
        private Odds1231 _odds;
        private ITicket _ticket;

        [SetUp]
        public void Setup()
        {
            _odds = new Odds1231();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.VHR_Win;
        }

        [Test]
        public void OddsTypeIsNull_OddsTypeIsZero()
        {
            // Arrange
            _ticket.OddsType = null;

            // Act
            _odds.Render(_ticket, null, Substitute.For<GetCachePropertyById>());

            // Assert
            var expectedOdds = "<font color='#B50000'>-0.73</font>";
            Assert.AreEqual(expectedOdds, _odds.Template.odds);
        }

        [Test]
        public void OddsTypeIsNull_OddsTypeIsZero1()
        {
            // Arrange
            _ticket.OddsType = "1";

            // Act
            _odds.Render(_ticket, null, GetCachePropertyByIdStub);

            // Assert
            var expectedOdds = "0.27";
            Assert.AreEqual(expectedOdds, _odds.Template.odds);
        }

        [Test]
        public void OddsTypeIsNull_OddsTypeIsZero12()
        {
            // Arrange
            _ticket.OddsType = "1";

            // Act
            _odds.Render(_ticket, null, GetCachePropertyByIdStub);

            // Assert
            var expectedOdds = "1";
            Assert.AreEqual(expectedOdds, _odds.Template.oddsType);
        }

        private string GetCachePropertyByIdStub(object id)
        {
            return id.ToString();
        }
    }
}