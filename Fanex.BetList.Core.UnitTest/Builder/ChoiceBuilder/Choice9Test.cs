// --------------------------------------------------------------------------
// <copyright file="Choice9Test.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2014.5.30">Created</change>
// </history>

namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using System.Linq;
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Templates;
    using Fanex.BetList.Core.UnitTest.Stubs;
    using Fanex.BetList.Core.UnitTest.Templates;
    using Fanex.BetList.Core.UnitTest.Ultils.Enums;
    using HtmlAgilityPack;
    using NUnit.Framework;
    using Ultils;

    public class Choice9Test
    {
        private Choice9 _choice;
        private ITicketHelper _ticketHelperStub;
        private ITicket _ticket;
        private const string Template = "<a id=\"hidden0\" href=\"{0}\">{1}</a><br><br><div id=\"divEvent_{2}\" style=\"display: none\"></div>";

        [SetUp]
        public void Setup()
        {
            _choice = new Choice9();
            _ticketHelperStub = new TicketHelperStub();
            _ticket = TestData.CreateTicket((int)BetTypes.MixParlay);
        }

        [Test]
        public void TicketHelperClientMixParlaySubbetsDetailFunctionIsABC_HrefToABC()
        {
            _ticketHelperStub.ClientMixParlaySubbetsDetailFunction = "ABC";
            HtmlDocument htmlDoc = GetHtmlDoc(_ticket);

            string expected = string.Format(Template, _ticketHelperStub.ClientMixParlaySubbetsDetailFunction, _ticketHelperStub.GetBetTypeNameById(_ticket.BetTypeId), _ticket.TransId);
            string actual = htmlDoc.DocumentNode.OuterHtml;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TicketHelperClientMixParlaySubbetsDetailFunctionIsEmpty_HrefClientMixParlayDetailFunction()
        {
            _ticketHelperStub.ClientMixParlaySubbetsDetailFunction = string.Empty;
            HtmlDocument htmlDoc = GetHtmlDoc(_ticket);
            string clientMixParlayDetailFunction = string.Format("javascript:showMP(&quot;{0}&quot;,&quot;{1}&quot;);", _ticket.TransId, _ticket.RefNo);

            string expected = string.Format(Template, clientMixParlayDetailFunction, _ticketHelperStub.GetBetTypeNameById(_ticket.BetTypeId), _ticket.TransId);
            string actual = htmlDoc.DocumentNode.OuterHtml;

            Assert.AreEqual(expected, actual);
        }

        private HtmlDocument GetHtmlDoc(ITicket ticket, bool isShowSoreMap = false)
        {
            Choice_Template choiceTemplate = _choice.Render(ticket, _ticketHelperStub, null, isShowSoreMap);
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(choiceTemplate.ToString());
            return htmlDoc;
        }
    }
}
