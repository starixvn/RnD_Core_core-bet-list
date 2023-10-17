namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using System.Text;
    using App_GlobalResources;
    using Entities;
    using BetListNPOI.SS.UserModel;
    using Fanex.BetList.Core.Utils.StringExt;
    using System;

    public class Choice2799 : Choice1
    {
        private const string MaxPayoutXmlTag = "l255";
        protected override void BuildChoice(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            string betTypeName = GetBetTypeName(ticket, ticketHelper);
            string mixParlaySubBetsFuncName = BuildMixParlaySubBetsFuncName(ticket, ticketHelper);
            string offeredCombinations = BuildOfferedCombinationBlock(ticket, ticketHelper);
            string maxPayout = BuildMaxPayoutBlock(ticket, ticketHelper);
            var mixParlayMarkup = new StringBuilder("<div class=\"vs-mix-parlay\">");

            mixParlayMarkup
                .Append($"<a id=\"hidden0\" href=\"{mixParlaySubBetsFuncName}\">{betTypeName}</a>")
                .Append(offeredCombinations)
                .Append(maxPayout)
                .Append($"<div id=\"divEvent_{ticket.TransId}{ticketHelper.Index}\" style=\"display: none\" ></div>")
                .Append("</div>");

            Template.SetValue(mixParlayMarkup.ToString());
        }

        private static string GetBetTypeName(ITicket ticket, ITicketHelper ticketHelper)
            => ticketHelper.GetBetTypeNameById(ticket.BetTypeId);

        private static string BuildOfferedCombinationBlock(ITicket ticket, ITicketHelper ticketHelper)
        {
            var offeredCombinations = ticketHelper.GetCombinationMPOfferNameList(ticket.BetTeam, ticket.BetCheck, ticket.Handicap1, ticket.Handicap2);
            var combinationBlock = new StringBuilder($"<div class=\"offer-name-list\" id=\"combinationOfferName_{ticket.TransId}{ticketHelper.Index}\">");

            if (offeredCombinations != null)
            {
                foreach (var combination in offeredCombinations)
                {
                    combinationBlock.Append($"<div>{combination}</div>");
                }
            }

            combinationBlock.Append("</div>");

            return combinationBlock.ToString();
        }

        private static string BuildMixParlaySubBetsFuncName(ITicket ticket, ITicketHelper ticketHelper)
        {
            return string.IsNullOrWhiteSpace(ticketHelper.ClientSystemParlaySubbetsDetailFunction)
                ? $"javascript:showNewCombMP('{ticket.TransId}',{ticketHelper.Index},'{ticket.RefNo}',{ticket.Handicap1},{ticket.Handicap2},'{ticket.BetCheck}','{ticket.BetTeam}',{ticket.CustId});"
                : ticketHelper.ClientSystemParlaySubbetsDetailFunction;
        }

        private static string BuildMaxPayoutBlock(ITicket ticket, ITicketHelper ticketHelper)
        {
            if(ticket.Status.Equals(BetStatus.Won, StringComparison.OrdinalIgnoreCase) && ticket.ExtraInfo.HasXmlTag(MaxPayoutXmlTag)){
                var maxPayoutBlock =  new StringBuilder($"<div class=\"max-payout\" id=\"maxPayout_{ticket.TransId}{ticketHelper.Index}\">({CoreBetList.MaxPayoutAlert})<span class=\"icon-max-payout\" title=\"{CoreBetList.MaxPayoutAlertInfo}\"></span></div>");

                return maxPayoutBlock.ToString();
            }

            return string.Empty;
        }

        public override List<IRichTextString> RenderRTF(
            ITicket ticket,
            ITicketHelper ticketHelper,
            List<ITicketData> ticketData,
            bool isShowScoreMap,
            RTFHelper rtfHelper)
        {
            RTFHelper clonedRtfHelper = rtfHelper.Clone();

            clonedRtfHelper.RTFRenderer.AddText(GetBetTypeName(ticket, ticketHelper), clonedRtfHelper.PosFont);
            AppendRTFOfferedCombinations(clonedRtfHelper, ticket, ticketHelper);
            AppendRTFMaxPayout(clonedRtfHelper, ticket);

            IRichTextString choice = clonedRtfHelper.RTFRenderer.Render();

            clonedRtfHelper.RTFRenderer.Clear();

            return new List<IRichTextString> { choice };
        }

        private static void AppendRTFOfferedCombinations(RTFHelper rtfHelper, ITicket ticket, ITicketHelper ticketHelper)
        {
            IList<string> offeredCombinations = ticketHelper.GetCombinationMPOfferNameList(ticket.BetTeam, ticket.BetCheck, ticket.Handicap1, ticket.Handicap2);

            if (offeredCombinations != null)
            {
                foreach (var combination in offeredCombinations)
                {
                    rtfHelper.RTFRenderer.AddText($"\n{combination}", rtfHelper.NormalFont);
                }
            }
        }

        private static void AppendRTFMaxPayout(RTFHelper rtfHelper, ITicket ticket)
        { 
            if(ticket.ExtraInfo.HasXmlTag(MaxPayoutXmlTag)){
                rtfHelper.RTFRenderer.AddText($"\n({CoreBetList.MaxPayoutAlert})", rtfHelper.NormalFont);
            }
        }
    }
}