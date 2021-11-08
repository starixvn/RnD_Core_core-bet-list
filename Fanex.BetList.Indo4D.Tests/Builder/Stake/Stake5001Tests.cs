using System.Collections.Generic;
using Fanex.BetList.Core.Builder.StakeBuilder;
using Fanex.BetList.Core.Constants;
using Fanex.BetList.Core.Entities;
using Fanex.BetList.Core.UnitTest.Common;
using NUnit.Framework;
using System.Linq;
using NSubstitute;

namespace Fanex.BetList.Indo4D.Tests.Builder.Stake
{
    public class Stake5001Tests
    {
        [TestCase(100, 30, "<div>100</div><font class='minus-value'>-70</font><div>30</div>")]
        [TestCase(3000, 30, "<div>3,000</div><font class='minus-value'>-2,970</font><div>30</div>")]
        [TestCase(3000.01, 30, "<div>3,000.01</div><font class='minus-value'>-2,970.01</font><div>30</div>")]
        [TestCase(3000.123, 30, "<div>3,000.12</div><font class='minus-value'>-2,970.12</font><div>30</div>")]
        [TestCase(3000, 2900.45, "<div>3,000</div><font class='minus-value'>-99.55</font><div>2,900.45</div>")]
        public void BuilStake_Always_HideBonus(decimal stakeValue, decimal acutalStake, string expectedStake)
        {
            // Arrange.
            var ticket = TestData.CreateTicket(Indo4DBetTypes.FourD);
            ticket.ActualStake = acutalStake;
            ticket.Stake = stakeValue;
            var stake = new Stake5001();
            var ticketHelper = Substitute.For<ITicketHelper>();

            // Act.
            var template = stake.Render(ticket, ticketHelper);

            // Assert.

            Assert.AreEqual(expectedStake, template.stake);
        }


        [Test]
        public void BuildStake_Always_RemoveHtmlTag()
        {
            // Arrange
            var ticket = TestData.CreateTicket(Indo4DBetTypes.FourD);
            var stake = new Stake5001();
            var htmlTags = new List<string> { "<div>", "</div>", "</font>", "<font color='#B50000'>" };
            var ticketHelper = Substitute.For<ITicketHelper>();

            // Act
            var template = stake.RenderRTF(ticket, ticketHelper, new RTFHelper().Clone());

            // Assert
            Assert.IsFalse(htmlTags.Any(i => template.String.Contains(i)));
        }
    }
}
