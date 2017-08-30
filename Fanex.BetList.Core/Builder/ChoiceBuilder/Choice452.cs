namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Constants;
    using Entities;

    /// <summary>
    /// PH AIO Phase 3 - Highest Scoring Half (2 Way).
    /// </summary>
    public class Choice452 : Choice1
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            string betTeam = ticket.BetTeam?.ToLowerInvariant();

            switch (betTeam)
            {
                case BetTeamValue.OneH:
                    Template.betTeam = CoreBetList.FirstHalf;
                    break;

                case BetTeamValue.TwoH:
                    Template.betTeam = CoreBetList.SecondHalf;
                    break;

                default:
                    Template.betTeam = string.Empty;
                    break;
            }
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.betTeamClassName = Favorite;
            Template.Handicap.handicap = null;
        }
    }
}