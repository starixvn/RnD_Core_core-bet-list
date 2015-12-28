namespace Fanex.BetList.ColossusBets.Tests.Builder.ChoiceBuilder
{
    using System;
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice18000Tests
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice18000();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void BuildChoice_ReturnTemplate()
        {
            //// Arrange
            _ticketHelper.GetBetTypeNameById(Arg.Any<int>()).Returns("Colossus Bets");
            _ticket.RefNo = "300";
            _ticket.WinlostDate = new DateTime(2014, 01, 01, 12, 00, 00);
            _ticket.CustId = 10000;
            _ticket.TransId = 200;
            _ticket.BetCheck = "BetCheck";

            //// Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            //// Assert
            string expected = "<div class=\"combinationLink\">" +
                                    "<span class=\"main-ticket\">Colossus Bets</span>" +
                                "</div>" +
                                "<div class=\"detail colossus-bets-details\" " +
                                "onclick=\'showColossusBetsDetails(&quot;300&quot;,&quot;1/1/2014 12:00:00 PM&quot;,&quot;200&quot;,&quot;10000&quot;,&quot;BetCheck&quot;);'>" +
                                "<a style=\"color:#755200;font-weight:bold\" href=\"javascript:void(&quot;&quot;);\">Details</a></div>";

            Assert.AreEqual(expected, _choice.Template.ToString());
        }
    }
}