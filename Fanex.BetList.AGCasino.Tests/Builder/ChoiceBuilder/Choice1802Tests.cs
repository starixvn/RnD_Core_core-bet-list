namespace Fanex.BetList.AGCasino.Tests.Builder.ChoiceBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice1802Tests
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice1802();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void BuildBetTeam_TypeIsOutOf101To106And110_BetChoiceDoesNotContainVALvalue()
        {
            //// Arrange
            var choiceName = "Choice Name";
            _ticket.BetTypeId = 1802;

            _ticket.TransDesc = "round=DSP*tableCode=va21*game=BAC*type=107*val=2,3,4,5";

            _ticketHelper.GetResourceData("AG_Roulette", "107").Returns(choiceName);

            //// Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            //// Assert
            var expectedChoiceName = "Choice Name";
            Assert.AreEqual(expectedChoiceName, _choice.Template.betTeam);
        }

        [TestCase(101)]
        [TestCase(102)]
        [TestCase(103)]
        [TestCase(104)]
        [TestCase(105)]
        [TestCase(106)]
        [TestCase(110)]
        public void BuildBetTeam_TypeIsIn101To106And110_BetChoiceContainsVALvalue(int type)
        {
            //// Arrange
            var choiceName = "Choice Name";
            _ticket.BetTypeId = 1802;

            _ticket.TransDesc = "round=DSP*tableCode=va21*game=BAC*type=" + type + "*val=2,3,4,5";

            _ticketHelper.GetResourceData("AG_Roulette", type.ToString()).Returns(choiceName);

            //// Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            //// Assert
            var expectedChoiceName = "Choice Name (2,3,4,5)";
            Assert.AreEqual(expectedChoiceName, _choice.Template.betTeam);
        }
    }
}
