namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System;
    using System.Collections.Generic;
    using App_GlobalResources;
    using Constants;
    using Entities;
    using Templates;

    /// <summary>
    /// BetRadar Virtual Basketball - Winning Margin 6 Way.
    /// </summary>
    public class Choice2807 : Choice4
    {
        public const string BasketballRefId = "WMBasketball";

        private string _matchRoundInfo;

        public override Choice_Template Render(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData, bool isShowScoreMap)
        {
            _matchRoundInfo = VirtualSportsChoiceUtils.ParseMatchInfo(ticket.TransDesc, ticketHelper);

            return base.Render(ticket, ticketHelper, ticketData, isShowScoreMap);
        }

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            string betTeam = ticket.BetTeam.ToLowerInvariant();

            if (betTeam.Length > 1)
            {
                string resourceId = betTeam.Substring(1);
                string betOptionFormat = ticketHelper.GetResourceData(BasketballRefId, resourceId);
                string teamName = betTeam.StartsWith(BetTeamValue.H, StringComparison.InvariantCultureIgnoreCase)
                    ? CoreBetList.Home
                    : CoreBetList.Away;

                if (!string.IsNullOrEmpty(betOptionFormat))
                {
                    Template.betTeam = string.Format(betOptionFormat, teamName);

                    return;
                }
            }

            Template.betTeam = string.Empty;
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