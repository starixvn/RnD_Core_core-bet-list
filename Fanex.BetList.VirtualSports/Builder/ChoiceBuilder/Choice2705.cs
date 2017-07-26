namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Entities;
    using Templates;

    /// <summary>
    /// BetRadar Virtual Sports - FT HDP.
    /// </summary>
    public class Choice2705 : Choice1
    {
        private string _matchRoundInfo;

        public override Choice_Template Render(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData, bool isShowScoreMap)
        {
            _matchRoundInfo = VirtualSportsChoiceUtils.ParseMatchInfo(ticket.TransDesc);

            return base.Render(ticket, ticketHelper, ticketData, isShowScoreMap);
        }

        protected override void BuildMatch(ITicket ticket, ITicketHelper ticketHelper)
        {
            base.BuildMatch(ticket, ticketHelper);

            Template.AppendMatchOrRoundInfo(_matchRoundInfo);
        }

        protected override void BuildBetTeamsInfo(ITicket ticket, ref RTFHelper rtfHelper)
        {
            base.BuildBetTeamsInfo(ticket, ref rtfHelper);

            rtfHelper.RTFRenderer.AddText($"\n{_matchRoundInfo}");
        }
    }
}