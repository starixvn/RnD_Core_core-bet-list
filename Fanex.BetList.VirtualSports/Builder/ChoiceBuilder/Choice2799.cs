namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using System.Text;
    using Entities;
    using BetListNPOI.SS.UserModel;

    public class Choice2799 : Choice1
    {
        protected override void BuildChoice(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            string betTypeName = GetBetTypeName(ticket, ticketHelper);
            string mixParlaySubBetsFuncName = BuildMixParlaySubBetsFuncName(ticket, ticketHelper);
            string offeredCombinations = BuildOfferedCombinationBlock(ticket, ticketHelper);
            var mixParlayMarkup = new StringBuilder("<div class=\"vs-mix-parlay\">");

            mixParlayMarkup
                .Append($"<a id=\"hidden0\" href=\"{mixParlaySubBetsFuncName}\">{betTypeName}</a>")
                .Append(offeredCombinations)
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
    }
}