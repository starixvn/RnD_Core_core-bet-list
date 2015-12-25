namespace Fanex.BetList.CockFighting.Tests.Builder.OddsBuilder
{
    using Fanex.BetList.Core.Builder.OddsBuilder;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Odds1003Tests
    {
        private Odds1003 _odds;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _odds = new Odds1003();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void Render_MemberChoiceIsFTD_OddsIs1_88()
        {
            _ticket.TransDesc = "Betchoice=4;CockOwne1r=ABC,b";
            _ticket.Odds = 88;

            var oddTemplate = _odds.Render(_ticket, null, GetCachePropertyByIdStub);

            Assert.AreEqual("1:88", oddTemplate.odds);
        }

        [Test]
        public void Render_MemberChoiceIsBDD_OddsIs1_8()
        {
            _ticket.TransDesc = "Betchoice=3;CockOwne1r=ABC,b";
            _ticket.Odds = 8;

            var oddTemplate = _odds.Render(_ticket, null, GetCachePropertyByIdStub);

            Assert.AreEqual("1:8", oddTemplate.odds);
        }

        [TestCase("1")]
        [TestCase("2")]
        public void Render_MemberChoiceIsOthersExceptBDDandFTD_ValidOdds(string betChoiceId)
        {
            var expectedOddsValue = 1;
            _ticket.TransDesc = string.Format("Betchoice={0};CockOwne1r=ABC,b", betChoiceId);
            _ticket.Odds = expectedOddsValue;

            var oddTemplate = _odds.Render(_ticket, null, GetCachePropertyByIdStub);

            Assert.AreEqual(expectedOddsValue.ToString(), oddTemplate.odds);
        }

        private string GetCachePropertyByIdStub(object id)
        {
            return id.ToString();
        }
    }
}