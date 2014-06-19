namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Constants;
    using Entities;

    /// <summary>
    /// BetRadar BetType : Choice of bet type Highest Scoring Half.
    /// </summary>
    public class Choice140 : Choice133
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var betTeam = string.Empty;

            switch (ticket.BetTeam.ToLower())
            {
                case BetTeamValue.TwoH:
                    betTeam = CoreLang.SecondHalf;
                    break;
                case BetTeamValue.OneH:
                    betTeam = CoreLang.FirstHalf;
                    break;
                case BetTeamValue.Tie:
                    betTeam = CoreLang.Tie;
                    break;
            }

            Template.betTeam = betTeam;
        }
    }
}