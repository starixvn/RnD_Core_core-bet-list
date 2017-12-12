namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Constants;
    using Entities;

    /// <summary>
    /// PH AIO Phase 3 - Last Team To Score.
    /// </summary>
    public class Choice448 : Choice1
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            string betTeam;

            switch (ticket.BetTeam?.ToLowerInvariant())
            {
                case BetTeamValue.H:
                    betTeam = Template.Match.homeTeam;
                    break;

                case BetTeamValue.A:
                    betTeam = Template.Match.awayTeam;
                    break;

                case BetTeamValue.NG:
                    betTeam = CoreBetList.NoGoal14;
                    break;

                default:
                    betTeam = string.Empty;
                    break;
            }

            Template.betTeam = betTeam;
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.betTeamClassName = Favorite;
            Template.Handicap.handicap = null;
        }
    }
}