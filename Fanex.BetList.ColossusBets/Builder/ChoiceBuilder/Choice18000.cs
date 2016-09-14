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
            tmpRtfHelper.RTFRenderer.AddText($"{betTypeName} - {ticket.BetCheck}", tmpRtfHelper.PosFont);

            var choice = tmpRtfHelper.RTFRenderer.Render();
            tmpRtfHelper.RTFRenderer.Clear();

            return new List<IRichTextString>() { choice };
        }

        protected override void BuildChoice(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            string betTypeName = ticketHelper.GetBetTypeNameById(ticket.BetTypeId);
            string showColosussBetsDetailsHtml = BuildShowColossusBetsDetails(
                ticket.RefNo,
                ticket.WinlostDate.ToString(),
                ticket.TransId.ToString(),
                ticket.CustId.ToString(),
                ticket.BetCheck);
            
            string colosussBetsHtml = BuildColossusBetsHtml(betTypeName, ticket.BetCheck, showColosussBetsDetailsHtml);

            Template.SetValue(colosussBetsHtml);
        }

        private string BuildShowColossusBetsDetails(string refNo, string winlossDate, string transId, string custId, string betCheck)
        {            
            string detailsFunction = $"showColossusBetsDetails('{refNo}', '{winlossDate}', '{transId}', '{custId}', '{betCheck}');";
            string detailsLink = $"<a style=\"color:#755200;font-weight:bold\" href=\"javascript:void('');\">{CoreBetList.details}</a>";

            return $"<div class=\"detail colossus-bets-details\" onclick=\"{detailsFunction }\">{detailsLink}</div>";
        }

        private string BuildColossusBetsHtml(string betTypeName, string colossusBetsId, string detailsFunctionDiv)
        {            
            var betTypeNameAndId = $"<span class=\"sport\">{betTypeName}</span> - <span class=\"underdog\">{colossusBetsId}</span>";

            return $"<div class=\"combinationLink\">{betTypeNameAndId}</div>{detailsFunctionDiv}";
        }
    }
}