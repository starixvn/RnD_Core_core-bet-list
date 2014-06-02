// --------------------------------------------------------------------------
// <copyright file="ChoiceCreator.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2014.6.2">Created</change>
// </history>

namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Templates;
    using Fanex.BetList.Core.UnitTest.Stubs;
    using Fanex.BetList.Core.UnitTest.Ultils;
    using HtmlAgilityPack;
    using System.Linq;

    public class ChoiceCreator
    {
        private IChoice _choice;
        private ITicketHelper _ticketHelperStub;
        private ITicket _ticket;
        private bool _isShowScoreMap;
        private HtmlDocument _htmlDoc;

        public ChoiceCreator(IChoice choice, int betTypeId)
        {
            _choice = choice;
            _ticketHelperStub = new TicketHelperStub();
            _ticket = TestData.CreateTicket(betTypeId);
            _isShowScoreMap = false;
            _htmlDoc = null;
        }

        public IChoice Choice
        {
            get
            {
                return _choice;
            }
        }

        public ITicket Ticket
        {
            get
            {
                return _ticket;
            }
        }

        public ITicketHelper TicketHelperStub
        {
            get
            {
                return _ticketHelperStub;
            }
        }

        public bool IsShowScoreMap
        {
            get
            {
                return _isShowScoreMap;
            }

            set
            {
                _isShowScoreMap = value;
            }
        }

        public HtmlDocument HtmlDocument
        {
            get
            {
                if (ReferenceEquals(_htmlDoc, null))
                {
                    Choice_Template choiceTemplate = _choice.Render(_ticket, _ticketHelperStub, null, _isShowScoreMap);
                    _htmlDoc = new HtmlDocument();
                    _htmlDoc.LoadHtml(choiceTemplate.ToString());
                }

                return _htmlDoc;
            }
        }

        public string GetOuterHtml(string xPath)
        {
            HtmlNode htmlNode = HtmlDocument.DocumentNode.SelectNodes(xPath).FirstOrDefault();

            return htmlNode.OuterHtml;
        }

        public string GetInnerHtml(string xPath)
        {
            HtmlNode htmlNode = HtmlDocument.DocumentNode.SelectNodes(xPath).FirstOrDefault();

            return htmlNode.InnerHtml;
        }

        public string GetPreviousSiblingOuterHtml(string xPath, int length = 1)
        {
            HtmlNode htmlNode = HtmlDocument.DocumentNode.SelectNodes(xPath).FirstOrDefault();

            for (int i = 0; i < length; i++)
            {
                htmlNode = htmlNode.PreviousSibling;
            }

            return htmlNode.OuterHtml;
        }

        public string GetFirstChildInnerHtml(string xPath)
        {
            HtmlNode htmlNode = HtmlDocument.DocumentNode.SelectNodes(xPath).FirstOrDefault();

            return htmlNode.FirstChild.InnerHtml;
        }

        public string GetFirstChildNextSiblingOuterHtml(string xPath)
        {
            HtmlNode htmlNode = HtmlDocument.DocumentNode.SelectNodes(xPath).FirstOrDefault();

            return htmlNode.FirstChild.NextSibling.OuterHtml;
        }

        public string GetHtmlClassValue(string xPath)
        {
            HtmlNode htmlNode = HtmlDocument.DocumentNode.SelectNodes(xPath).FirstOrDefault();

            return htmlNode.Attributes["class"].Value;
        }

        public bool IsExist(string xPath)
        {
            HtmlNodeCollection htmlNode = HtmlDocument.DocumentNode.SelectNodes(xPath);

            return !ReferenceEquals(htmlNode, null);
        }
    }
}