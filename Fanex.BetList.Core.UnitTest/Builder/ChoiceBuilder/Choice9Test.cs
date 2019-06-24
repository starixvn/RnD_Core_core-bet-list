namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using System.Diagnostics.CodeAnalysis;
    using Constants;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    [SuppressMessage("StyleCop.CSharp.StylecopPlus.Rules", "SP2100:CodeLineMustNotBeLongerThan", Justification = "Reviewed.")]
    public class Choice9Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice9();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.MixParlay;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void Render_ClientMixParlaySubbetsDetailFunctionIsEmpty_ReturnTemplate()
        {
            // Arrange
            _ticketHelper.ClientMixParlaySubbetsDetailFunction = string.Empty;
            _ticketHelper.GetBetTypeNameById(Arg.Any<int>()).Returns("Bet type name");
            _ticket.CustId = 1;
            _ticket.TransId = 1000;
            _ticket.RefNo = "2000";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expected = "<a id=\"hidden0\" href=\"javascript:showMP(1000, '2000', 1);\">Bet type name</a><br/><div id=\"divEvent_1000\" style=\"display: none\" ></div>";
            Assert.AreEqual(expected, _choice.Template.ToString());
        }

        [Test]
        public void Render_ClientMixParlaySubbetsDetailFunctionIsNotEmpty_ReturnTemplate()
        {
            // Arrange
            _ticketHelper.ClientMixParlaySubbetsDetailFunction = "Client MixParlay";
            _ticketHelper.GetBetTypeNameById(Arg.Any<int>()).Returns("Bet type name");
            _ticket.TransId = 1000;
            _ticket.RefNo = "2000";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expected = "<a id=\"hidden0\" href=\"Client MixParlay\">Bet type name</a><br/><div id=\"divEvent_1000\" style=\"display: none\" ></div>";
            Assert.AreEqual(expected, _choice.Template.ToString());
        }
    }
}