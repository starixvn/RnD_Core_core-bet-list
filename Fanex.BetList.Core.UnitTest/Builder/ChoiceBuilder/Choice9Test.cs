namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using System.Diagnostics.CodeAnalysis;
    using Common.Enums;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    /// Unit test for Choice9 class.
    /// </summary>
    [TestFixture]
    [SuppressMessage("StyleCop.CSharp.Nexcel.NexcelCustomRules", "SP2100:CodeLineMustNotBeLongerThan", Justification = "Reviewed.")]
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

        /// <summary>
        /// Return template choice.
        /// </summary>
        [Test]
        public void Render_ClientMixParlaySubbetsDetailFunctionIsEmpty_ReturnTemplate()
        {
            // Arrange
            _ticketHelper.ClientMixParlaySubbetsDetailFunction = string.Empty;
            _ticketHelper.GetBetTypeNameById(Arg.Any<int>()).Returns("Bet type name");
            _ticket.TransId = 1000;
            _ticket.RefNo = "2000";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expected = "<a id=\"hidden0\" href=\"javascript:showMP(&quot;1000&quot;,&quot;2000&quot;);\">Bet type name</a><br/><br/><div id=\"divEvent_1000\" style=\"display: none\" ></div>";
            Assert.AreEqual(expected, _choice.Template.ToString());
        }

        /// <summary>
        /// Return template choice.
        /// </summary>
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
            string expected = "<a id=\"hidden0\" href=\"Client MixParlay\">Bet type name</a><br/><br/><div id=\"divEvent_1000\" style=\"display: none\" ></div>";
            Assert.AreEqual(expected, _choice.Template.ToString());
        }
    }
}