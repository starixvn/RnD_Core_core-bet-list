namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Entities;
    using NPOI.SS.UserModel;

    public class Choice18000 : Choice1
    {
        public override List<IRichTextString> RenderRTF(
            ITicket ticket, 
            ITicketHelper ticketHelper, 
            List<ITicketData> ticketData, 
            bool isShowScoreMap, 
            RTFHelper rtfHelper)
        {
            RTFHelper tmpRtfHelper = rtfHelper.Clone();

            string betTypeName = ticketHelper.GetBetTypeNameById(ticket.BetTypeId);
            tmpRtfHelper.RTFRenderer.AddText(betTypeName, tmpRtfHelper.PosFont);

            var choice = tmpRtfHelper.RTFRenderer.Render();
            tmpRtfHelper.RTFRenderer.Clear();

            return new List<IRichTextString>() { choice };
        }

        protected override void BuildChoice(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            string betTypeName = ticketHelper.GetBetTypeNameById(ticket.BetTypeId);
            string custid = ticket.CustId.ToString();
            string showColosussBetsDetailsHtml = BuildShowColossusBetsDetails(
                ticket.RefNo,
                ticket.WinlostDate.ToString(),
                ticket.TransId.ToString(),
                custid,
                ticket.BetCheck);

            string colosussBetsHtml = BuildColossusBetsHtml(betTypeName, showColosussBetsDetailsHtml);

            Template.SetValue(colosussBetsHtml);
        }

        private string BuildShowColossusBetsDetails(string refNo, string winlossDate, string transId, string custId, string betCheck)
        {
            string[] detailsFunctionCompositions = new string[]
                                            {
                                                "showColossusBetsDetails(&quot;",
                                                refNo,
                                                "&quot;",
                                                ",&quot;",
                                                winlossDate,
                                                "&quot;",
                                                ",&quot;",
                                                transId,
                                                "&quot;",
                                                ",&quot;",
                                                custId,
                                                "&quot;",
                                                ",&quot;",
                                                betCheck,
                                                "&quot;);"
                                            };

            string detailsFunction = string.Join(null, detailsFunctionCompositions);

            string[] detailsFunctionDivCompositions = new string[]
                                            {
                                                "<div class=\"detail colossus-bets-details\" onclick='",
                                                detailsFunction,
                                                "'>",
                                                "<a style=\"color:#755200;font-weight:bold\" href=\"javascript:void(&quot;&quot;);\">",
                                                CoreBetList.details,
                                                "</a>",
                                                "</div>"
                                            };

            string markups = string.Join(null, detailsFunctionDivCompositions);

            return markups;
        }

        private string BuildColossusBetsHtml(
                        string betTypeName,
                        string detailsFunctionDiv)
        {
            string[] htmlElements = new string[]
                                                {
                                                    "<div class=\"combinationLink\"><span class=\"main-ticket\">",
                                                    betTypeName,
                                                    "</span>",
                                                    "</div>",
                                                    detailsFunctionDiv
                                                };

            string html = string.Join(null, htmlElements);

            return html;
        }
    }
}